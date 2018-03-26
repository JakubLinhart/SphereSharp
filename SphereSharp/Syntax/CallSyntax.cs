using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public class CallSyntax : StatementSyntax
    {
        public static CallSyntax GlobalObject { get; } = new CallSyntax(new SymbolSyntax("[global]"), ArgumentListSyntax.Empty);

        public SymbolSyntax MemberNameSyntax { get; }
        public string MemberName => MemberNameSyntax.ToString();
        public ArgumentListSyntax Arguments { get; }
        public CallSyntax Object { get; }
        public bool IsGlobal => Object == GlobalObject;

        public CallSyntax(SymbolSyntax functionName, ArgumentListSyntax arguments, CallSyntax obj)
        {
            Object = obj;
            MemberNameSyntax = functionName;
            Arguments = arguments;
        }

        public CallSyntax(SymbolSyntax functionName, ArgumentListSyntax arguments)
            : this(functionName, arguments, GlobalObject)
        {

        }

        internal CallSyntax(CallSyntax obj, CallSyntax member)
            : this(member.MemberNameSyntax, member.Arguments, obj)
        {
        }

        public static CallSyntax Parse(string src) => CallParser.Call.Parse(src);
    }
}
