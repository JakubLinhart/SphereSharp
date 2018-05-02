grammar sphereScript99;

file: NEWLINE? section+ (eofSection | EOF);

section: functionSection | itemDefSection | typeDefSection | templateSection;
eofSection: EOF_SECTION_HEADER;

functionSection: functionSectionHeader codeBlock;
functionSectionHeader: FUNCTION_SECTION_HEADER_START SYMBOL ']' NEWLINE;

itemDefSection: itemDefSectionHeader propertyList triggerList ;
itemDefSectionHeader: ITEMDEF_SECTION_HEADER_START SYMBOL ']' NEWLINE;

typeDefSection: typeDefSectionHeader triggerList ;
typeDefSectionHeader: TYPEDEF_SECTION_HEADER_START SYMBOL ']' NEWLINE;

templateSection: templateSectionHeader propertyList ;
templateSectionHeader: TEMPLATE_SECTION_HEADER_START SYMBOL ']' NEWLINE;

codeBlock: statement+;

statement: WS*? (call | assignment | ifStatement | whileStatement) (NEWLINE | EOF);

ifStatement: IF WS+ evalExpression NEWLINE codeBlock? (elseIfStatement)* elseStatement? WS* ENDIF ;
elseIfStatement: WS* ELSEIF WS+ evalExpression (NEWLINE | EOF) codeBlock?;
elseStatement: WS* ELSE NEWLINE codeBlock?;

whileStatement: WHILE WS* evalExpression NEWLINE codeBlock? WS* ENDWHILE;

macro: LESS_THAN firstMemberAccess MORE_THAN ;
call: firstMemberAccess;
assignment: firstMemberAccess ASSIGN argumentList;

memberAccess: firstMemberAccess | argumentAccess;
firstMemberAccess: evalCall | nativeMemberAccess | customMemberAccess;
unquotedMemberAccessLiteral: (SYMBOL | macro | argumentOperator | DEC_NUMBER | HEX_NUMBER | WS | '[' | ']' | '#' | ':')+?;
evalCall: EVAL_FUNCTIONS WS* evalExpression; 
nativeMemberAccess: nativeFunction nativeArgumentList? chainedMemberAccess?;
nativeArgumentList: enclosedArgumentList | (WS+ argumentList);
argumentAccess: (expressionArgument | quotedLiteralArgument | unquotedArgumentAccess) chainedMemberAccess?;
unquotedArgumentAccess: (SYMBOL | macro | argumentOperator | DEC_NUMBER | HEX_NUMBER | WS | '[' | ']' | '#' | ':')+? ;
customMemberAccess: memberName enclosedArgumentList? chainedMemberAccess?;
chainedMemberAccess: '.' memberAccess;

nativeFunction: SYSMESSAGE | RETURN | TIMER | CONSUME | EVENTS | TRIGGER;
memberName: (SYMBOL | macro)+;

// properties
propertyList: propertyAssignment*;
propertyAssignment: propertyName ASSIGN propertyValue (NEWLINE | EOF);
propertyName: SYMBOL;
propertyValue: (SYMBOL | DEC_NUMBER | WS)+;

// trigger
triggerList: trigger*;
trigger: TRIGGER_HEADER triggerName (NEWLINE | EOF) triggerBody?;
triggerName: TIMER | SYMBOL;
triggerBody: codeBlock;

// argument, argument expression
enclosedArgumentList: LPAREN argumentList? RPAREN;
argumentList: argument (',' argument)*;
argument: triggerArgument | expressionArgument | quotedLiteralArgument | eventArgument | unquotedLiteralArgument;
expressionArgument: signedArgumentOperand argumentBinaryOperation* ;
quotedLiteralArgument: '"' unquotedLiteralArgument '"';
unquotedLiteralArgument: (memberAccess | SYMBOL | macro | argumentOperator | DEC_NUMBER | HEX_NUMBER | WS | '[' | ']' | '#' | ':')+? ;
triggerArgument: '@' SYMBOL;
eventArgument: (PLUS | MINUS) SYMBOL;

signedArgumentOperand: unaryOperator signedArgumentOperand | argumentOperand ;
argumentOperand: rangeExpression | constantExpression | argumentSubExpression | macroExpression ;
argumentBinaryOperation: argumentOperator signedArgumentOperand ;
argumentOperator: argumentBinaryOperator | macroOperator ;
argumentSubExpression: '(' expressionArgument ')' ;
argumentBinaryOperator: binaryOperator;

// eval expression
evalExpression: signedEvalOperand evalBinaryOperation* ;
signedEvalOperand: unaryOperator signedEvalOperand | evalOperand;
evalOperand: rangeExpression | constantExpression | evalSubExpression | macro | firstMemberAccess;
evalBinaryOperation: evalOperator signedEvalOperand ;
evalOperator: WS* (evalBinaryOperator | macroOperator) WS* ;
evalSubExpression: '(' evalExpression ')' ;
evalBinaryOperator: binaryOperator | EQUAL | NOT_EQUAL | MORE_THAN_EQUAL | LESS_THAN_EQUAL | MORE_THAN | LESS_THAN;
binaryOperator: PLUS | MINUS | MULTIPLY | DIVIDE | MODULO | LOGICAL_AND | LOGICAL_OR | BITWISE_AND | BITWISE_OR;

constantExpression: DEC_NUMBER | HEX_NUMBER;
rangeExpression: '{' evalExpression WS+ evalExpression '}';
macroExpression: macro ;

macroOperator: macro ;
unaryOperator: PLUS | MINUS | LOGICAL_NOT | BITWISE_COMPLEMENT;

NEWLINE: ([ \t]* ('//' (~( '\r' | '\n' ))*)? ('\r'? '\n') )+;
WS: [ \t];
EOF_SECTION_HEADER: '[' [eE] [oO] [fF] ']';
FUNCTION_SECTION_HEADER_START: '[' [fF][uU][nN][cC][tT][iI][oO][nN] WS+;
ITEMDEF_SECTION_HEADER_START: '[' [iI][tT][eE][mM][dD][eE][fF] WS+;
TYPEDEF_SECTION_HEADER_START: '[' [tT][yY][pP][eE][dD][eE][fF] WS+;
TEMPLATE_SECTION_HEADER_START: '[' [tT][eE][mM][pP][lL][aA][tT][eE] WS+;
IF: [iI][fF];
ELSEIF: [eE][lL][sS][eE][iI][fF]; 
ELSE: [eE][lL][sS][eE];
ENDIF: [eE][nN][dD][iI][fF];
WHILE: [wW][hH][iI][lL][eE];
ENDWHILE: [eE][nN][dD][wW][hH][iI][lL][eE];

SYSMESSAGE: [sS][yY][sS][mM][eE][sS][sS][aA][gG][eE];
RETURN: [rR][eE][tT][uU][rR][nN];
TIMER: [tT][iI][mM][eE][rR];
CONSUME: [cC][oO][nN][sS][uU][mM][eE];
EVENTS: [eE][vV][eE][nN][tT][sS];
TRIGGER: [tT][rR][iI][gG][gG][eE][rR];

EVAL_FUNCTIONS: EVAL | HVAL | SAFE;
EVAL: [eE][vV][aA][lL];
HVAL: [hH][vV][aA][lL];
SAFE: [sS][aA][fF][eE];

TRIGGER_HEADER: [oO][nN] '=@';
SYMBOL: VALID_SYMBOL_START VALID_SYMBOL_CHAR*;
DEC_NUMBER: ('1' .. '9') DEC_DIGIT* ;
HEX_NUMBER: '0' HEX_DIGIT* ;

EQUAL: '==';
ASSIGN: '=';
NOT_EQUAL: '!=';
MORE_THAN: '>';
LESS_THAN: '<';
PLUS: '+' ;
MINUS: '-' ;
MULTIPLY: '*';
DIVIDE: '/';
BITWISE_AND: '&';
BITWISE_OR: '|';
BITWISE_COMPLEMENT: '~';
MODULO: '%';
LOGICAL_AND: '&&';
LOGICAL_OR: '||';
LOGICAL_NOT: '!';

MORE_THAN_EQUAL: MORE_THAN ASSIGN;
LESS_THAN_EQUAL: LESS_THAN ASSIGN;

fragment VALID_SYMBOL_START
   : ('a' .. 'z') | ('A' .. 'Z') | '_'
   ;

fragment VALID_SYMBOL_CHAR
   : VALID_SYMBOL_START | DEC_DIGIT
   ;

fragment DEC_DIGIT : ('0' .. '9');

fragment HEX_DIGIT : DEC_DIGIT | ('a' .. 'f') | ('A' .. 'F');

LPAREN
   : '('
   ;

RPAREN
   : ')'
   ;
