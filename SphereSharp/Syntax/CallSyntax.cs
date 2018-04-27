using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public class CallSyntax : StatementSyntax
    {
        public SymbolSyntax MemberNameSyntax { get; }
        public string MemberName => MemberNameSyntax.ToString();
        public ArgumentListSyntax Arguments { get; }
        public CallSyntax ChainedCall { get; }

        public CallSyntax(SymbolSyntax functionName, ArgumentListSyntax arguments, CallSyntax chainedCall)
        {
            ChainedCall = chainedCall;
            MemberNameSyntax = functionName;
            Arguments = arguments;
        }

        public CallSyntax(SymbolSyntax functionName, ArgumentListSyntax arguments)
            : this(functionName, arguments, null)
        {

        }

        public CallSyntax(CallSyntax call, CallSyntax chainedCall)
            : this(call.MemberNameSyntax, call.Arguments, chainedCall)
        {
        }

        public static CallSyntax Parse(string src) => CallParser.Call.Parse(src);
        public static CallSyntax ParseCustom(string src) => CallParser.CustomCall.Parse(src);

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitCall(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return MemberNameSyntax;
            yield return Arguments;
            if (ChainedCall != null)
                yield return ChainedCall;
        }
    }
}
