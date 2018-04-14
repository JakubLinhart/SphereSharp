using Sprache;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public class DoSwitchSyntax : StatementSyntax
    {
        public ExpressionSyntax Condition { get; }
        public ImmutableArray<StatementSyntax> Cases { get; }

        public DoSwitchSyntax(ExpressionSyntax condition, ImmutableArray<StatementSyntax> cases)
        {
            Condition = condition;
            Cases = cases;
        }

        public static DoSwitchSyntax Parse(string src)
            => DoSwitchParser.DoSwitch.Parse(src);

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitDoSwitch(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Condition;
            foreach (var c in Cases)
                yield return c;
        }
    }
}
