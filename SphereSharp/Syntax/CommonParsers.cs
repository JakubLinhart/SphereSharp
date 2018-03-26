using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SphereSharp.Syntax
{
    internal static class CommonParsers
    {
        public static Parser<char> SymbolFirstChar =>
            from firstChar in Parse.Letter
            select firstChar;

        public static Parser<string> Symbol =>
            from symbol in SymbolCore.Except(Keyword)
            select symbol;

        public static Parser<string> SymbolCore =>
            from firstChar in SymbolFirstChar.Once()
            from nextChars in Parse.LetterOrDigit.Or(Parse.Chars('_')).Many()
            select new string(firstChar.Concat(nextChars).ToArray());

        public static Parser<string> Keyword =>
            from keyword in IfKeyword.Or(ElseKeyword).Or(ElseIfKeyword).Or(EndifKeyword).Or(DoSwitchKeyword).Or(EndDoKeyword)
            select new string(keyword.ToArray());

        internal static Parser<IEnumerable<char>> EmptyLine =>
            from line in Ignored
            select line;

        public static Parser<IEnumerable<char>> Comment =>
            from wh1 in OneLineWhiteSpace.Many()
            from commentStart in Parse.String("//")
            from comment in Parse.AnyChar.Except(Parse.LineTerminator).Many()
            from eol in Parse.LineTerminator
            select wh1.Concat(commentStart).Concat(comment);

        public static Parser<IEnumerable<char>> Eol =>
            from lineTerminator in Parse.LineTerminator.Or(Comment)
            select lineTerminator;

        public static Parser<IEnumerable<char>> IfKeyword => Parse.IgnoreCase("if");
        public static Parser<IEnumerable<char>> ElseKeyword => Parse.IgnoreCase("else");
        public static Parser<IEnumerable<char>> ElseIfKeyword => Parse.IgnoreCase("elseif");
        public static Parser<IEnumerable<char>> EndifKeyword => Parse.IgnoreCase("endif");
        public static Parser<IEnumerable<char>> DoSwitchKeyword => Parse.IgnoreCase("doswitch");
        public static Parser<IEnumerable<char>> EndDoKeyword => Parse.IgnoreCase("enddo");

        public static Parser<string> IntegerDecadicNumber =>
            from sign in Parse.Char('-').Optional()
            from number in Parse.Number
            select sign.IsDefined ? "-" + number : number;

        public static Parser<string> IntegerHexNumber =>
            from _ in Parse.Char('0')
            from number in Parse.Chars("0123456789ABCDEFabcdef").AtLeastOnce().Text()
            select number;

        public static Parser<char> OneLineWhiteSpace =>
            from ws in Parse.WhiteSpace.Except(Parse.LineEnd)
            select ws;

        public static Parser<IEnumerable<char>> Ignored =>
            Parse.WhiteSpace.Many().Or(Parse.LineTerminator).Or(Comment);

        public static Parser<IEnumerable<char>> LeftMacroParenthesis =>
            from paren in Parse.String("<")
            select paren;

        public static Parser<IEnumerable<char>> RightMacroParenthesis =>
            from paren in Parse.String(">")
            select paren;

        public static Parser<IEnumerable<char>> LeftLiteralMacroParenthesis =>
            from paren in Parse.String("<?")
            select paren;

        public static Parser<IEnumerable<char>> RightLiteralMacroParenthesis =>
            from paren in Parse.String("?>")
            select paren;
    }
}
