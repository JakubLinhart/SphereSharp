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
    public class TagTests
    {
        [TestMethod]
        public void Argumented_assignment()
        {
            TranspileStatementCheck("tag(name,value)", "tag.name=value");
        }

        [TestMethod]
        public void Argumented_assignment_to_indexed_tag()
        {
            TranspileStatementCheck("tag(u[0],1)", "tag.u[0]=1");
        }

        [TestMethod]
        public void Assignment_to_indexed_tag_by_underscore_with_constant_index()
        {
            TranspileStatementCheck("tag(u_0,1)", "tag.u[0]=1");
        }

        [TestMethod]
        public void Assignment_to_indexed_tag_by_underscore_with_variable_index()
        {
            // This is most likely not correct. SphereSharp should convert this tag name
            // to indexed tag. Not needed so far.
            TranspileStatementCheck("tag(spawn_<eval arg(i)>)", "tag.spawn_<eval <local.i>>");
        }

        [TestMethod]
        public void Assignment_to_indexed_tag_by_underscore_with_underscores_in_name()
        {
            TranspileStatementCheck("tag(a_b_c_0,1)", "tag.a_b_c[0]=1");
        }

        [TestMethod]
        public void Variable_assingment_to_tag_with_variable_name_and_variable_value()
        {
            TranspileStatementCheck("tag(<argv(0)>,\"<argv(1)>\")", "tag.<argv[0]>=\"<argv[1]>\"");
        }

        [TestMethod]
        public void Argumented_assignment_to_indexed_tag_with_variable_index()
        {
            TranspileStatementCheck("tag(u[arg(x)],1)", "tag.u[<eval <local.x>>]=1");
        }

        [TestMethod]
        public void Argumented_assignment_to_indexed_tag_with_native_function_name_and_variable_index()
        {
            TranspileStatementCheck("tag(scroll[arg(x)],1)", "tag.scroll[<eval <local.x>>]=1");
        }

        [TestMethod]
        public void Chained_assignment()
        {
            TranspileStatementCheck("tag.name=value", "tag.name=value");
        }

        [TestMethod]
        public void Chained_argumented_assignment()
        {
            TranspileStatementCheck("tag.name(value)", "tag.name=value");
        }

        [TestMethod]
        public void Argumented_assignment_to_quoted_tag()
        {
            TranspileStatementCheck("tag(\"u\",value)", "tag.u=value");
        }

        [TestMethod]
        public void Argumented_read()
        {
            TranspileStatementCheck("tag(u)", "tag.u");
        }

        [TestMethod]
        public void Chained_read()
        {
            TranspileStatementCheck("tag.u", "tag.u");
        }

        [TestMethod]
        public void Chained_read_with_variable_name()
        {
            TranspileStatementCheck("tag(<args>)", "tag.<args>");
        }

        [TestMethod]
        public void Argumented_read_access_to_quoted_tag()
        {
            TranspileStatementCheck("tag(\"u\")", "tag.u");
        }

        [TestMethod]
        public void Sharp_replacement()
        {
            TranspileStatementCheck("tag(u,#+1)", "tag.u=<tag.u>+1");
        }

        [TestMethod]
        public void Indexed_sharp_replacement_with_variable_index()
        {
            TranspileStatementCheck("tag(u[arg(x)],#+1)", "tag.u[<eval <local.x>>]=<tag.u[<eval <local.x>>]>+1");
        }

        [TestMethod]
        public void Indexed_sharp_replacement()
        {
            TranspileStatementCheck("tag(u[2],#+1)", "tag.u[2]=<tag.u[2]>+1");
        }

        [TestMethod]
        public void Underscore_index_sharp_replacement()
        {
            TranspileStatementCheck("tag(u_2,#+1)", "tag.u[2]=<tag.u[2]>+1");
        }

        [TestMethod]
        public void Remove_tag()
        {
            TranspileStatementCheck("tag.remove(u)", "tag.u=");
        }

        [TestMethod]
        public void Chained_remove_tag()
        {
            TranspileStatementCheck("tag.remove.u", "tag.u=");
        }

        [TestMethod]
        public void Remove_indexed_tag_with_variable_index()
        {
            TranspileStatementCheck("tag.remove(u[<arg(x)>])", "tag.u[<eval <local.x>>]=");
        }

        [TestMethod]
        public void Remove_indexed_tag()
        {
            TranspileStatementCheck("tag.remove(u[1])", "tag.u[1]=");
        }

        [TestMethod]
        public void Remove_tag_indexed_by_underscore()
        {
            TranspileStatementCheck("tag.remove(tagname_0)", "tag.tagname[0]=");
        }

        [TestMethod]
        public void Remove_quoted_tag()
        {
            TranspileStatementCheck("tag.remove(\"u\")", "tag.u=");
        }
    }
}
