//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Math.g4 by ANTLR 4.13.0

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

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.CLSCompliant(false)]
public partial class MathParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, OP_ADD=3, OP_SUB=4, OP_MUL=5, OP_DIV=6, NUM=7, ID=8, WS=9;
	public const int
		RULE_compileUnit = 0, RULE_expr = 1;
	public static readonly string[] ruleNames = {
		"compileUnit", "expr"
	};

	private static readonly string[] _LiteralNames = {
		null, "'('", "')'", "'+'", "'-'", "'*'", "'/'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, "OP_ADD", "OP_SUB", "OP_MUL", "OP_DIV", "NUM", "ID", 
		"WS"
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

	public override string GrammarFileName { get { return "Math.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static MathParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public MathParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public MathParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class CompileUnitContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(MathParser.Eof, 0); }
		public CompileUnitContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_compileUnit; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IMathVisitor<TResult> typedVisitor = visitor as IMathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCompileUnit(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public CompileUnitContext compileUnit() {
		CompileUnitContext _localctx = new CompileUnitContext(Context, State);
		EnterRule(_localctx, 0, RULE_compileUnit);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 4;
			expr(0);
			State = 5;
			Match(Eof);
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

	public partial class ExprContext : ParserRuleContext {
		public ExprContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expr; } }
	 
		public ExprContext() { }
		public virtual void CopyFrom(ExprContext context) {
			base.CopyFrom(context);
		}
	}
	public partial class InfixExprContext : ExprContext {
		public ExprContext left;
		public IToken op;
		public ExprContext right;
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OP_MUL() { return GetToken(MathParser.OP_MUL, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OP_DIV() { return GetToken(MathParser.OP_DIV, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OP_ADD() { return GetToken(MathParser.OP_ADD, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OP_SUB() { return GetToken(MathParser.OP_SUB, 0); }
		public InfixExprContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IMathVisitor<TResult> typedVisitor = visitor as IMathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInfixExpr(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class UnaryExprContext : ExprContext {
		public IToken op;
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OP_ADD() { return GetToken(MathParser.OP_ADD, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OP_SUB() { return GetToken(MathParser.OP_SUB, 0); }
		public UnaryExprContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IMathVisitor<TResult> typedVisitor = visitor as IMathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitUnaryExpr(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class FuncExprContext : ExprContext {
		public IToken func;
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ID() { return GetToken(MathParser.ID, 0); }
		public FuncExprContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IMathVisitor<TResult> typedVisitor = visitor as IMathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFuncExpr(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class NumberExprContext : ExprContext {
		public IToken value;
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode NUM() { return GetToken(MathParser.NUM, 0); }
		public NumberExprContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IMathVisitor<TResult> typedVisitor = visitor as IMathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNumberExpr(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ParensExprContext : ExprContext {
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public ParensExprContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IMathVisitor<TResult> typedVisitor = visitor as IMathVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitParensExpr(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExprContext expr() {
		return expr(0);
	}

	private ExprContext expr(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		ExprContext _localctx = new ExprContext(Context, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 2;
		EnterRecursionRule(_localctx, 2, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 20;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case T__0:
				{
				_localctx = new ParensExprContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;

				State = 8;
				Match(T__0);
				State = 9;
				expr(0);
				State = 10;
				Match(T__1);
				}
				break;
			case OP_ADD:
			case OP_SUB:
				{
				_localctx = new UnaryExprContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 12;
				((UnaryExprContext)_localctx).op = TokenStream.LT(1);
				_la = TokenStream.LA(1);
				if ( !(_la==OP_ADD || _la==OP_SUB) ) {
					((UnaryExprContext)_localctx).op = ErrorHandler.RecoverInline(this);
				}
				else {
					ErrorHandler.ReportMatch(this);
				    Consume();
				}
				State = 13;
				expr(5);
				}
				break;
			case ID:
				{
				_localctx = new FuncExprContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 14;
				((FuncExprContext)_localctx).func = Match(ID);
				State = 15;
				Match(T__0);
				State = 16;
				expr(0);
				State = 17;
				Match(T__1);
				}
				break;
			case NUM:
				{
				_localctx = new NumberExprContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 19;
				((NumberExprContext)_localctx).value = Match(NUM);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			Context.Stop = TokenStream.LT(-1);
			State = 30;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,2,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					State = 28;
					ErrorHandler.Sync(this);
					switch ( Interpreter.AdaptivePredict(TokenStream,1,Context) ) {
					case 1:
						{
						_localctx = new InfixExprContext(new ExprContext(_parentctx, _parentState));
						((InfixExprContext)_localctx).left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 22;
						if (!(Precpred(Context, 4))) throw new FailedPredicateException(this, "Precpred(Context, 4)");
						State = 23;
						((InfixExprContext)_localctx).op = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==OP_MUL || _la==OP_DIV) ) {
							((InfixExprContext)_localctx).op = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 24;
						((InfixExprContext)_localctx).right = expr(5);
						}
						break;
					case 2:
						{
						_localctx = new InfixExprContext(new ExprContext(_parentctx, _parentState));
						((InfixExprContext)_localctx).left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 25;
						if (!(Precpred(Context, 3))) throw new FailedPredicateException(this, "Precpred(Context, 3)");
						State = 26;
						((InfixExprContext)_localctx).op = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==OP_ADD || _la==OP_SUB) ) {
							((InfixExprContext)_localctx).op = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 27;
						((InfixExprContext)_localctx).right = expr(4);
						}
						break;
					}
					} 
				}
				State = 32;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,2,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 1: return expr_sempred((ExprContext)_localctx, predIndex);
		}
		return true;
	}
	private bool expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0: return Precpred(Context, 4);
		case 1: return Precpred(Context, 3);
		}
		return true;
	}

	private static int[] _serializedATN = {
		4,1,9,34,2,0,7,0,2,1,7,1,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
		1,1,1,1,1,1,1,1,1,3,1,21,8,1,1,1,1,1,1,1,1,1,1,1,1,1,5,1,29,8,1,10,1,12,
		1,32,9,1,1,1,0,1,2,2,0,2,0,2,1,0,3,4,1,0,5,6,36,0,4,1,0,0,0,2,20,1,0,0,
		0,4,5,3,2,1,0,5,6,5,0,0,1,6,1,1,0,0,0,7,8,6,1,-1,0,8,9,5,1,0,0,9,10,3,
		2,1,0,10,11,5,2,0,0,11,21,1,0,0,0,12,13,7,0,0,0,13,21,3,2,1,5,14,15,5,
		8,0,0,15,16,5,1,0,0,16,17,3,2,1,0,17,18,5,2,0,0,18,21,1,0,0,0,19,21,5,
		7,0,0,20,7,1,0,0,0,20,12,1,0,0,0,20,14,1,0,0,0,20,19,1,0,0,0,21,30,1,0,
		0,0,22,23,10,4,0,0,23,24,7,1,0,0,24,29,3,2,1,5,25,26,10,3,0,0,26,27,7,
		0,0,0,27,29,3,2,1,4,28,22,1,0,0,0,28,25,1,0,0,0,29,32,1,0,0,0,30,28,1,
		0,0,0,30,31,1,0,0,0,31,3,1,0,0,0,32,30,1,0,0,0,3,20,28,30
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
