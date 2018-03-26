using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Runtime
{
    public interface IGump
    {
        void SetLocation(int x, int y);
        void ResizePic(int x, int y, int gumpBack, int scaleX, int scaleY);
        void GumpPic(int x, int y, int gumpb);
        void HtmlGump(int x, int y, int scaleX, int scaleY);
        void HtmlGumpA(int x, int y, int scaleX, int scaleY, string text);
        void Button(int x, int y, int normalId, int pressedId, int buttonType, int param, int buttonId);
        void TextA(int x, int y, int hue, string text);
        void SetText(int startIndex, string text);
        void TextEntry(int x, int y, int width, int height, int hue, int entryId, int startIndex);
    }
}
