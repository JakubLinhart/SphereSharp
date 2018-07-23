using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.Tests.Sphere99.Sphere56Transpiler.TranspilerTestsHelper;

namespace SphereSharp.Tests.Sphere99.Sphere56Transpiler
{
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        public void Book_with_properties()
        {
            TranspileFileCheck(
@"[BOOK Kniha_011_klaj]
PAGES=8    
TITLE=""Neznicitelna kuze""
AUTHOR = Targoth
",
@"[BOOK Kniha_011_klaj]
PAGES=8    
TITLE=""Neznicitelna kuze""
AUTHOR = Targoth");
        }

        [TestMethod]
        public void Book_with_free_text_lines()
        {
            TranspileFileCheck(
@"[BOOK b_VoiceReason]
//Selected from Ayn Rand's The Voice of Reason
If the good, the virtuous,
the morally ideal is suffering
",
@"[BOOK b_VoiceReason]
//Selected from Ayn Rand's The Voice of Reason
If the good, the virtuous,
the morally ideal is suffering
");
        }
    }
}
