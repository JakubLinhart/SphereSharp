﻿grammar sphereScript99;

file: NEWLINE? section+ (eofSection | EOF);

section: WS* functionSection | itemDefSection | charDefSection | typeDefSection | templateSection |
            eventsSection | defNamesSection | dialogSection | dialogTextSection | dialogButtonSection;
eofSection: EOF_SECTION_HEADER;

functionSection: functionSectionHeader codeBlock;
functionSectionHeader: FUNCTION_SECTION_HEADER_START SYMBOL ']' NEWLINE;

itemDefSection: itemDefSectionHeader propertyList triggerList ;
itemDefSectionHeader: ITEMDEF_SECTION_HEADER_START SYMBOL ']' NEWLINE;

charDefSection: charDefSectionHeader propertyList triggerList ;
charDefSectionHeader: CHARDEF_SECTION_HEADER_START SYMBOL ']' NEWLINE;

typeDefSection: typeDefSectionHeader triggerList ;
typeDefSectionHeader: TYPEDEF_SECTION_HEADER_START SYMBOL ']' NEWLINE;

templateSection: templateSectionHeader propertyList ;
templateSectionHeader: TEMPLATE_SECTION_HEADER_START SYMBOL ']' NEWLINE;

eventsSection: eventsSectionHeader triggerList ;
eventsSectionHeader: EVENTS_SECTION_HEADER_START SYMBOL ']' NEWLINE;

defNamesSection: defNamesSectionHeader propertyList;
defNamesSectionHeader: DEFNAMES_SECTION_HEADER_START ~(NEWITEM | ']') ']' NEWLINE;

dialogSection: dialogSectionHeader dialogPosition? codeBlock;
dialogSectionHeader: DIALOG_SECTION_HEADER_START SYMBOL ']' NEWLINE;
dialogPosition: number WS* ',' WS* number NEWLINE;

dialogTextSection: dialogTextSectionHeader dialogTextSectionLine*;
dialogTextSectionHeader: DIALOG_SECTION_HEADER_START SYMBOL WS+ TEXT ']' NEWLINE;
dialogTextSectionLine: ~(NEWLINE)+ NEWLINE;

dialogButtonSection: dialogButtonSectionHeader triggerList;
dialogButtonSectionHeader: DIALOG_SECTION_HEADER_START SYMBOL WS+ BUTTON ']' NEWLINE;

codeBlock: statement+;
number: DEC_NUMBER | HEX_NUMBER;

statement: WS*? (call | assignment | ifStatement | whileStatement | doswitchStatement) (NEWLINE | EOF);

ifStatement: IF WS* evalExpression NEWLINE codeBlock? (elseIfStatement)* elseStatement? WS* ENDIF ;
elseIfStatement: WS* ELSEIF WS+ evalExpression (NEWLINE | EOF) codeBlock?;
elseStatement: WS* ELSE NEWLINE codeBlock?;

whileStatement: WHILE WS* evalExpression NEWLINE codeBlock? WS* ENDWHILE;

doswitchStatement: DOSWITCH WS* evalExpression NEWLINE codeBlock WS* ENDDO;

macro: escapedMacro | nonEscapedMacro;
escapedMacro: LESS_THAN '?' macroBody '?' MORE_THAN ;
nonEscapedMacro: LESS_THAN macroBody  MORE_THAN ;
macroBody: (firstMemberAccess | indexedMemberName);
call: firstMemberAccess;
assignment: firstMemberAccess WS* ASSIGN WS* argumentList?;

memberAccess: firstMemberAccess | argumentAccess;
firstMemberAccess: evalCall | nativeMemberAccess | customMemberAccess;
evalCall: EVAL_FUNCTIONS WS* evalExpression; 
nativeMemberAccess: nativeFunction nativeArgumentList? chainedMemberAccess?;
nativeArgumentList: enclosedArgumentList | freeArgumentList;
argumentAccess: (evalExpression | quotedLiteralArgument | unquotedArgumentAccess) chainedMemberAccess?;
unquotedArgumentAccess: (SYMBOL | macro | binaryOperator | constantExpression | WS | '[' | ']' | '#' | ':' | '.'|  ',' | '?' | '!' | EQUAL)+? ;
customMemberAccess: memberName enclosedArgumentList? chainedMemberAccess?;
chainedMemberAccess: '.' memberAccess;

nativeFunction: SYSMESSAGE | RETURN | TIMER | CONSUME | EVENTS | TRIGGER | ARROWQUEST | DIALOG | EVAL_FUNCTIONS | SOUND | TRY | X | NEWITEM | EQUIP
                | MENU | GO | INVIS | SHOW | DAMAGE | ECHO | XXC | XXI | MOVE | RESIZEPIC | TILEPIC | HTMLGUMP | PAGE | TEXTENTRY | TEXT | BUTTON;
memberName: (SYMBOL | macro)+?;
indexedMemberName: memberName '[' evalExpression ']';

// properties
propertyList: NEWLINE? propertyAssignment+;
propertyAssignment: WS* propertyName  ((WS* ASSIGN WS*) | WS+) propertyValue (NEWLINE | EOF);
propertyName: SYMBOL ('[' number ']')?;
propertyValue: unquotedLiteralArgument;

// trigger
triggerList: trigger*;
trigger: TRIGGER_HEADER (('@' triggerName) | (number)) (NEWLINE | EOF) triggerBody?;
triggerName: nativeFunction | SYMBOL;
triggerBody: codeBlock;

// argument, argument expression
enclosedArgumentList: LPAREN argumentList? RPAREN;
freeArgumentList: firstFreeArgument (',' argument)*;
firstFreeArgument: firstFreeArgumentOptionalWhiteSpace | firstFreeArgumentMandatoryWhiteSpace;
firstFreeArgumentOptionalWhiteSpace: WS* (triggerArgument | evalExpression | quotedLiteralArgument);
firstFreeArgumentMandatoryWhiteSpace: WS+ (assignmentArgument | unquotedLiteralArgument);
argumentList: argument (',' argument)*;
argument: triggerArgument | evalExpression | quotedLiteralArgument | assignmentArgument | unquotedLiteralArgument;
assignmentArgument: assignment;
quotedLiteralArgument: '"' innerQuotedLiteralArgument '"';
innerQuotedLiteralArgument: (macro | '\'' | '\\' | ~('"' | NEWLINE))*?;
unquotedLiteralArgument: (memberAccess | SYMBOL | macro | constantExpression | WS | '[' | ']' | '#' | ':' |  '.'|  ',' | '?' | '!' | assignment | EQUAL)+? ;
triggerArgument: '@' SYMBOL;

// eval expression
evalExpression: signedEvalOperand evalBinaryOperation* ;
signedEvalOperand: unaryOperator signedEvalOperand | evalOperand;
evalOperand: randomExpression | constantExpression | macroConstantExpression | evalSubExpression | macro | indexedMemberName | firstMemberAccess;
evalBinaryOperation: evalOperator signedEvalOperand ;
evalOperator: WS* (evalBinaryOperator | macroOperator) WS* ;
evalSubExpression: '(' WS* evalExpression WS* ')' ;
evalBinaryOperator: binaryOperator | EQUAL | NOT_EQUAL | moreThanEqual | lessThanEqual | MORE_THAN | LESS_THAN;
binaryOperator: PLUS | MINUS | MULTIPLY | DIVIDE | MODULO | LOGICAL_AND | LOGICAL_OR | BITWISE_AND | BITWISE_OR;
moreThanEqual: MORE_THAN ASSIGN;
lessThanEqual: LESS_THAN ASSIGN;

macroConstantExpression: constantExpression macro;
constantExpression: DEC_NUMBER | HEX_NUMBER;
randomExpression: '{' (randomExpressionList | macro) '}';
randomExpressionList: WS* evalExpression WS+ evalExpression (WS+ evalExpression WS+ evalExpression)* WS*;
macroExpression: macro ;

macroOperator: macro ;
unaryOperator: PLUS | MINUS | LOGICAL_NOT | BITWISE_COMPLEMENT;

NEWLINE: ([ \t]* ('//' (~( '\r' | '\n' ))*)? ('\r'? '\n') )+;
WS: [ \t];
EOF_SECTION_HEADER: '[' [eE] [oO] [fF] ']';
FUNCTION_SECTION_HEADER_START: '[' [fF][uU][nN][cC][tT][iI][oO][nN] WS+;
ITEMDEF_SECTION_HEADER_START: '[' [iI][tT][eE][mM][dD][eE][fF] WS+;
CHARDEF_SECTION_HEADER_START: '[' [cC][hH][aA][rR][dD][eE][fF] WS+;
TYPEDEF_SECTION_HEADER_START: '[' [tT][yY][pP][eE][dD][eE][fF] WS+;
TEMPLATE_SECTION_HEADER_START: '[' [tT][eE][mM][pP][lL][aA][tT][eE] WS+;
EVENTS_SECTION_HEADER_START: '[' [eE][vV][eE][nN][tT][sS] WS+;
DEFNAMES_SECTION_HEADER_START: '[' [dD][eE][fF][nN][aA][mM][eE][sS] WS+;
DIALOG_SECTION_HEADER_START: '[' [dD][iI][aA][lL][oO][gG] WS+;
IF: [iI][fF];
ELSEIF: [eE][lL][sS][eE][iI][fF]; 
ELSE: [eE][lL][sS][eE];
ENDIF: [eE][nN][dD][iI][fF];
WHILE: [wW][hH][iI][lL][eE];
ENDWHILE: [eE][nN][dD][wW][hH][iI][lL][eE];
DOSWITCH: [dD][oO][sS][wW][iI][tT][cC][hH];
ENDDO: [eE][nN][dD][dD][oO];

SYSMESSAGE: [sS][yY][sS][mM][eE][sS][sS][aA][gG][eE];
RETURN: [rR][eE][tT][uU][rR][nN];
TIMER: [tT][iI][mM][eE][rR];
CONSUME: [cC][oO][nN][sS][uU][mM][eE];
EVENTS: [eE][vV][eE][nN][tT][sS];
TRIGGER: [tT][rR][iI][gG][gG][eE][rR];
ARROWQUEST: [aA][rR][rR][oO][wW][qQ][uU][eE][sS][tT];
DIALOG: [dD][iI][aA][lL][oO][gG];
SOUND: [sS][oO][uU][nN][dD];
TRY: [tT][rR][yY];
X: [xX];
XXC: [xX][xX][cC];
XXI: [xX][xX][iI];
NEWITEM: [nN][eE][wW][iI][tT][eE][mM];
EQUIP: [eE][qQ][uU][iI][pP];
MENU: [mM][eE][nN][uU];
GO: [gG][oO];
INVIS: [iI][nN][vV][iI][sS];
SHOW: [sS][hH][oO][wW];
DAMAGE: [dD][aA][mM][aA][gG][eE];
ECHO: [eE][cC][hH][oO];
MOVE: [mM][oO][vV][eE];
RESIZEPIC: [rR][eE][sS][iI][zZ][eE][pP][iI][cC];
TILEPIC: [tT][iI][lL][eE][pP][iI][cC];
HTMLGUMP: [hH][tT][mM][lL][gG][uU][mM][pP];
PAGE: [pP][aA][gG][eE];
TEXTENTRY: [tT][eE][xX][tT][eE][nN][tT][rR][yY];
TEXT: [tT][eE][xX][tT];
BUTTON: [bB][uU][tT][tT][oO][nN];

EVAL_FUNCTIONS: EVAL | HVAL | SAFE;
EVAL: [eE][vV][aA][lL];
HVAL: [hH][vV][aA][lL];
SAFE: [sS][aA][fF][eE];

TRIGGER_HEADER: [oO][nN] '=';
SYMBOL: VALID_SYMBOL_START VALID_SYMBOL_CHAR*;
DEC_NUMBER: ('1' .. '9') DEC_DIGIT* ;
HEX_NUMBER: '0' HEX_DIGIT* ;

EQUAL: '==';
NOT_EQUAL: '!=';
MORE_THAN: '>';
LESS_THAN: '<';
ASSIGN: '=';
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
