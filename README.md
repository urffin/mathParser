# mathParser
simple parser math string

Parse string to C# Expression Tree

## Available operations:
Unary operations: +, -
Binary operations with priority: +,-,*,/  
Expression in brackets.  
Built-in function (case insensitive): sin, cos, exp  

## It's not a bug, it's a feature!
For binary operations, (except **+,-**) and functions available prefix, infix, postfix notation

In case **+** and **-** operations: in prefix notation identify as unary, infix and postfix - work as expected
