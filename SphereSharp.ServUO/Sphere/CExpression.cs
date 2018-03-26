using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public static partial class _Global
    {
        private static Random random = new Random();

        static _Global()
        {
            unchecked
            {
                random = new Random((int)DateTime.UtcNow.Ticks);
            }
        }

        public static int Calc_GetRandVal(int iqty)
        {
            if (iqty <= 0)
                return (0);
            if (iqty >= int.MaxValue)
            {
                return (IMULDIV(random.Next(), (DWORD)iqty, int.MaxValue));
            }
            return (random.Next() % iqty);
        }

    }
}
