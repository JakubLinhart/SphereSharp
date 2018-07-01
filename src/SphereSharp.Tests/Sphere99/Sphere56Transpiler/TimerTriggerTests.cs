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
    public class TimerTriggerTests
    {
        [TestMethod]
        public void Adds_return_statement_to_the_end_if_missing_return_in_main_flow()
        {
            TranspileTriggerCheck(
@"on=@timer
call1",
@"on=@timer
call1
return 0
");
        }

        [TestMethod]
        public void Doesnt_add_return_statement_to_the_end_if_return_in_main_flow()
        {
            TranspileTriggerCheck(
@"on=@timer
call1
return 1",
@"on=@timer
call1
return 1
");
        }

        [TestMethod]
        public void Adds_0_argument_to_return_statement_in_main_flow_when_no_value_specified()
        {
            TranspileTriggerCheck(
@"on=@timer
call1
return",
@"on=@timer
call1
return 0
");
        }

        [TestMethod]
        public void Adds_0_argument_to_return_statement_in_side_flow_when_no_value_specified()
        {
            TranspileTriggerCheck(
@"on=@timer
if 1==1
    call1
    if 1==1
        return
    endif
endif
return 0
",
@"on=@timer
if 1==1
    call1
    if 1==1
        return 0
    endif
endif
return 0
");
        }

        [TestMethod]
        public void Leaves_return_argument_in_main_flow()
        {
            TranspileTriggerCheck(
@"on=@timer
call1
return 1",
@"on=@timer
call1
return 1
");
        }

        [TestMethod]
        public void Leaves_return_argument_in_side_flow()
        {
            TranspileTriggerCheck(
@"on=@timer
if 1==1
    call1
    if 1==1
        return 1
    endif
endif
return 1
",
@"on=@timer
if 1==1
    call1
    if 1==1
        return 1
    endif
endif
return 1
");
        }

        [TestMethod]
        public void Doesnt_add_return_statement_for_generic_trigger()
        {
            TranspileTriggerCheck(
@"on=@somegenerictrigger
call1",
@"on=@somegenerictrigger
call1
");
        }
    }
}
