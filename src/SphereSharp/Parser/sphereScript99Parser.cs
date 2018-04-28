//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /mnt/c/Users/jakub/sources/ultima/SphereSharp/SphereSharp/src/SphereSharp/sphereScript99.g4 by ANTLR 4.7

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7")]
[System.CLSCompliant(false)]
public partial class sphereScript99Parser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, SYMBOL=8, NUMBER=9, 
		PLUS=10, MINUS=11, LPAREN=12, RPAREN=13;
	public const int
		RULE_macro = 0, RULE_call = 1, RULE_argumentList = 2, RULE_argument = 3, 
		RULE_expressionArgument = 4, RULE_unquotedLiteralArgument = 5, RULE_signedOperand = 6, 
		RULE_operand = 7, RULE_binaryOperation = 8, RULE_operator = 9, RULE_constantExpression = 10, 
		RULE_macroExpression = 11, RULE_subExpression = 12, RULE_macroOperator = 13, 
		RULE_constantOperator = 14;
	public static readonly string[] ruleNames = {
		"macro", "call", "argumentList", "argument", "expressionArgument", "unquotedLiteralArgument", 
		"signedOperand", "operand", "binaryOperation", "operator", "constantExpression", 
		"macroExpression", "subExpression", "macroOperator", "constantOperator"
	};

	private static readonly string[] _LiteralNames = {
		null, "'<'", "'>'", "','", "' '", "'['", "']'", "'*'", null, null, "'+'", 
		"'-'", "'('", "')'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, "SYMBOL", "NUMBER", "PLUS", 
		"MINUS", "LPAREN", "RPAREN"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "sphereScript99.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static sphereScript99Parser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public sphereScript99Parser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public sphereScript99Parser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}
	public partial class MacroContext : ParserRuleContext {
		public CallContext call() {
			return GetRuleContext<CallContext>(0);
		}
		public MacroContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_macro; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterMacro(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitMacro(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMacro(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public MacroContext macro() {
		MacroContext _localctx = new MacroContext(Context, State);
		EnterRule(_localctx, 0, RULE_macro);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 30; Match(T__0);
			State = 31; call();
			State = 32; Match(T__1);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class CallContext : ParserRuleContext {
		public ITerminalNode SYMBOL() { return GetToken(sphereScript99Parser.SYMBOL, 0); }
		public ITerminalNode LPAREN() { return GetToken(sphereScript99Parser.LPAREN, 0); }
		public ArgumentListContext argumentList() {
			return GetRuleContext<ArgumentListContext>(0);
		}
		public ITerminalNode RPAREN() { return GetToken(sphereScript99Parser.RPAREN, 0); }
		public CallContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_call; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterCall(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitCall(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCall(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public CallContext call() {
		CallContext _localctx = new CallContext(Context, State);
		EnterRule(_localctx, 2, RULE_call);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 34; Match(SYMBOL);
			State = 35; Match(LPAREN);
			State = 36; argumentList();
			State = 37; Match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ArgumentListContext : ParserRuleContext {
		public ArgumentContext[] argument() {
			return GetRuleContexts<ArgumentContext>();
		}
		public ArgumentContext argument(int i) {
			return GetRuleContext<ArgumentContext>(i);
		}
		public ArgumentListContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_argumentList; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterArgumentList(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitArgumentList(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitArgumentList(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ArgumentListContext argumentList() {
		ArgumentListContext _localctx = new ArgumentListContext(Context, State);
		EnterRule(_localctx, 4, RULE_argumentList);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 39; argument();
			State = 44;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==T__2) {
				{
				{
				State = 40; Match(T__2);
				State = 41; argument();
				}
				}
				State = 46;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ArgumentContext : ParserRuleContext {
		public ExpressionArgumentContext expressionArgument() {
			return GetRuleContext<ExpressionArgumentContext>(0);
		}
		public UnquotedLiteralArgumentContext unquotedLiteralArgument() {
			return GetRuleContext<UnquotedLiteralArgumentContext>(0);
		}
		public ArgumentContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_argument; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterArgument(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitArgument(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitArgument(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ArgumentContext argument() {
		ArgumentContext _localctx = new ArgumentContext(Context, State);
		EnterRule(_localctx, 6, RULE_argument);
		try {
			State = 49;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,1,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 47; expressionArgument();
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 48; unquotedLiteralArgument();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExpressionArgumentContext : ParserRuleContext {
		public SignedOperandContext signedOperand() {
			return GetRuleContext<SignedOperandContext>(0);
		}
		public BinaryOperationContext[] binaryOperation() {
			return GetRuleContexts<BinaryOperationContext>();
		}
		public BinaryOperationContext binaryOperation(int i) {
			return GetRuleContext<BinaryOperationContext>(i);
		}
		public ExpressionArgumentContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expressionArgument; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterExpressionArgument(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitExpressionArgument(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExpressionArgument(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExpressionArgumentContext expressionArgument() {
		ExpressionArgumentContext _localctx = new ExpressionArgumentContext(Context, State);
		EnterRule(_localctx, 8, RULE_expressionArgument);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 51; signedOperand();
			State = 55;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__6) | (1L << PLUS) | (1L << MINUS))) != 0)) {
				{
				{
				State = 52; binaryOperation();
				}
				}
				State = 57;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class UnquotedLiteralArgumentContext : ParserRuleContext {
		public CallContext[] call() {
			return GetRuleContexts<CallContext>();
		}
		public CallContext call(int i) {
			return GetRuleContext<CallContext>(i);
		}
		public ITerminalNode[] SYMBOL() { return GetTokens(sphereScript99Parser.SYMBOL); }
		public ITerminalNode SYMBOL(int i) {
			return GetToken(sphereScript99Parser.SYMBOL, i);
		}
		public MacroContext[] macro() {
			return GetRuleContexts<MacroContext>();
		}
		public MacroContext macro(int i) {
			return GetRuleContext<MacroContext>(i);
		}
		public OperatorContext[] @operator() {
			return GetRuleContexts<OperatorContext>();
		}
		public OperatorContext @operator(int i) {
			return GetRuleContext<OperatorContext>(i);
		}
		public ITerminalNode[] NUMBER() { return GetTokens(sphereScript99Parser.NUMBER); }
		public ITerminalNode NUMBER(int i) {
			return GetToken(sphereScript99Parser.NUMBER, i);
		}
		public UnquotedLiteralArgumentContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_unquotedLiteralArgument; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterUnquotedLiteralArgument(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitUnquotedLiteralArgument(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitUnquotedLiteralArgument(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public UnquotedLiteralArgumentContext unquotedLiteralArgument() {
		UnquotedLiteralArgumentContext _localctx = new UnquotedLiteralArgumentContext(Context, State);
		EnterRule(_localctx, 10, RULE_unquotedLiteralArgument);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 66;
			ErrorHandler.Sync(this);
			_alt = 1+1;
			do {
				switch (_alt) {
				case 1+1:
					{
					State = 66;
					ErrorHandler.Sync(this);
					switch ( Interpreter.AdaptivePredict(TokenStream,3,Context) ) {
					case 1:
						{
						State = 58; call();
						}
						break;
					case 2:
						{
						State = 59; Match(SYMBOL);
						}
						break;
					case 3:
						{
						State = 60; macro();
						}
						break;
					case 4:
						{
						State = 61; @operator();
						}
						break;
					case 5:
						{
						State = 62; Match(NUMBER);
						}
						break;
					case 6:
						{
						State = 63; Match(T__3);
						}
						break;
					case 7:
						{
						State = 64; Match(T__4);
						}
						break;
					case 8:
						{
						State = 65; Match(T__5);
						}
						break;
					}
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				State = 68;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,4,Context);
			} while ( _alt!=1 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class SignedOperandContext : ParserRuleContext {
		public SignedOperandContext signedOperand() {
			return GetRuleContext<SignedOperandContext>(0);
		}
		public ITerminalNode MINUS() { return GetToken(sphereScript99Parser.MINUS, 0); }
		public ITerminalNode PLUS() { return GetToken(sphereScript99Parser.PLUS, 0); }
		public OperandContext operand() {
			return GetRuleContext<OperandContext>(0);
		}
		public SignedOperandContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_signedOperand; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterSignedOperand(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitSignedOperand(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSignedOperand(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public SignedOperandContext signedOperand() {
		SignedOperandContext _localctx = new SignedOperandContext(Context, State);
		EnterRule(_localctx, 12, RULE_signedOperand);
		int _la;
		try {
			State = 73;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case PLUS:
			case MINUS:
				EnterOuterAlt(_localctx, 1);
				{
				State = 70;
				_la = TokenStream.LA(1);
				if ( !(_la==PLUS || _la==MINUS) ) {
				ErrorHandler.RecoverInline(this);
				}
				else {
					ErrorHandler.ReportMatch(this);
				    Consume();
				}
				State = 71; signedOperand();
				}
				break;
			case T__0:
			case NUMBER:
			case LPAREN:
				EnterOuterAlt(_localctx, 2);
				{
				State = 72; operand();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class OperandContext : ParserRuleContext {
		public ConstantExpressionContext constantExpression() {
			return GetRuleContext<ConstantExpressionContext>(0);
		}
		public SubExpressionContext subExpression() {
			return GetRuleContext<SubExpressionContext>(0);
		}
		public MacroExpressionContext macroExpression() {
			return GetRuleContext<MacroExpressionContext>(0);
		}
		public OperandContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_operand; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterOperand(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitOperand(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitOperand(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public OperandContext operand() {
		OperandContext _localctx = new OperandContext(Context, State);
		EnterRule(_localctx, 14, RULE_operand);
		try {
			State = 78;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case NUMBER:
				EnterOuterAlt(_localctx, 1);
				{
				State = 75; constantExpression();
				}
				break;
			case LPAREN:
				EnterOuterAlt(_localctx, 2);
				{
				State = 76; subExpression();
				}
				break;
			case T__0:
				EnterOuterAlt(_localctx, 3);
				{
				State = 77; macroExpression();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class BinaryOperationContext : ParserRuleContext {
		public OperatorContext @operator() {
			return GetRuleContext<OperatorContext>(0);
		}
		public SignedOperandContext signedOperand() {
			return GetRuleContext<SignedOperandContext>(0);
		}
		public BinaryOperationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_binaryOperation; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterBinaryOperation(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitBinaryOperation(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitBinaryOperation(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public BinaryOperationContext binaryOperation() {
		BinaryOperationContext _localctx = new BinaryOperationContext(Context, State);
		EnterRule(_localctx, 16, RULE_binaryOperation);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 80; @operator();
			State = 81; signedOperand();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class OperatorContext : ParserRuleContext {
		public ConstantOperatorContext constantOperator() {
			return GetRuleContext<ConstantOperatorContext>(0);
		}
		public MacroOperatorContext macroOperator() {
			return GetRuleContext<MacroOperatorContext>(0);
		}
		public OperatorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_operator; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterOperator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitOperator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitOperator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public OperatorContext @operator() {
		OperatorContext _localctx = new OperatorContext(Context, State);
		EnterRule(_localctx, 18, RULE_operator);
		try {
			State = 85;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case T__6:
			case PLUS:
			case MINUS:
				EnterOuterAlt(_localctx, 1);
				{
				State = 83; constantOperator();
				}
				break;
			case T__0:
				EnterOuterAlt(_localctx, 2);
				{
				State = 84; macroOperator();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ConstantExpressionContext : ParserRuleContext {
		public ITerminalNode NUMBER() { return GetToken(sphereScript99Parser.NUMBER, 0); }
		public ConstantExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_constantExpression; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterConstantExpression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitConstantExpression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitConstantExpression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ConstantExpressionContext constantExpression() {
		ConstantExpressionContext _localctx = new ConstantExpressionContext(Context, State);
		EnterRule(_localctx, 20, RULE_constantExpression);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 87; Match(NUMBER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MacroExpressionContext : ParserRuleContext {
		public MacroContext macro() {
			return GetRuleContext<MacroContext>(0);
		}
		public MacroExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_macroExpression; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterMacroExpression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitMacroExpression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMacroExpression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public MacroExpressionContext macroExpression() {
		MacroExpressionContext _localctx = new MacroExpressionContext(Context, State);
		EnterRule(_localctx, 22, RULE_macroExpression);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 89; macro();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class SubExpressionContext : ParserRuleContext {
		public ExpressionArgumentContext expressionArgument() {
			return GetRuleContext<ExpressionArgumentContext>(0);
		}
		public SubExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_subExpression; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterSubExpression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitSubExpression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSubExpression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public SubExpressionContext subExpression() {
		SubExpressionContext _localctx = new SubExpressionContext(Context, State);
		EnterRule(_localctx, 24, RULE_subExpression);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 91; Match(LPAREN);
			State = 92; expressionArgument();
			State = 93; Match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MacroOperatorContext : ParserRuleContext {
		public MacroContext macro() {
			return GetRuleContext<MacroContext>(0);
		}
		public MacroOperatorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_macroOperator; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterMacroOperator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitMacroOperator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMacroOperator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public MacroOperatorContext macroOperator() {
		MacroOperatorContext _localctx = new MacroOperatorContext(Context, State);
		EnterRule(_localctx, 26, RULE_macroOperator);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 95; macro();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ConstantOperatorContext : ParserRuleContext {
		public ITerminalNode PLUS() { return GetToken(sphereScript99Parser.PLUS, 0); }
		public ITerminalNode MINUS() { return GetToken(sphereScript99Parser.MINUS, 0); }
		public ConstantOperatorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_constantOperator; } }
		public override void EnterRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.EnterConstantOperator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IsphereScript99Listener typedListener = listener as IsphereScript99Listener;
			if (typedListener != null) typedListener.ExitConstantOperator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IsphereScript99Visitor<TResult> typedVisitor = visitor as IsphereScript99Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitConstantOperator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ConstantOperatorContext constantOperator() {
		ConstantOperatorContext _localctx = new ConstantOperatorContext(Context, State);
		EnterRule(_localctx, 28, RULE_constantOperator);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 97;
			_la = TokenStream.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__6) | (1L << PLUS) | (1L << MINUS))) != 0)) ) {
			ErrorHandler.RecoverInline(this);
			}
			else {
				ErrorHandler.ReportMatch(this);
			    Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\xF', '\x66', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', '\x5', '\x4', 
		'\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', '\t', '\b', 
		'\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', '\t', '\v', 
		'\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', '\t', 
		'\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x3', 
		'\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\a', '\x4', '-', '\n', '\x4', '\f', '\x4', '\xE', 
		'\x4', '\x30', '\v', '\x4', '\x3', '\x5', '\x3', '\x5', '\x5', '\x5', 
		'\x34', '\n', '\x5', '\x3', '\x6', '\x3', '\x6', '\a', '\x6', '\x38', 
		'\n', '\x6', '\f', '\x6', '\xE', '\x6', ';', '\v', '\x6', '\x3', '\a', 
		'\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', 
		'\a', '\x3', '\a', '\x6', '\a', '\x45', '\n', '\a', '\r', '\a', '\xE', 
		'\a', '\x46', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x5', '\b', 'L', 
		'\n', '\b', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x5', '\t', 'Q', '\n', 
		'\t', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\v', '\x3', '\v', 
		'\x5', '\v', 'X', '\n', '\v', '\x3', '\f', '\x3', '\f', '\x3', '\r', '\x3', 
		'\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', 
		'\x3', '\xF', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', '\x46', 
		'\x2', '\x11', '\x2', '\x4', '\x6', '\b', '\n', '\f', '\xE', '\x10', '\x12', 
		'\x14', '\x16', '\x18', '\x1A', '\x1C', '\x1E', '\x2', '\x4', '\x3', '\x2', 
		'\f', '\r', '\x4', '\x2', '\t', '\t', '\f', '\r', '\x2', '\x65', '\x2', 
		' ', '\x3', '\x2', '\x2', '\x2', '\x4', '$', '\x3', '\x2', '\x2', '\x2', 
		'\x6', ')', '\x3', '\x2', '\x2', '\x2', '\b', '\x33', '\x3', '\x2', '\x2', 
		'\x2', '\n', '\x35', '\x3', '\x2', '\x2', '\x2', '\f', '\x44', '\x3', 
		'\x2', '\x2', '\x2', '\xE', 'K', '\x3', '\x2', '\x2', '\x2', '\x10', 'P', 
		'\x3', '\x2', '\x2', '\x2', '\x12', 'R', '\x3', '\x2', '\x2', '\x2', '\x14', 
		'W', '\x3', '\x2', '\x2', '\x2', '\x16', 'Y', '\x3', '\x2', '\x2', '\x2', 
		'\x18', '[', '\x3', '\x2', '\x2', '\x2', '\x1A', ']', '\x3', '\x2', '\x2', 
		'\x2', '\x1C', '\x61', '\x3', '\x2', '\x2', '\x2', '\x1E', '\x63', '\x3', 
		'\x2', '\x2', '\x2', ' ', '!', '\a', '\x3', '\x2', '\x2', '!', '\"', '\x5', 
		'\x4', '\x3', '\x2', '\"', '#', '\a', '\x4', '\x2', '\x2', '#', '\x3', 
		'\x3', '\x2', '\x2', '\x2', '$', '%', '\a', '\n', '\x2', '\x2', '%', '&', 
		'\a', '\xE', '\x2', '\x2', '&', '\'', '\x5', '\x6', '\x4', '\x2', '\'', 
		'(', '\a', '\xF', '\x2', '\x2', '(', '\x5', '\x3', '\x2', '\x2', '\x2', 
		')', '.', '\x5', '\b', '\x5', '\x2', '*', '+', '\a', '\x5', '\x2', '\x2', 
		'+', '-', '\x5', '\b', '\x5', '\x2', ',', '*', '\x3', '\x2', '\x2', '\x2', 
		'-', '\x30', '\x3', '\x2', '\x2', '\x2', '.', ',', '\x3', '\x2', '\x2', 
		'\x2', '.', '/', '\x3', '\x2', '\x2', '\x2', '/', '\a', '\x3', '\x2', 
		'\x2', '\x2', '\x30', '.', '\x3', '\x2', '\x2', '\x2', '\x31', '\x34', 
		'\x5', '\n', '\x6', '\x2', '\x32', '\x34', '\x5', '\f', '\a', '\x2', '\x33', 
		'\x31', '\x3', '\x2', '\x2', '\x2', '\x33', '\x32', '\x3', '\x2', '\x2', 
		'\x2', '\x34', '\t', '\x3', '\x2', '\x2', '\x2', '\x35', '\x39', '\x5', 
		'\xE', '\b', '\x2', '\x36', '\x38', '\x5', '\x12', '\n', '\x2', '\x37', 
		'\x36', '\x3', '\x2', '\x2', '\x2', '\x38', ';', '\x3', '\x2', '\x2', 
		'\x2', '\x39', '\x37', '\x3', '\x2', '\x2', '\x2', '\x39', ':', '\x3', 
		'\x2', '\x2', '\x2', ':', '\v', '\x3', '\x2', '\x2', '\x2', ';', '\x39', 
		'\x3', '\x2', '\x2', '\x2', '<', '\x45', '\x5', '\x4', '\x3', '\x2', '=', 
		'\x45', '\a', '\n', '\x2', '\x2', '>', '\x45', '\x5', '\x2', '\x2', '\x2', 
		'?', '\x45', '\x5', '\x14', '\v', '\x2', '@', '\x45', '\a', '\v', '\x2', 
		'\x2', '\x41', '\x45', '\a', '\x6', '\x2', '\x2', '\x42', '\x45', '\a', 
		'\a', '\x2', '\x2', '\x43', '\x45', '\a', '\b', '\x2', '\x2', '\x44', 
		'<', '\x3', '\x2', '\x2', '\x2', '\x44', '=', '\x3', '\x2', '\x2', '\x2', 
		'\x44', '>', '\x3', '\x2', '\x2', '\x2', '\x44', '?', '\x3', '\x2', '\x2', 
		'\x2', '\x44', '@', '\x3', '\x2', '\x2', '\x2', '\x44', '\x41', '\x3', 
		'\x2', '\x2', '\x2', '\x44', '\x42', '\x3', '\x2', '\x2', '\x2', '\x44', 
		'\x43', '\x3', '\x2', '\x2', '\x2', '\x45', '\x46', '\x3', '\x2', '\x2', 
		'\x2', '\x46', 'G', '\x3', '\x2', '\x2', '\x2', '\x46', '\x44', '\x3', 
		'\x2', '\x2', '\x2', 'G', '\r', '\x3', '\x2', '\x2', '\x2', 'H', 'I', 
		'\t', '\x2', '\x2', '\x2', 'I', 'L', '\x5', '\xE', '\b', '\x2', 'J', 'L', 
		'\x5', '\x10', '\t', '\x2', 'K', 'H', '\x3', '\x2', '\x2', '\x2', 'K', 
		'J', '\x3', '\x2', '\x2', '\x2', 'L', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'M', 'Q', '\x5', '\x16', '\f', '\x2', 'N', 'Q', '\x5', '\x1A', '\xE', 
		'\x2', 'O', 'Q', '\x5', '\x18', '\r', '\x2', 'P', 'M', '\x3', '\x2', '\x2', 
		'\x2', 'P', 'N', '\x3', '\x2', '\x2', '\x2', 'P', 'O', '\x3', '\x2', '\x2', 
		'\x2', 'Q', '\x11', '\x3', '\x2', '\x2', '\x2', 'R', 'S', '\x5', '\x14', 
		'\v', '\x2', 'S', 'T', '\x5', '\xE', '\b', '\x2', 'T', '\x13', '\x3', 
		'\x2', '\x2', '\x2', 'U', 'X', '\x5', '\x1E', '\x10', '\x2', 'V', 'X', 
		'\x5', '\x1C', '\xF', '\x2', 'W', 'U', '\x3', '\x2', '\x2', '\x2', 'W', 
		'V', '\x3', '\x2', '\x2', '\x2', 'X', '\x15', '\x3', '\x2', '\x2', '\x2', 
		'Y', 'Z', '\a', '\v', '\x2', '\x2', 'Z', '\x17', '\x3', '\x2', '\x2', 
		'\x2', '[', '\\', '\x5', '\x2', '\x2', '\x2', '\\', '\x19', '\x3', '\x2', 
		'\x2', '\x2', ']', '^', '\a', '\xE', '\x2', '\x2', '^', '_', '\x5', '\n', 
		'\x6', '\x2', '_', '`', '\a', '\xF', '\x2', '\x2', '`', '\x1B', '\x3', 
		'\x2', '\x2', '\x2', '\x61', '\x62', '\x5', '\x2', '\x2', '\x2', '\x62', 
		'\x1D', '\x3', '\x2', '\x2', '\x2', '\x63', '\x64', '\t', '\x3', '\x2', 
		'\x2', '\x64', '\x1F', '\x3', '\x2', '\x2', '\x2', '\n', '.', '\x33', 
		'\x39', '\x44', '\x46', 'K', 'P', 'W',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
