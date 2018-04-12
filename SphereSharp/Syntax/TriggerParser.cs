using Sprache;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SphereSharp.Syntax
{
    public static class TriggerParser
    {
        public static Parser<IEnumerable<char>> TriggerPrefix =>
            Parse.IgnoreCase("on=");

        public static Parser<string> Name =>
            from name in Parse.Letter.Many().Text()
            select name;

        public static Parser<TriggerSyntax> NumberedTrigger =>
            from _1 in CommonParsers.Ignored.Many()
            from _2 in TriggerPrefix
            from name in CommonParsers.IntegerDecadicNumber
            from _4 in CommonParsers.Ignored.Many()
            from codeBlock in CodeBlockParser.CodeBlock.Optional()
            select new TriggerSyntax(name, codeBlock.IsDefined ? codeBlock.Get() : new CodeBlockSyntax(ImmutableArray<StatementSyntax>.Empty));


        public static Parser<TriggerSyntax> NamedTrigger =>
            from _1 in CommonParsers.Ignored.Many()
            from _2 in TriggerPrefix
            from _3 in Parse.String("@")
            from name in Name
            from _4 in CommonParsers.Ignored.Many()
            from codeBlock in CodeBlockParser.CodeBlock.Optional()
            select new TriggerSyntax(name, codeBlock.IsDefined ? codeBlock.Get() : new CodeBlockSyntax(ImmutableArray<StatementSyntax>.Empty));

        public static Parser<TriggerSyntax> Trigger =>
            from trigger in NumberedTrigger.Or(NamedTrigger)
            select trigger;
    }
}
