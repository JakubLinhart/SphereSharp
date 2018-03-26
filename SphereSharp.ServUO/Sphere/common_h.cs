using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public static partial class _Global
    {
        public static long IsNegative(long c) => ((c < 0)?1:0);
        public static int IMULDIV(int a, int b, int c) => (int)(((((a) * (b)) + (c / 2)) / (c)) - (IsNegative((a) * (b))));

    }
}
