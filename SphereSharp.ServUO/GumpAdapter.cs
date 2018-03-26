using Server.Gumps;
using SphereSharp.Model;
using SphereSharp.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace SphereSharp.ServUO
{
    internal class GumpAdapter : IGump
    {
        private Dictionary<string, string> tags = new Dictionary<string, string>();
        private readonly GumpDef gumpDef;

        public Gump Gump { get; set; }

        public GumpAdapter(GumpDef gumpDef)
        {
            this.gumpDef = gumpDef;
        }

        public void Button(int x, int y, int normalId, int pressedId, int buttonType, int param, int buttonId)
        {
            this.Gump.AddButton(x, y, normalId, pressedId, buttonId, buttonType == 1 ? GumpButtonType.Reply : GumpButtonType.Page, param);
        }

        public void GumpPic(int x, int y, int gumpb)
        {
            this.Gump.AddImage(x, y, gumpb);
        }

        public void HtmlGump(int x, int y, int scaleX, int scaleY)
        {
            string text = gumpDef.Texts.Any() ? gumpDef.Texts[0] : string.Empty;

            this.Gump.AddHtml(x, y, scaleX, scaleY, text, false, false);
        }

        public void HtmlGumpA(int x, int y, int scaleX, int scaleY, string text)
        {
            this.Gump.AddHtml(x, y, scaleX, scaleY, text, false, false);
        }

        public void TextA(int x, int y, int hue, string text)
        {
            this.Gump.AddLabel(x, y, hue, text);
        }

        public void ResizePic(int x, int y, int gumpBack, int scaleX, int scaleY)
        {
            this.Gump.AddBackground(x, y, scaleX, scaleY, gumpBack);
        }

        private Dictionary<int, string> texts = new Dictionary<int, string>();

        public void SetText(int startIndex, string text)
        {
            texts[startIndex] = text;
        }

        public void TextEntry(int x, int y, int width, int height, int hue, int entryId, int startIndex)
        {
            this.Gump.AddTextEntry(x, y, width, height, hue, entryId, texts[startIndex]);
        }

        public void SetLocation(int x, int y)
        {
            this.Gump.X = x;
            this.Gump.Y = y;
        }

        public void Tag(string key, string value)
        {
            tags[key] = value;
        }

        public string Tag(string key)
        {
            if (!tags.TryGetValue(key, out string value))
                value = string.Empty;

            return value;
        }
    }
}
