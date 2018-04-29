grammar sphereScript99;

file: functionSection+ eofSection? EOF;

section: functionSection;
eofSection: EOF_SECTION_HEADER (NEWLINE+ | EOF);
functionSection: functionSectionHeader codeBlock;
functionSectionHeader: FUNCTION_SECTION_HEADER_START SYMBOL ']' (NEWLINE+ | EOF);
codeBlock: statement+;

statement: (call | assignment) (NEWLINE+ | EOF);

macro: '<' memberAccess '>' ;
call: memberAccess;
assignment: memberAccess '=' argumentList;

memberAccess: memberName enclosedArgumentList? chainedMemberAccess?;
chainedMemberAccess: '.' memberAccess;

memberName: (SYMBOL | macro)+;

enclosedArgumentList: LPAREN argumentList? RPAREN;
argumentList: argument (',' argument)*;
argument: expressionArgument | unquotedLiteralArgument;
expressionArgument: signedOperand binaryOperation* ;
unquotedLiteralArgument: (memberAccess | SYMBOL | macro | operator | NUMBER | ' ' | '[' | ']')+? ;

signedOperand: (MINUS | PLUS) signedOperand | operand ;
operand: constantExpression | subExpression | macroExpression ;
binaryOperation: operator signedOperand ;
operator: constantOperator | macroOperator ;

constantExpression: NUMBER ;
macroExpression: macro ;
subExpression: '(' expressionArgument ')' ;

macroOperator: macro ;
constantOperator: PLUS | MINUS | MULTIPLY ;

EOF_SECTION_HEADER: '[' [eE] [oO] [fF] ']';
FUNCTION_SECTION_HEADER_START: '[function' WS+;

SYMBOL : VALID_SYMBOL_START VALID_SYMBOL_CHAR*;

NUMBER : DIGIT+ ;

PLUS: '+' ;
MINUS: '-' ;
MULTIPLY: '*';

NEWLINE: '\r'? '\n';
WS : (' ' | '\t') ;

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
