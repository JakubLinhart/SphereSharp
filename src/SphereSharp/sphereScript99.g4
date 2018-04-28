grammar sphereScript99;

macro : '<' call '>' ;
call : SYMBOL LPAREN argumentList RPAREN ;

argumentList : argument (',' argument)* ;
argument : expressionArgument | unquotedLiteralArgument;
expressionArgument : signedOperand binaryOperation* ;
unquotedLiteralArgument: (call | SYMBOL | macro | operator | NUMBER | ' ' | '[' | ']')+? ;

signedOperand: (MINUS | PLUS) signedOperand | operand ;
operand: constantExpression | subExpression | macroExpression ;
binaryOperation : operator signedOperand ;
operator: constantOperator | macroOperator ;

constantExpression : NUMBER ;
macroExpression : macro ;
subExpression : '(' expressionArgument ')' ;

macroOperator : macro ;
constantOperator : PLUS | MINUS | '*' ;

SYMBOL : VALID_SYMBOL_START VALID_SYMBOL_CHAR*;

NUMBER : DIGIT+ ;

PLUS: '+' ;
MINUS: '-' ;

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
