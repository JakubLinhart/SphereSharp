using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.Tests.Sphere99.Parser.ParserTestsHelper;

namespace SphereSharp.Tests.Sphere99.Parser
{
    [TestClass]
    public class TagAccessTests
    {
        [TestMethod]
        public void Tag_argumented_assignment()
        {
            StatementShouldSucceed("tag(tag1,1+1)");
            StatementShouldSucceed("tag(tag2,(2+2))");
            StatementShouldSucceed("tag(tag3,var1)");
            StatementShouldSucceed("tag(tag4,var1+1)");
            StatementShouldSucceed("tag(tag5,<var1>+1)");
        }

        [TestMethod]
        public void Tag_argumented_read()
        {
            StatementShouldSucceed("<tag(tag1)>");
        }

        [TestMethod]
        public void Tag_chained_assignment()
        {
            StatementShouldSucceed("tag.tag1=value");
        }

        [TestMethod]
        public void Tag_chained_read()
        {
            StatementShouldSucceed("<tag.tag1>");
        }

        [TestMethod]
        public void Chained_call_on_tag()
        {
            StatementShouldSucceed("arg(u,<tag(tag1).name>)");
            StatementShouldFail("arg(u,<tag.tag1.name>)");
        }
    }
}
