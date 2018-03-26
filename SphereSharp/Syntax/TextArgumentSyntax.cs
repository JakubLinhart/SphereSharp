namespace SphereSharp.Syntax
{
    public class TextArgumentSyntax : ArgumentSyntax
    {
        public string Text { get; }

        public TextArgumentSyntax(string text)
        {
            Text = text;
        }
    }
}
