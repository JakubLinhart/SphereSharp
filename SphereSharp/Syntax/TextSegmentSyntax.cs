namespace SphereSharp.Syntax
{
    public sealed class TextSegmentSyntax : SegmentSyntax
    {
        public string Text { get; }

        public TextSegmentSyntax(string text)
        {
            Text = text;
        }
    }
}
