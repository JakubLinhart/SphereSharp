using System;
using System.Text;

namespace SphereSharp.Sphere.Generator
{
    public class IndentedStringBuilder
    {
        private StringBuilder builder = new StringBuilder();
        private int indentationLevel = 0;
        private bool requireIndentation = true;

        public string IndentationString { get; set; } = "  ";

        private void EnsureIndentation()
        {
            if (requireIndentation)
            {
                for (int i = 0; i < indentationLevel; i++)
                {
                    builder.Append(IndentationString);
                }
                requireIndentation = false;
            }
        }

        public void Append(string str)
        {
            EnsureIndentation();
            builder.Append(str);
        }

        public void Append(char ch)
        {
            EnsureIndentation();
            builder.Append(ch);
        }

        public void AppendLine(string str)
        {
            Append(str);
            AppendLine();
        }

        public void AppendLine(char ch)
        {
            Append(ch);
            AppendLine();
        }

        public void AppendLine()
        {
            builder.AppendLine();
            requireIndentation = true;
        }

        public void Indent()
        {
            indentationLevel++;
        }

        public void Unindent()
        {
            indentationLevel--;
        }

        public override string ToString() => builder.ToString();

        internal void Append(object operatorString)
        {
            throw new NotImplementedException();
        }
    }
}
