grammar sphereScript99;

file: NEWLINE? section+ (eofSection | EOF);
saveFile: NEWLINE? propertyList? saveFileSection+ (eofSection | EOF);

section: WS* (functionSection | itemDefSection | charDefSection | typeDefSection | typeDefsSection | templateSection
            | eventsSection | defNamesSection | dialogSection | dialogTextSection | dialogButtonSection
            | bookSection | bookPageSection | speechSection | commentSection | professionSection | spellSection
            | areaSection | regionTypeSection | regionResourceSection | namesSection | spawnSection) WS*;
saveFileSection: WS* (varNamesSection | worldCharSection | worldItemSection | sectorSection) WS*;
eofSection: EOF_SECTION_HEADER;

functionSection: functionSectionHeader codeBlock;
functionSectionHeader: FUNCTION_SECTION_HEADER_START functionName ']' NEWLINE;
functionName: SYMBOL | nativeFunctionName;

itemDefSection: itemDefSectionHeader propertyList? triggerList? ;
itemDefSectionHeader: ITEMDEF_SECTION_HEADER_START sectionName ']' NEWLINE;

charDefSection: charDefSectionHeader propertyList triggerList ;
charDefSectionHeader: CHARDEF_SECTION_HEADER_START sectionName ']' NEWLINE;

sectionName: SYMBOL | number;

professionSection: professionSectionHeader propertyList triggerList;
professionSectionHeader: PROFESSION_SECTION_HEADER_START professionSectionName ']' NEWLINE;
professionSectionName: number;

spellSection: spellSectionHeader propertyList triggerList?;
spellSectionHeader: SPELL_SECTION_HEADER_START spellSectionName ']' NEWLINE;
spellSectionName: number;

areaSection: areaSectionHeader propertyList;
areaSectionHeader: AREA_SECTION_HEADER_START areaSectionName ']' NEWLINE;
areaSectionName: ~(NEWITEM | ']')+;

regionTypeSection: regionTypeSectionHeader propertyList? triggerList?;
regionTypeSectionHeader: REGIONTYPE_SECTION_HEADER_START sectionName regionTypeSectionSubName? ']' NEWLINE;
regionTypeSectionSubName: WS+ secondName=sectionName;

regionResourceSection: regionResourceSectionHeader propertyList;
regionResourceSectionHeader: REGIONRESOURCE_SECTION_HEADER_START sectionName ']' NEWLINE;

typeDefSection: typeDefSectionHeader triggerList ;
typeDefSectionHeader: TYPEDEF_SECTION_HEADER_START SYMBOL ']' NEWLINE;

typeDefsSection: typeDefsSectionHeader propertyList ;
typeDefsSectionHeader: TYPEDEFS_SECTION_HEADER_START ']' NEWLINE;

templateSection: templateSectionHeader propertyList ;
templateSectionHeader: TEMPLATE_SECTION_HEADER_START sectionName ']' NEWLINE;

eventsSection: eventsSectionHeader triggerList ;
eventsSectionHeader: EVENTS_SECTION_HEADER_START SYMBOL ']' NEWLINE;

defNamesSection: defNamesSectionHeader propertyList;
defNamesSectionHeader: DEFNAMES_SECTION_HEADER_START defNameSectionName ']' NEWLINE;
defNameSectionName: ~(NEWITEM | ']')+;

dialogSection: dialogSectionHeader dialogPosition? codeBlock;
dialogSectionHeader: DIALOG_SECTION_HEADER_START dialogName=SYMBOL ']' NEWLINE;
dialogPosition: number WS* ',' WS* number NEWLINE;

dialogTextSection: dialogTextSectionHeader dialogTextSectionLine*?;
dialogTextSectionHeader: DIALOG_SECTION_HEADER_START dialogName=SYMBOL WS+ TEXT ']' NEWLINE;
dialogTextSectionLine: freeTextLine;

dialogButtonSection: dialogButtonSectionHeader dialogButtonTriggerList;
dialogButtonSectionHeader: DIALOG_SECTION_HEADER_START dialogName=SYMBOL WS+ BUTTON ']' NEWLINE;
dialogButtonTriggerList: dialogButtonTrigger*;
dialogButtonTrigger: dialogButtonTriggerHeader triggerBody?;
dialogButtonTriggerHeader: HEADER=(TRIGGER_HEADER | BUTTON_TRIGGER_HEADER) dialogButtonTriggerName NEWLINE;
dialogButtonTriggerName: number | ANY_BUTTON;

bookPageSection: bookPageSectionHeader pageLine*?;
bookPageSectionHeader: BOOK_SECTION_HEADER_START bookName=SYMBOL WS+ pageNumber=DEC_NUMBER ']' NEWLINE;
pageLine: freeTextLine;

bookSection: bookSectionHeader propertyList;
bookSectionHeader: BOOK_SECTION_HEADER_START bookName=SYMBOL ']' NEWLINE;

speechSection: speechSectionHeader speechTriggerList;
speechSectionHeader: SPEECH_SECTION_HEADER_START speechName=SYMBOL ']' NEWLINE;
speechTriggerList: speechTrigger+;
speechTrigger: speechTriggerHeader+ triggerBody;
speechTriggerHeader: TRIGGER_HEADER speechTriggerName NEWLINE;
speechTriggerName: ~(NEWLINE)+;

commentSection: commentSectionHeader commentLines*?;
commentSectionHeader: COMMENT_SECTION_HEADER_START WS? commentSectionName? ']' NEWLINE;
commentSectionName: ~(NEWITEM | ']')+;
commentLines: freeTextLine;

varNamesSection: varNamesSectionHeader propertyList;
varNamesSectionHeader: VARNAMES_SECTION_HEADER_START ']' NEWLINE;

worldItemSection: worldItemSectionHeader propertyList;
worldItemSectionHeader: WORLDITEM_SECTION_HEADER_START sectionName ']' NEWLINE;

worldCharSection: worldCharSectionHeader propertyList;
worldCharSectionHeader: WORLDCHAR_SECTION_HEADER_START sectionName ']' NEWLINE;

namesSection: namesSectionHeader namesCount freeTextLine+;
namesSectionHeader: NAMES_SECTION_HEADER_START sectionName ']' NEWLINE;
namesCount: number NEWLINE;

spawnSection: spawnSectionHeader propertyList;
spawnSectionHeader: SPAWN_SECTION_HEADER_START sectionName ']' NEWLINE;

sectorSection: sectorSectionHeader propertyList;
sectorSectionHeader: SECTOR_SECTION_HEADER_START sectorName ']' NEWLINE;
sectorName: ~(NEWITEM | ']')+;

freeTextLine: ~(FUNCTION_SECTION_HEADER_START | ITEMDEF_SECTION_HEADER_START | CHARDEF_SECTION_HEADER_START
    | TYPEDEF_SECTION_HEADER_START | TEMPLATE_SECTION_HEADER_START | EVENTS_SECTION_HEADER_START | DEFNAMES_SECTION_HEADER_START
    | DIALOG_SECTION_HEADER_START | BOOK_SECTION_HEADER_START | SPEECH_SECTION_HEADER_START | COMMENT_SECTION_HEADER_START
    | TYPEDEFS_SECTION_HEADER_START | NEWLINE)* NEWLINE;

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

doswitchStatement: DOSWITCH BEFORE_CONDITION_WS=WS* condition NEWLINE codeBlock AFTER_BLOCK_WS=WS* ENDDO;

dorandStatement: DORAND WS* condition NEWLINE codeBlock WS* ENDDO;

condition: numericExpression;
numericExpression: evalExpression;
macro: escapedMacro | nonEscapedMacro;
escapedMacro: LESS_THAN '?' LEFT_WS=WS* macroBody RIGHT_WS=WS* '?' MORE_THAN ;
nonEscapedMacro: LESS_THAN macroBody  MORE_THAN ;
macroBody: (firstMemberAccess | indexedMemberName);
call: firstMemberAccess;
assignment: firstMemberAccess assign argumentList?;
assign: WS* ASSIGN WS*;

memberAccess: firstMemberAccess | argumentAccess;
firstMemberAccess: evalCall | genericNativeMemberAccess | customMemberAccess;
evalCall: EVAL_FUNCTIONS WS* numericExpression; 
genericNativeMemberAccess: actionMemberAccess | nativeMemberAccess;
nativeMemberAccess: nativeFunctionName nativeArgumentList? chainedMemberAccess?;
nativeArgumentList: enclosedArgumentList | freeArgumentList;
argumentAccess: (evalExpression | quotedLiteralArgument | unquotedArgumentAccess) chainedMemberAccess?;
unquotedArgumentAccess: (SYMBOL | macro | binaryOperator | constantExpression | WS | '[' | ']' | '#' | ':' | '.'|  ',' | '?' | '!' | EQUAL)+? ;
customMemberAccess: memberName enclosedArgumentList? chainedMemberAccess?;
chainedMemberAccess: '.' memberAccess;

nativeFunctionName: SYSMESSAGE | RETURN | TIMER | CONSUME | EVENTS | TRIGGER | ARROWQUEST | DIALOG | EVAL_FUNCTIONS | SOUND | TRY | X | NEWITEM | EQUIP | NEWEQUIP
                | MENU | GO | INVIS | SHOW | DAMAGE | ECHO | XXC | XXI | MOVE | RESIZEPIC | GUMPPIC | TILEPIC | HTMLGUMP | PAGE | TEXTENTRY | TEXT | BUTTON
                | TARGET | TARGETG | SKILL | SFX | ATTR | NUKE | NUKECHAR | COLOR | ANIM | SAY | RESCOUNT | RESTEST | SMSG | FIX | INPDLG | SAFE
                | ISEVENT | SPELLEFFECT | ADDSPELL | NEWNPC | EMOTE | SEX | BANK;
actionMemberAccess: ACTION (enclosedArgumentList | actionNativeArgument)?;
actionNativeArgument: WS+ evalExpression;
memberName: (SYMBOL | macro | TAG | REGION)+?;
indexedMemberName: memberName '[' numericExpression ']';

// properties
propertyList: NEWLINE? propertyAssignment+;
propertyAssignment: LEADING_WS=WS* propertyName propertyAssignmentOperator? propertyValue? (NEWLINE | EOF);
propertyAssignmentOperator: ((WS* ASSIGN WS*) | WS+);
propertyName: propertyNameText propertyNameIndex?;
propertyNameText: (propertyTagName | propertyRegionName | nativeFunctionName | SYMBOL | ACTION);
propertyTagName: TAG '.' (nativeFunctionName | SYMBOL | ACTION);
propertyRegionName: REGION '.' (nativeFunctionName | SYMBOL | ACTION);
propertyNameIndex: ('[' number ']');
propertyValue: ~(NEWLINE)+?;

// trigger
triggerList: trigger*;
trigger: triggerHeader (NEWLINE | EOF) triggerBody?;
triggerHeader: TRIGGER_HEADER ('@' triggerName);
triggerName: nativeFunctionName | SYMBOL;
triggerBody: codeBlock;

// argument, argument expression
enclosedArgumentList: LPAREN argumentList? RPAREN;
freeArgumentList: firstFreeArgument (',' argument)*;
firstFreeArgument: firstFreeArgumentOptionalWhiteSpace | firstFreeArgumentMandatoryWhiteSpace;
firstFreeArgumentOptionalWhiteSpace: WS* (triggerArgument | evalExpression | quotedLiteralArgument);
firstFreeArgumentMandatoryWhiteSpace: WS+ (assignmentArgument | unquotedLiteralArgument);
argumentList: argument (',' WS* argument)*;
argument: triggerArgument | evalExpression | quotedLiteralArgument | assignmentArgument | unquotedLiteralArgument | emptyArgument;
emptyArgument: ;
triggerArgument: '@' SYMBOL;
assignmentArgument: assignment;
quotedLiteralArgument: '"' innerQuotedLiteralArgument '"';
innerQuotedLiteralArgument: (quotedTextLiteralSegment | macroLiteralSegment | lessThanSegment)*;
lessThanSegment: LESS_THAN;
quotedTextLiteralSegment: ~('"' | NEWLINE | LESS_THAN)+; 
unquotedLiteralArgument: (textLiteralSegment | macroLiteralSegment)+;
textLiteralSegment: ~(',' | NEWLINE | LESS_THAN)+;
macroLiteralSegment: macro;

// eval expression
evalExpression: signedEvalOperand evalBinaryOperation*? ;
signedEvalOperand: unaryOperator signedEvalOperand | evalOperand;
evalOperand: randomExpression | constantExpression | macroConstantExpression | evalSubExpression | macro | indexedMemberName | firstMemberAccessExpression | '#';
firstMemberAccessExpression: firstMemberAccess;
evalBinaryOperation: evalOperator signedEvalOperand ;
evalOperator: LEADING_WS=WS* evalBinaryOperator TRAILING_WS=WS* ;
evalSubExpression: '(' LEFT_WS=WS* numericExpression RIGHT_WS=WS* ')' ;
evalBinaryOperator: binaryOperator | EQUAL | NOT_EQUAL | moreThanEqual | lessThanEqual | MORE_THAN | LESS_THAN | rightBitShiftOperator;
binaryOperator: PLUS | MINUS | MULTIPLY | DIVIDE | MODULO | LOGICAL_AND | LOGICAL_OR | BITWISE_AND | BITWISE_OR;
moreThanEqual: MORE_THAN ASSIGN;
lessThanEqual: LESS_THAN ASSIGN;
rightBitShiftOperator: LEADING_WS=WS+ MORE_THAN MORE_THAN TRAILING_WS=WS+;

macroConstantExpression: constantExpression macro;
constantExpression: number;
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
TYPEDEFS_SECTION_HEADER_START: '[' [tT][yY][pP][eE][dD][eE][fF][sS];
TEMPLATE_SECTION_HEADER_START: '[' [tT][eE][mM][pP][lL][aA][tT][eE] WS+;
EVENTS_SECTION_HEADER_START: '[' [eE][vV][eE][nN][tT][sS] WS+;
DEFNAMES_SECTION_HEADER_START: '[' [dD][eE][fF][nN][aA][mM][eE][sS] WS+;
DIALOG_SECTION_HEADER_START: '[' [dD][iI][aA][lL][oO][gG] WS+;
BOOK_SECTION_HEADER_START: '[' [bB][oO][oO][kK] WS+;
SPEECH_SECTION_HEADER_START: '[' [sS][pP][eE][eE][cC][hH] WS+;
COMMENT_SECTION_HEADER_START: '[' [cC][oO][mM][mM][eE][nN][tT];
PROFESSION_SECTION_HEADER_START: '[' [pP][rR][oO][fF][eE][sS][sS][iI][oO][nN] WS+;
SPELL_SECTION_HEADER_START: '[' [sS][pP][eE][lL][lL] WS+;
AREA_SECTION_HEADER_START: '[' [aA][rR][eE][aA] WS+;
REGIONTYPE_SECTION_HEADER_START: '[' [rR][eE][gG][iI][oO][nN][tT][yY][pP][eE] WS+;
REGIONRESOURCE_SECTION_HEADER_START: '[' [rR][eE][gG][iI][oO][nN][rR][eE][sS][oO][uU][rR][cC][eE] WS+;
VARNAMES_SECTION_HEADER_START: '[' [vV][aA][rR][nN][aA][mM][eE][sS];
WORLDITEM_SECTION_HEADER_START: '[' [wW][oO][rR][lL][dD][iI][tT][eE][mM] WS+;
WORLDCHAR_SECTION_HEADER_START: '[' [wW][oO][rR][lL][dD][cC][hH][aA][rR] WS+;
SECTOR_SECTION_HEADER_START: '[' [sS][eE][cC][tT][oO][rR] WS+;
NAMES_SECTION_HEADER_START: '[' [nN][aA][mM][eE][sS]  WS+;
SPAWN_SECTION_HEADER_START: '[' [sS][pP][aA][wW][nN] WS+;

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
GUMPPIC: [gG][uU][mM][pP][pP][iI][cC];
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
INPDLG: [iI][nN][pP][dD][lL][gG];
SAFE: [sS][aA][fF][eE];
ISEVENT: [iI][sS][eE][vV][eE][nN][tT];
SPELLEFFECT: [sS][pP][eE][lL][lL][eE][fF][fF][eE][cC][tT];
ADDSPELL: [aA][dD][dD][sS][pP][eE][lL][lL];
NEWNPC: [nN][eE][wW][nN][pP][cC];
EMOTE: [eE][mM][oO][tT][eE];
SEX: [sS][eE][xX];
REGION: [rR][eE][gG][iI][oO][nN];
BANK: [bB][aA][nN][kK];

EVAL_FUNCTIONS: EVAL | HVAL;
EVAL: [eE][vV][aA][lL];
HVAL: [hH][vV][aA][lL];

TAG: [tT][aA][gG];

TRIGGER_HEADER: [oO][nN] WS* '=' WS*;
BUTTON_TRIGGER_HEADER: [oO][nN][bB][uU][tT][tT][oO][nN] '=';
ANY_BUTTON: '@' [aA][nN][yY][bB][uU][tT][tT][oO][nN];
SYMBOL: VALID_SYMBOL_START VALID_SYMBOL_CHAR*;
DEC_NUMBER: ('1' .. '9') DEC_DIGIT*  ('.' ('0'..'9')+)?;
HEX_NUMBER: '#'? '0' HEX_DIGIT* ;

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
SQ: '\'';
BACKSLASH: '\\';
SEMICOLON: ';';

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
