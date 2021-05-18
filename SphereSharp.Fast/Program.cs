using SphereSharp.Sphere99.Enumerable;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SphereSharp.Fast
{
    class Program
    {
        static void Main()
        {
            var objects = new Dictionary<uint, GameObject>();
            var defNamesCount = new Dictionary<string, int>();

            using (var reader = File.OpenText(@"c:\Users\jakub\sources\ultima\erebor\saves\20201111\save\fragment.scp"))
            {
                foreach (var section in ReadSections(reader))
                {
                    objects.Add(section.Serial, section);
                    if (defNamesCount.TryGetValue(section.Defname, out var count))
                        defNamesCount[section.Defname] = count + 1;
                    else
                        defNamesCount[section.Defname] = 1;
                }
            }

            foreach (var x in defNamesCount.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{x.Key} - {x.Value}");
            }
        }

        static IEnumerable<GameObject> ReadSections(StreamReader reader)
        {
            SkipSection(reader);

            while (true)
            {
                SectionType sectionType;
                string defname = "";

                try
                {
                    SkipWhitespace(reader);
                    Expect(reader, '[');
                    sectionType = ReadSectionType(reader);
                    switch (sectionType)
                    {
                        case SectionType.Char:
                        case SectionType.Item:
                            SkipWhitespace(reader);
                            defname = ReadSymbol(reader);
                            Expect(reader, ']');
                            ExpectEndOfLine(reader);
                            break;
                        case SectionType.EOF:
                        case SectionType.Vars:
                            Expect(reader, ']');
                            ExpectEndOfLine(reader);
                            break;
                        case SectionType.Sector:
                        case SectionType.GMPage:
                            break;
                    }
                }
                catch (EndOfFileException)
                {
                    break;
                }

                switch (sectionType)
                {
                    case SectionType.Char:
                    case SectionType.Item:
                        var obj = new GameObject(defname);
                        ReadProperties(obj, reader);
                        yield return obj;
                        break;
                    case SectionType.EOF:
                        yield break;
                    default:
                        try
                        {
                            SkipSection(reader);
                        }
                        catch (EndOfFileException)
                        {
                            yield break;
                        }
                        break;
                }
            }
        }

        static void SkipSection(StreamReader reader)
        {
            bool seekingEol = false;
            do
            {
                if (seekingEol)
                {
                    var ch = reader.Read();
                    if (ch == -1)
                        throw new EndOfFileException();
                    if (ch == '\n')
                        seekingEol = false;
                }
                else
                {
                    var ch = reader.Peek();
                    if (ch == -1)
                        throw new EndOfFileException();
                    if (ch == '[')
                        return;
                    if (char.IsWhiteSpace((char)ch))
                        reader.Read();
                    else
                        seekingEol = true;
                }    
            } while (true);
        }

        static void ReadProperties(GameObject obj, StreamReader reader)
        {
            while (true)
            {
                SkipWhitespace(reader);
                var ch = reader.Peek();
                if (ch == '[' || ch == -1)
                    break;

                var name = ReadSymbol(reader);

                switch (name.ToLower())
                {
                    case "serial":
                        Expect(reader, '=');
                        obj.Serial = ReadUid(reader);
                        break;
                    case "amount":
                        Expect(reader, '=');
                        obj.Amount = ReadInt32(reader);
                        break;
                    case "cont":
                        Expect(reader, '=');
                        obj.ContainerId = ReadUid(reader);
                        break;
                    case "tag.gold":
                        Expect(reader, '=');
                        obj.TagGold = ReadInt32(reader);
                        break;
                    case "more1":
                        Expect(reader, '=');
                        obj.More1 = ReadString(reader);
                        break;
                    default:
                        SkipLine(reader);
                        break;
                }
            }
        }

        static uint ReadUid(StreamReader reader)
        {
            Optional(reader, '#');
            Expect(reader, '0');

            return ReadHexUInt32(reader);
        }

        static uint ReadHexUInt32(StreamReader reader)
        {
            uint numericBase = 1;
            uint value = 0;
            int digitsCount = 0;
            while (true)
            {
                int ch = reader.Read();
                if (ch == -1 || char.IsWhiteSpace((char)ch))
                {
                    if (digitsCount > 0)
                        break;
                    else
                        throw new ParserException("Number expected");
                }

                if (!IsHexDigit((char)ch))
                    throw new ParserException("Digit expected");

                uint digit;
                if (!char.IsNumber((char)ch))
                {
                    if (ch >= 'a' && ch <= 'f')
                        digit = (uint)ch - 'a';
                    else
                        digit = (uint)ch - 'A' + 10;
                }
                else
                    digit = (uint)ch - '0';
                value += digit * numericBase;
                numericBase *= 16;
                digitsCount++;
            }

            SkipWhitespace(reader);
            return ReverseHexUInt(digitsCount, value);
        }

        static bool IsHexDigit(char ch)
        {
            if (char.IsDigit(ch))
                return true;

            return (ch >= 'a' && ch <= 'f') || (ch >= 'A' && ch <= 'F');
        }

        static string ReadString(StreamReader reader)
        {
            var builder = new StringBuilder();

            do
            {
                var ch = reader.Read();
                if (ch == -1)
                    break;
                if (ch == '\r' || ch == '\n')
                {
                    SkipWhitespace(reader);
                    break;
                }
                builder.Append((char)ch);
            } while (true);

            return builder.ToString();
        }

        static int ReadInt32(StreamReader reader)
        {
            int numericBase = 1;
            int value = 0;
            int digitsCount = 0;
            while (true)
            {
                int ch = reader.Read();
                if (ch== -1 || char.IsWhiteSpace((char)ch))
                {
                    if (digitsCount > 0)
                        break;
                    else
                        throw new ParserException("Number expected");
                }

                if (!char.IsDigit((char)ch))
                    throw new ParserException("Digit expected");

                var digit = ch - '0';
                value += digit * numericBase;
                numericBase *= 10;
                digitsCount++;
            }

            SkipWhitespace(reader);
            return ReverseInt(digitsCount, value);
        }

        public static int ReverseInt(int digitsCount, int num)
        {
            int result = 0;
            while (num > 0)
            {
                result = result * 10 + num % 10;
                num /= 10;
                digitsCount--;
            }

            while (digitsCount > 0)
            {
                result *= 16;
                digitsCount--;
            }

            return result;
        }

        public static uint ReverseHexUInt(int digitsCount, uint num)
        {
            uint result = 0;
            while (num > 0)
            {
                result = result * 16 + num % 16;
                num /= 16;
                digitsCount--;
            }

            while (digitsCount > 0)
            {
                result *= 16;
                digitsCount--;
            }

            return result;
        }

        static void ExpectEndOfLine(StreamReader reader)
        {
            var ch = (char)reader.Read();
            if (ch == '\r')
            {
                ch = (char)reader.Read();
                if (ch == '\n')
                    return;
            }
            else if (ch == '\n')
                return;

            throw new ParserException("Expecting end of line");
        }

        static void SkipLine(StreamReader reader)
        {
            int ch;
            do
            {
                ch = reader.Read();
            } while (ch != -1 && ch != '\n');
        }

        static void SkipWhitespace(StreamReader reader)
        {
            int ch;
            do
            {
                ch = reader.Peek();
                if (char.IsWhiteSpace((char)ch))
                    reader.Read();
                else
                    break;
            } while (true);
        }

        static void Optional(StreamReader reader, char optionalChar)
        {
            if (reader.Peek() == optionalChar)
                reader.Read();
        }

        static void Expect(StreamReader reader, char expectedChar)
        {
            int ch = reader.Read();
            if (ch < 0)
                throw new EndOfFileException();

            if ((char)ch != expectedChar)
                throw new ParserException($"Unexpected char {(char)ch}, expecting {expectedChar}.");
        }

        static int Expect(StreamReader reader, char[] delimiters, string[] strings, bool[] matches)
        {
            if (strings.Length != matches.Length)
                throw new InvalidOperationException();

            for (int i = 0; i < matches.Length; i++)
                matches[i] = true;

            StringBuilder asdf = new StringBuilder();

            int index = 0;
            bool canContinue;
            do
            {
                canContinue = false;
                int value = reader.Peek();
                if (value < 0)
                    throw new ParserException("Unexpected end of file.");

                var ch = (char)value;
                if (delimiters.Contains(ch))
                    break;
                asdf.Append((char)reader.Read());

                for (int i = 0; i < strings.Length; i++)
                {
                    if (matches[i])
                    {
                        var currentString = strings[i];
                        if (index >= currentString.Length || ch != currentString[index])
                            matches[i] = false;
                        else
                            canContinue = true;
                    }
                }
                index++;
            } while (canContinue);

            for (int i = 0; i < strings.Length; i++)
            {
                if (matches[i] && strings[i].Length == index)
                    return i;
            }

            throw new ParserException("Unexpected string found");
        }

        static string ReadSymbol(StreamReader reader)
        {
            int ch;
            var builder = new StringBuilder();
            bool isSymbolChar = true;

            do
            {
                ch = reader.Peek();
                if (IsSymbolChar(ch))
                {
                    reader.Read();
                    builder.Append((char)ch);
                }
                else
                    isSymbolChar = false;
            } while (isSymbolChar);

            return builder.ToString();
        }

        static char[] sectionTypeDelimiters = { ' ', ']' };

        static bool IsSymbolChar(int input)
        {
            char ch = (char)input;
            return ch == '#' || ch == '_' || ch == '.' || char.IsLetterOrDigit(ch);
        }

        static string[] sectionTypes = new[] { "WorldChar", "WorldItem", "VarNames", "GMPage", "EOF", "Sector" };
        static bool[] sectionMatches = new bool[sectionTypes.Length];

        static SectionType ReadSectionType(StreamReader reader)
        {
            return (SectionType)Expect(reader, sectionTypeDelimiters, sectionTypes, sectionMatches);
        }
    }

    public sealed class EndOfFileException : Exception
    {
    }

    public sealed class ParserException : Exception
    {
        public ParserException(string message) : base(message) { }
    }
}
