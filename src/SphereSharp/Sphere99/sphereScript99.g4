grammar sphereScript99;

file: NEWLINE? section+ (eofSection | EOF);

section: WS* functionSection | itemDefSection | charDefSection | typeDefSection | templateSection
            | eventsSection | defNamesSection | dialogSection | dialogTextSection | dialogButtonSection
            | bookSection | bookPageSection;
eofSection: EOF_SECTION_HEADER;

functionSection: functionSectionHeader codeBlock;
functionSectionHeader: FUNCTION_SECTION_HEADER_START SYMBOL ']' NEWLINE;

itemDefSection: itemDefSectionHeader propertyList triggerList ;
itemDefSectionHeader: ITEMDEF_SECTION_HEADER_START itemDefSectionName ']' NEWLINE;
itemDefSectionName: SYMBOL | number;

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
dialogSectionHeader: DIALOG_SECTION_HEADER_START dialogName=SYMBOL ']' NEWLINE;
dialogPosition: number WS* ',' WS* number NEWLINE;

dialogTextSection: dialogTextSectionHeader dialogTextSectionLine*?;
dialogTextSectionHeader: DIALOG_SECTION_HEADER_START dialogName=SYMBOL WS+ TEXT ']' NEWLINE;
dialogTextSectionLine: ~(FUNCTION_SECTION_HEADER_START | ITEMDEF_SECTION_HEADER_START | CHARDEF_SECTION_HEADER_START
    | TYPEDEF_SECTION_HEADER_START | TEMPLATE_SECTION_HEADER_START | EVENTS_SECTION_HEADER_START | DEFNAMES_SECTION_HEADER_START
    | DIALOG_SECTION_HEADER_START | BOOK_SECTION_HEADER_START | NEWLINE)* NEWLINE;

dialogButtonSection: dialogButtonSectionHeader triggerList;
dialogButtonSectionHeader: DIALOG_SECTION_HEADER_START dialogName=SYMBOL WS+ BUTTON ']' NEWLINE;

bookPageSection: bookPageSectionHeader pageLine*?;
bookPageSectionHeader: BOOK_SECTION_HEADER_START bookName=SYMBOL WS+ pageNumber=DEC_NUMBER ']' NEWLINE;
pageLine: ~(FUNCTION_SECTION_HEADER_START | ITEMDEF_SECTION_HEADER_START | CHARDEF_SECTION_HEADER_START
    | TYPEDEF_SECTION_HEADER_START | TEMPLATE_SECTION_HEADER_START | EVENTS_SECTION_HEADER_START | DEFNAMES_SECTION_HEADER_START
    | DIALOG_SECTION_HEADER_START | BOOK_SECTION_HEADER_START | NEWLINE)* NEWLINE;

bookSection: bookSectionHeader propertyList;
bookSectionHeader: BOOK_SECTION_HEADER_START bookName=SYMBOL ']' NEWLINE;

codeBlock: statement+;
number: DEC_NUMBER | HEX_NUMBER;

statement: WS*? (call | assignment | ifStatement | whileStatement | doswitchStatement | dorandStatement) (NEWLINE | EOF);

ifStatement: IF IF_WS=WS* condition NEWLINE codeBlock? (elseIfStatement)* elseStatement? endIf ;
endIf: WS* ENDIF;
elseIfStatement: elseIf condition ELSEIF_NEWLINE=(NEWLINE | EOF) codeBlock?;
elseIf: WS* ELSEIF WS+;
elseStatement: else codeBlock?;
else: WS* ELSE NEWLINE;

whileStatement: WHILE WS* condition NEWLINE codeBlock? endWhile;
endWhile: WS* ENDWHILE;

doswitchStatement: DOSWITCH WS* condition NEWLINE codeBlock WS* ENDDO;

dorandStatement: DORAND WS* condition NEWLINE codeBlock WS* ENDDO;

condition: numericExpression;
numericExpression: evalExpression;
macro: escapedMacro | nonEscapedMacro;
escapedMacro: LESS_THAN '?' macroBody '?' MORE_THAN ;
nonEscapedMacro: LESS_THAN macroBody  MORE_THAN ;
macroBody: (firstMemberAccess | indexedMemberName);
call: firstMemberAccess;
assignment: firstMemberAccess assign argumentList?;
assign: WS* ASSIGN WS*;

memberAccess: firstMemberAccess | argumentAccess;
firstMemberAccess: evalCall | nativeMemberAccess | customMemberAccess;
evalCall: EVAL_FUNCTIONS WS* numericExpression; 
nativeMemberAccess: nativeFunctionName nativeArgumentList? chainedMemberAccess?;
nativeArgumentList: enclosedArgumentList | freeArgumentList;
argumentAccess: (evalExpression | quotedLiteralArgument | unquotedArgumentAccess) chainedMemberAccess?;
unquotedArgumentAccess: (SYMBOL | macro | binaryOperator | constantExpression | WS | '[' | ']' | '#' | ':' | '.'|  ',' | '?' | '!' | EQUAL)+? ;
customMemberAccess: memberName enclosedArgumentList? chainedMemberAccess?;
chainedMemberAccess: '.' memberAccess;

nativeFunctionName: SYSMESSAGE | RETURN | TIMER | CONSUME | EVENTS | TRIGGER | ARROWQUEST | DIALOG | EVAL_FUNCTIONS | SOUND | TRY | X | NEWITEM | EQUIP | NEWEQUIP
                | MENU | GO | INVIS | SHOW | DAMAGE | ECHO | XXC | XXI | MOVE | RESIZEPIC | TILEPIC | HTMLGUMP | PAGE | TEXTENTRY | TEXT | BUTTON
                | TARGET | TARGETG | SKILL | SFX | ACTION | ATTR | NUKE | NUKECHAR | COLOR | ANIM | SAY | RESCOUNT | RESTEST | SMSG | FIX;
memberName: (SYMBOL | macro)+?;
indexedMemberName: memberName '[' numericExpression ']';

// properties
propertyList: NEWLINE? propertyAssignment+;
propertyAssignment: WS* propertyName  ((WS* ASSIGN WS*) | WS+) propertyValue (NEWLINE | EOF);
propertyName: SYMBOL ('[' number ']')?;
propertyValue: unquotedLiteralArgument;

// trigger
triggerList: trigger*;
trigger: triggerHeader (NEWLINE | EOF) triggerBody?;
triggerHeader: TRIGGER_HEADER (('@' triggerName) | (number));
triggerName: nativeFunctionName | SYMBOL;
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
innerQuotedLiteralArgument: (macro | '\'' | '\\' | ';' | ~('"' | NEWLINE))*?;
unquotedLiteralArgument: (memberAccess | SYMBOL | macro | constantExpression | WS | '[' | ']' | '#' | ':' |  '.'|  ',' | '?' | '!' | '@' | assignment | EQUAL)+? ;
triggerArgument: '@' SYMBOL;

// eval expression
evalExpression: signedEvalOperand evalBinaryOperation* ;
signedEvalOperand: unaryOperator signedEvalOperand | evalOperand;
evalOperand: randomExpression | constantExpression | macroConstantExpression | evalSubExpression | macro | indexedMemberName | firstMemberAccessExpression | '#';
firstMemberAccessExpression: firstMemberAccess;
evalBinaryOperation: evalOperator signedEvalOperand ;
evalOperator: WS* (evalBinaryOperator | macroOperator) WS* ;
evalSubExpression: '(' LEFT_WS=WS* numericExpression RIGHT_WS=WS* ')' ;
evalBinaryOperator: binaryOperator | EQUAL | NOT_EQUAL | moreThanEqual | lessThanEqual | MORE_THAN | LESS_THAN;
binaryOperator: PLUS | MINUS | MULTIPLY | DIVIDE | MODULO | LOGICAL_AND | LOGICAL_OR | BITWISE_AND | BITWISE_OR;
moreThanEqual: MORE_THAN ASSIGN;
lessThanEqual: LESS_THAN ASSIGN;

macroConstantExpression: constantExpression macro;
constantExpression: DEC_NUMBER | HEX_NUMBER;
randomExpression: '{' (randomExpressionList | macro) '}';
randomExpressionList: STARTING_WS=WS* numericExpression randomExpressionElement (randomExpressionElement randomExpressionElement)* ENDING_WS=WS*;
randomExpressionElement: WS+ numericExpression;
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
BOOK_SECTION_HEADER_START: '[' [bB][oO][oO][kK] WS+;

IF: [iI][fF];
ELSEIF: [eE][lL][sS][eE] WS* [iI][fF]; 
ELSE: [eE][lL][sS][eE];
ENDIF: [eE][nN][dD][iI][fF];
WHILE: [wW][hH][iI][lL][eE];
ENDWHILE: [eE][nN][dD][wW][hH][iI][lL][eE];
DOSWITCH: [dD][oO][sS][wW][iI][tT][cC][hH];
DORAND: [dD][oO][rR][aA][nN][dD];
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
NEWEQUIP: [nN][eE][wW][eE][qQ][uU][iI][pP];
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
TARGET: [tT][aA][rR][gG][eE][tT];
TARGETG: [tT][aA][rR][gG][eE][tT][gG];
SKILL: [sS][kK][iI][lL][lL];
SFX: [sS][fF][xX];
ACTION: [aA][cC][tT][iI][oO][nN];
ATTR: [aA][tT][tT][rR];
NUKECHAR: [nN][uU][kK][eE][cC][hH][aA][rR];
NUKE: [nN][uU][kK][eE];
COLOR: [cC][oO][lL][oO][rR];
ANIM: [aA][nN][iI][mM];
SAY: [sS][aA][yY];
RESCOUNT: [rR][eE][sS][cC][oO][uU][nN][tT];
RESTEST: [rR][eE][sS][tT][eE][sS][tT];
SMSG: [sS][mM][sS][gG];
FIX: [fF][iI][xX];

EVAL_FUNCTIONS: EVAL | HVAL | SAFE;
EVAL: [eE][vV][aA][lL];
HVAL: [hH][vV][aA][lL];
SAFE: [sS][aA][fF][eE];

TRIGGER_HEADER: [oO][nN] '=';
SYMBOL: VALID_SYMBOL_START VALID_SYMBOL_CHAR*;
DEC_NUMBER: ('1' .. '9') DEC_DIGIT*  ('.' ('0'..'9')+)?;
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
