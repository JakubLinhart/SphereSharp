using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    public static class SharpStriper
    {
        public static bool TryStrip(string value, out string strippedValue)
        {
            if (value != null && (value.StartsWith("#0") || value.StartsWith("\"#0")))
            {
                bool startsWithDoubleQuotes = value.StartsWith("\"");
                string hexValueText = value.Trim('"').TrimStart('#');

                if (int.TryParse(hexValueText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hexValue))
                {
                    if (startsWithDoubleQuotes)
                        hexValueText = '"' + hexValueText + '"';

                    strippedValue = hexValueText;
                    return true;
                }
            }

            strippedValue = value;
            return false;
        }
    }
}
