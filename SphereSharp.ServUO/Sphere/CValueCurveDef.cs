using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public class CValueCurveDef
    {
        public int[] m_aiValues = new int[] { };

        public int GetLinear(int iSkillPercent)
        {
	        //
	        // ARGS:
	        //  iSkillPercent = 0 - 1000 = 0 to 100.0 percent
	        //  m_Rate[3] = the 3 advance rate control numbers, 100,50,0 skill levels
	        //		Acts as line segments.
	        // RETURN:
	        //  raw chance value.

	        int iSegSize;
	        int iLoIdx;

	        int iQty = m_aiValues.Length;
            switch (iQty)
            {
                case 0:
                    return (0); // no values defined !
                case 1:
                    return (m_aiValues[0]);
                case 2:
                    iLoIdx = 0;
                    iSegSize = 1000;
                    break;
                case 3:
                    // Do this the fastest.
                    if (iSkillPercent >= 500)
                    {
                        iLoIdx = 1;
                        iSkillPercent -= 500;
                    }
                    else
                    {
                        iLoIdx = 0;
                    }
                    iSegSize = 500;
                    break;
                default:
                    // More
                    iLoIdx = IMULDIV(iSkillPercent, iQty, 1000);
                    iQty--;
                    if (iLoIdx >= iQty)
                        iLoIdx = iQty - 1;
                    iSegSize = 1000 / iQty;
                    iSkillPercent -= (iLoIdx * iSegSize);
                    break;

            }

            int iLoVal = m_aiValues[iLoIdx];
            int iHiVal = m_aiValues[iLoIdx + 1];
            int iChance = iLoVal + IMULDIV(iHiVal - iLoVal, (iSkillPercent), (iSegSize));

            if (iChance <= 0)
                return 0; // less than no chance ?

            return (iChance);
        }
    }
}
