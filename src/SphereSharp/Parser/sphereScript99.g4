grammar sphereScript99;

macro : '<' call '>' ;
call : SYMBOL LPAREN argumentList RPAREN ;

argumentList : argument (',' argument)* ;
argument : expressionArgument | unquotedLiteralArgument;
expressionArgument : operand binaryOperation* ;
unquotedLiteralArgument: (call | SYMBOL | macro | operator | NUMBER | ' ' | '[' | ']')+? ;

operand: (constantExpression | subExpression | macroExpression) ;
binaryOperation : operator operand ;
operator: constantOperator | macroOperator ;

constantExpression : NUMBER ;
macroExpression : macro ;
subExpression : '(' expressionArgument ')' ;

macroOperator : macro ;
constantOperator : BINARY_OPERATOR ;

SYMBOL : VALID_SYMBOL_START VALID_SYMBOL_CHAR*;

NUMBER : DIGIT+ ;

BINARY_OPERATOR : '+'|'-'|'*' ;

LITERAL : '"' .*? '"' ;

fragment VALID_SYMBOL_START
   : ('a' .. 'z') | ('A' .. 'Z') | '_'
   ;


fragment VALID_SYMBOL_CHAR
   : VALID_SYMBOL_START | DIGIT
   ;

fragment DIGIT
   : ('0' .. '9')
   ;

LPAREN
   : '('
   ;

RPAREN
   : ')'
   ;
