using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99
{
    public sealed class FirstMemberAccessArgumentsVisitor : sphereScript99BaseVisitor<IParseTree[]>
    {
        public override IParseTree[] VisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context)
        {
            var arguments = context.enclosedArgumentList()?.argumentList()?.argument();
            if (arguments != null)
                return arguments;

            return Array.Empty<IParseTree>();
        }

        public override IParseTree[] VisitNativeMemberAccess([NotNull] sphereScript99Parser.NativeMemberAccessContext context)
        {
            if (context.nativeArgumentList()?.enclosedArgumentList()?.argumentList()?.argument() != null)
                return context.nativeArgumentList().enclosedArgumentList().argumentList().argument().Select(x => x.children[0]).ToArray();

            var freeArgumentList = context.nativeArgumentList()?.freeArgumentList();
            if (freeArgumentList != null)
            {
                var arguments = new List<IParseTree>();
                var optionalWhiteSpaceArgument = freeArgumentList?.firstFreeArgument()?.firstFreeArgumentOptionalWhiteSpace();
                if (optionalWhiteSpaceArgument != null)
                {
                    arguments.Add(optionalWhiteSpaceArgument.evalExpression() 
                        ?? optionalWhiteSpaceArgument.triggerArgument() 
                        ?? (IParseTree)optionalWhiteSpaceArgument.quotedLiteralArgument());
                }

                var mandatoryWhiteSpaceArgument = freeArgumentList?.firstFreeArgument()?.firstFreeArgumentMandatoryWhiteSpace();
                if (mandatoryWhiteSpaceArgument != null)
                {
                    arguments.Add(mandatoryWhiteSpaceArgument.assignmentArgument()
                        ?? (IParseTree)mandatoryWhiteSpaceArgument.unquotedLiteralArgument());
                }

                arguments.AddRange(freeArgumentList.argument());

                return arguments.ToArray();
            }

            return Array.Empty<IParseTree>();
        }
    }
}
