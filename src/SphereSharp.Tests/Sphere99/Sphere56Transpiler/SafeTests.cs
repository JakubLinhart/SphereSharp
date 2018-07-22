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
    public class SafeTests
    {
        [TestMethod]
        public void Enclosed_arguments_with_chained_call()
        {
            TranspileStatementCheck("safe(findid.i_something.remove)", "findid.i_something.remove");
        }

        [TestMethod]
        public void Free_arguments_with_chained_call()
        {
            TranspileStatementCheck("safe findid.i_something.remove", "findid.i_something.remove");
        }

        [TestMethod]
        public void Enclosed_argument_with_free_arguments()
        {
            TranspileStatementCheck("safe(cast 020)", "cast 020");
        }

        [TestMethod]
        public void Enclosed_arguments_with_events_subscription()
        {
            TranspileStatementCheck("safe(events=+e_something)", "events=+e_something");
        }

        [TestMethod]
        public void Free_argument_with_chained_custom_function_call()
        {
            TranspileStatementCheck("safe fun1(<argv(0)>).isChar", "fun1.<argv[0]>.isChar");
        }

        [TestMethod]
        public void Enclosed_argument_with_chained_custom_function_call()
        {
            TranspileConditionCheck("safe(fun1(<argv(0)>).isChar)", "<fun1.<argv[0]>.isChar>");
        }

        [TestMethod]
        public void Chained_argument_with_read_tag_access()
        {
            TranspileConditionCheck("safe.tag(orig_stealth)", "<tag0.orig_stealth>");
        }

        [TestMethod]
        public void Evaluated_chained_argument_with_read_tag_access()
        {
            TranspileConditionCheck("<safe.tag(orig_stealth)>", "<tag0.orig_stealth>");
        }

        [TestMethod]
        public void Free_argument_with_chained_object_tag_property_read_access()
        {
            TranspileCodeBlockCheck("safe src.tag(from).isitem", "src.uid.<tag0.from>.isitem");
        }

        [TestMethod]
        public void Removes_whitespace_after_safe_from_free_argument_list_in_subexpression()
        {
            TranspileConditionCheck("(safe fun1)", "(<fun1>)");
        }
    }
}
