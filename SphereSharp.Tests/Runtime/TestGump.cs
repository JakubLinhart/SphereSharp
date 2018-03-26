using SphereSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Runtime
{
    public class TestGump : RuntimeTestObject, IGump
    {
        public void Button(int x, int y, int normalId, int pressedId, int buttonType, int param, int buttonId)
        {
            WriteLine($"button {x}, {y}, {normalId}, {pressedId}, {buttonType}, {param}, {buttonId}");
        }

        public void GumpPic(int x, int y, int gumpb)
        {
            WriteLine($"gumppic {x}, {y}, {gumpb}");
        }

        public void HtmlGump(int x, int y, int scaleX, int scaleY)
        {
            WriteLine($"htmlgump {x}, {y}, {scaleX}, {scaleY}");
        }

        public void HtmlGumpA(int x, int y, int scaleX, int scaleY, string text)
        {
            WriteLine($"htmlgumpa {x}, {y}, {scaleX}, {scaleY}, \"{text}\"");
        }

        public void ResizePic(int x, int y, int gumpBack, int scaleX, int scaleY)
        {
            WriteLine($"resizepic {x}, {y}, {gumpBack}, {scaleX}, {scaleY}");
        }

        public void SetLocation(int x, int y)
        {
            WriteLine($"setlocation {x}, {y}");
        }

        public void TextA(int x, int y, int hue, string text)
        {
            WriteLine($"texta {x}, {y}, {hue}, {text}");
        }

        public void TextEntry(int x, int y, int width, int height, int hue, int entryId, int startIndex)
        {
            WriteLine($"textentry {x}, {y}, {width}, {height}, {hue}, {entryId}, {startIndex}");
        }

        public void SetText(int startIndex, string text)
        {
            WriteLine($"settext {startIndex}, {text}");
        }
    }
}
