using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MathParser.Tokens;

namespace MathParser
{
    public static class Parser
    {
        public static Expression<Func<double>> ParseWithoutParameter(string expression)
        {
            Stack<OperationToken> operationStack = new Stack<OperationToken>();
            Stack<Expression> expressionStack = new Stack<Expression>();

            foreach (var token in expression.Tokinize())
            {
                switch (token.TokenType)
                {
                    case TokenType.Value:
                        expressionStack.Push(token.GetExpression());
                        break;
                    case TokenType.Operation:
                        ProcessOperationToken((OperationToken)token, operationStack, expressionStack);
                        break;
                    case TokenType.OpenBracket:
                        operationStack.Push((OperationToken)token);
                        break;
                    case TokenType.CloseBracket:
                        ProcessCloseToken((OperationToken)token, operationStack, expressionStack);
                        break;
                    default: throw new InvalidOperationException("Token with wrong token type: " + token.TokenType);
                }
            }

            ProcessEndStack(operationStack, expressionStack);

            if (expressionStack.Count != 1)
            {
                throw new InvalidOperationException("Error parse expression: " + expression);
            }


            return Expression.Lambda<Func<double>>(expressionStack.Pop());
        }

        private static void ProcessEndStack(Stack<OperationToken> operationStack, Stack<Expression> expressionStack)
        {
            while (operationStack.Count > 0)
            {
                var tokenFromStack = operationStack.Pop();
                var tokenParameters = new Expression[tokenFromStack.Arity];
                for (int i = tokenParameters.Length - 1; i >= 0; i--)
                {
                    tokenParameters[i] = expressionStack.Pop();
                }
                expressionStack.Push(tokenFromStack.GetExpression(tokenParameters));
            }

        }

        private static void ProcessCloseToken(OperationToken operationToken, Stack<OperationToken> operationStack, Stack<Expression> expressionStack)
        {
            do
            {
                var tokenFromStack = operationStack.Pop();
                var tokenParameters = new Expression[tokenFromStack.Arity];
                for (int i = tokenParameters.Length - 1; i >= 0; i--)
                {
                    tokenParameters[i] = expressionStack.Pop();
                }
                expressionStack.Push(tokenFromStack.GetExpression(tokenParameters));

            } while (operationStack.Peek().TokenType != TokenType.OpenBracket);

            operationStack.Pop();
        }

        private static void ProcessOperationToken(OperationToken token, Stack<OperationToken> operationStack, Stack<Expression> expressionStack)
        {
            if (operationStack.Count > 0 && operationStack.Peek().Priority > token.Priority)
            {
                do
                {
                    var tokenFromStack = operationStack.Pop();
                    var tokenParameters = new Expression[tokenFromStack.Arity];
                    for (int i = tokenParameters.Length - 1; i >= 0; i--)
                    {
                        tokenParameters[i] = expressionStack.Pop();
                    }
                    expressionStack.Push(tokenFromStack.GetExpression(tokenParameters));

                } while (operationStack.Count > 0 && operationStack.Peek().Priority > token.Priority);
            }

            operationStack.Push(token);
        }
    }
}
