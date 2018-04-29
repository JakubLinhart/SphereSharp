grammar sphereScript99;

file: functionSection+ eofSection? EOF;

section: functionSection;
eofSection: EOF_SECTION_HEADER (NEWLINE+ | EOF);
functionSection: functionSectionHeader codeBlock;
functionSectionHeader: FUNCTION_SECTION_HEADER_START SYMBOL ']' (NEWLINE | EOF);
codeBlock: statement+;

statement: WS* (call | assignment | ifStatement) (NEWLINE | EOF);

ifStatement: IF WS+ evalExpression (NEWLINE | EOF) codeBlock (elseIfStatement)* elseStatement? WS* ENDIF ;
elseIfStatement: WS* ELSEIF WS+ evalExpression (NEWLINE | EOF) codeBlock;
elseStatement: WS* ELSE (NEWLINE | EOF) codeBlock;

macro: LESS_THAN memberAccess MORE_THAN ;
call: memberAccess;
assignment: memberAccess ASSIGN argumentList;

memberAccess: evalCall | nativeMemberAccess | customMemberAccess;
evalCall: EVAL_FUNCTIONS WS* evalExpression; 
nativeMemberAccess: NATIVE_FUNCTIONS nativeArgumentList? chainedMemberAccess?;
nativeArgumentList: enclosedArgumentList | (WS+ argumentList);
customMemberAccess: memberName enclosedArgumentList? chainedMemberAccess?;
chainedMemberAccess: '.' memberAccess;

memberName: (SYMBOL | macro)+;

// argument, argument expression
enclosedArgumentList: LPAREN argumentList? RPAREN;
argumentList: argument (',' argument)*;
argument: expressionArgument | quotedLiteralArgument | unquotedLiteralArgument;
expressionArgument: signedArgumentOperand argumentBinaryOperation* ;
quotedLiteralArgument: '"' unquotedLiteralArgument '"';
unquotedLiteralArgument: (memberAccess | SYMBOL | macro | argumentOperator | NUMBER | WS | '[' | ']')+? ;

signedArgumentOperand: (MINUS | PLUS) signedArgumentOperand | argumentOperand ;
argumentOperand: constantExpression | argumentSubExpression | macroExpression ;
argumentBinaryOperation: argumentOperator signedArgumentOperand ;
argumentOperator: argumentBinaryOperator | macroOperator ;
argumentSubExpression: '(' expressionArgument ')' ;
argumentBinaryOperator: PLUS | MINUS | MULTIPLY ;

// eval expression
evalExpression: signedEvalOperand evalBinaryOperation* ;
signedEvalOperand: unaryOperator signedEvalOperand | evalOperand;
evalOperand: constantExpression | evalSubExpression | macro | memberAccess;
evalBinaryOperation: evalOperator signedEvalOperand ;
evalOperator: WS* (evalBinaryOperator | macroOperator) WS* ;
evalSubExpression: '(' evalExpression ')' ;
evalBinaryOperator: PLUS | MINUS | MULTIPLY | EQUAL | NOT_EQUAL | MORE_THAN_EQUAL | LESS_THAN_EQUAL | MORE_THAN | LESS_THAN;

constantExpression: NUMBER ;
macroExpression: macro ;

macroOperator: macro ;
unaryOperator: PLUS | MINUS;

NEWLINE: ('\r'? '\n')+;
WS: [ \t];

EOF_SECTION_HEADER: '[' [eE] [oO] [fF] ']';
FUNCTION_SECTION_HEADER_START: '[function' WS+;
IF: [iI][fF];
ELSEIF: [eE][lL][sS][eE][iI][fF]; 
ELSE: [eE][lL][sS][eE];
ENDIF: [eE][nN][dD][iI][fF];

NATIVE_FUNCTIONS: SYSMESSAGE | RETURN | TIMER | CONSUME;
SYSMESSAGE: [sS][yY][sS][mM][eE][sS][sS][aA][gG][eE];
RETURN: [rR][eE][tT][uU][rR][nN];
TIMER: [tT][iI][mM][eE][rR];
CONSUME: [cC][oO][nN][sS][uU][mM][eE];

EVAL_FUNCTIONS: EVAL | HVAL | SAFE;
EVAL: [eE][vV][aA][lL];
HVAL: [hH][vV][aA][lL];
SAFE: [sS][aA][fF][eE];

SYMBOL : VALID_SYMBOL_START VALID_SYMBOL_CHAR*;
NUMBER : DIGIT+ ;

EQUAL: '==';
ASSIGN: '=';
NOT_EQUAL: '!=';
MORE_THAN: '>';
LESS_THAN: '<';
PLUS: '+' ;
MINUS: '-' ;
MULTIPLY: '*';

MORE_THAN_EQUAL: MORE_THAN ASSIGN;
LESS_THAN_EQUAL: LESS_THAN ASSIGN;

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
