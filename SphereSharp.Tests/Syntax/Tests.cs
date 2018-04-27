using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Syntax
{
    [TestClass]
    public class Tests
    {
        public static Parser<string> Symbol =>
            from symbol in Parse.Letter.AtLeastOnce().Text()
            select $"Symbol({symbol})";

        public static Parser<string> Number =>
            from number in Parse.Number
            select number;

        public static Parser<string> Operator =>
            from @operator in Parse.String("+").Or(Parse.String("-")).Or(Parse.String("<")).Text()
            select @operator;

        public static Parser<string> Eval =>
            from evalKeyword in Parse.IgnoreCase("eval")
            from _1 in Parse.Char(' ')
            from expression in EvalExpression
            select "eval(" + expression + ")";

        public static Parser<string> Macro =>
            from _1 in Parse.Char('<')
            from symbol in Eval
            //from symbol in Symbol
            //from symbol in Eval.Or(Symbol)
            from _2 in Parse.Char('>')
            select $"Macro({symbol})";

        public static Parser<string> ArgOperation =>
            from op in Operator
            from operand2 in Number.Or(Macro).Or(SubExpression)
            select $"Op({op}:{operand2})";

        public static Parser<string> EvalOperation =>
            from op in Parse.Chars("<+-")
            from operand2 in Symbol.Or(Number).Or(Macro).Or(SubExpression)
            select $"Op({op}:{operand2})";

        public static Parser<string> Argument =>
            ExpressionArgument.Or(TokenizedArgument);

        public static Parser<string> ExpressionArgument = ArgumentExpression;

        public static Parser<string> ArgumentExpression =>
            from firstSegment in Macro.Or(Number).Or(SubExpression)
            from segments in ArgOperation.Or(Macro).Many()
            from _ in Parse.LineTerminator
            select "ArgExpression(" + firstSegment + segments.Aggregate(string.Empty, (l, r) => l + r) + ")";

        public static Parser<string> TokenizedArgument =>
            from tokens in ArgumentToken.AtLeastOnce()
            select tokens.Aggregate(string.Empty, (l, r) => l + r);

        public static Parser<string> ArgumentToken =>
            from token in Symbol.Or(Number).Or(Parse.String("(").Text()).Or(Parse.String(")").Text()).Or(Macro).Or(Operator)
            select $"Token({token})";

        public static Parser<string> SubExpression =>
            from _1 in Parse.Char('(')
            from expr in EvalExpression
            from _2 in Parse.Char(')')
            select $"SubExpression({expr})";

        public static Parser<string> EvalExpression =>
            from firstSegment in Symbol.Or(Macro).Or(SubExpression).Or(Number)
            from segments in EvalOperation.Or(Macro).Or(SubExpression).Many()
            select "ArgExpression(" + firstSegment + segments.Aggregate(string.Empty, (l, r) => l + r) + ")";

        public static Parser<string> String =>
            from str in Parse.AnyChar.AtLeastOnce().Text()
            select $"String({str})";

        [TestMethod]
        public void MyTestMethod()
        {
            var result = Argument.Parse("1+1");
            result = Argument.Parse("(1)");
            result = Argument.Parse("(1+2)");
            result = Argument.Parse("(1+2");
            result = Argument.Parse("(1+2)+(3+4)");
            result = Argument.Parse("asdf+1");
            result = Argument.Parse("<eval (1)>");
            result = Argument.Parse("<eval (1+2)>");
            result = Argument.Parse("<eval (1+2>");
            result = Argument.Parse("<eval (1+2)+3>");
            result = Argument.Parse("<eval (1+2)+(3+4)>");
            result = Argument.Parse("<eval (1+(1+2))+(3+4)>");
            result = Argument.Parse("<eval asdf+1>+1");
            result = Argument.Parse("<eval 1>+1");
            result = Argument.Parse("1+asdf");
            result = Argument.Parse("1+<eval asdf>");
            result = Argument.Parse("<eval qwer>+<eval asdf>");
            result = Argument.Parse("<eval asdf+qwer>+<eval rewq+fdsa>");
            result = Argument.Parse("<eval <eval asdf>+qwer>+<eval rewq+<eval fdsa>>");
            result = Argument.Parse("<eval asdf><eval asdf>");
            result = Argument.Parse("<eval asdf><eval asdf>+");
            result = Argument.Parse("<eval asdf><eval asdf>+xxx");
        }
    }
}
