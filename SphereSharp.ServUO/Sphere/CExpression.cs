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

        public static int Calc_GetSCurve(int iValDiff, int iVariance)
        {
            // ARGS:
            //   iValDiff = Difference between our skill level and difficulty.
            //     positive = high chance, negative = lower chance
            //     0 = 50.0% chance.
            //   iVariance = the 25.0% difference point of the bell curve
            // RETURN:
            //   What is the (0-100.0)% chance of success = 0-1000
            // NOTE:
            //   Chance of skill gain is inverse to chance of success.

            int iChance = Calc_GetBellCurve(iValDiff, iVariance);
            if (iValDiff > 0)
                return 1000 - iChance;
            return iChance;
        }

        public static int Calc_GetBellCurve(int iValDiff, int iVariance)
        {
            // Produce a log curve.
            //
            // 50+
            //	 |
            //	 |
            //	 |
            // 25|  +
            //	 |
            //	 |     +
            //	 |        +
            //	0 --+--+--+--+------
            //    iVar				iValDiff
            //
            // ARGS:
            //   iValDiff = Given a value relative to 0
            //     0 = 50.0% chance.
            //   iVariance = the 25.0% point of the bell curve
            // RETURN:
            //   (0-100.0) % chance at this iValDiff.
            //   Chance gets smaller as Diff gets bigger.
            // EXAMPLE:
            //   if ( iValDiff == iVariance ) return 250
            //   if ( iValDiff == 0 ) return 500

            if (iVariance <= 0) // this really should not happen but just in case.
                return 500;

            if (iValDiff < 0)
                iValDiff = -iValDiff;

            int iChance = 500;
            while ((iValDiff > iVariance) && iChance != 0)
            {
                iValDiff -= iVariance;
                iChance /= 2;   // chance is halved for each Variance period.
            }

            return iChance - IMULDIV(iChance / 2, iValDiff, iVariance);
        }



    }
}
