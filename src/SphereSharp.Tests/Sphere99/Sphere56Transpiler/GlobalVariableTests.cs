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
    public class GlobalVariableTests
    {
        [TestMethod]
        public void Global_variables_with_native_function_name_requires_explicit_access_to_be_explicitelly_prefixed()
        {
            TranspileFileCheck(
@"[function fun1]
var(p,1)
[function fun2]
p=1
var(p,1)",

@"[function fun1]
var.p=1
[function fun2]
p=1
var.p=1"
);
        }

        [TestMethod]
        public void Can_recognize_chained_global_variable_declaration()
        {
            TranspileCodeBlockCheck(
@"var.v1=1
var.v1=<v1>*15",
@"var.v1=1
var.v1=<var.v1>*15");
        }

        [TestMethod]
        public void Ignores_chained_call_on_global_variable_access_with_arguments()
        {
            TranspileCodeBlockCheck(
@"var(x).more1=1
more1=2",
@"uid.<var0.x>.more1=1
more1=2");
        }

        [TestMethod]
        public void Can_recognize_self_reference_sharp_replacement_for_chained_global_variable_declaration()
        {
            TranspileCodeBlockCheck(
@"var.x=1
var.x=#+1",
@"var.x=1
var.x=<var.x>+1");
        }

        [TestMethod]
        public void Self_reference_sharp_replacement()
        {
            TranspileStatementCheck("var(u,#+1)", "var.u=<var.u>+1");
            TranspileStatementCheck("var(u[arg(x)],#+1)", "var.u[<eval <local.x>>]=<var.u[<eval <local.x>>]>+1");
            TranspileStatementCheck("var(u[1],#+1)", "var.u[1]=<var.u[1]>+1");
        }

        [TestMethod]
        public void Basic_tests()
        {
            TranspileStatementCheck("var(name,value)", "var.name=value");
            TranspileStatementCheck("arg(u,var(name))", "local.u=var.name");

            TranspileCodeBlockCheck(@"var(u,1)
var(v,<eval u>)",
@"var.u=1
var.v=<eval <var.u>>");

            TranspileCodeBlockCheck(
@"var(u,1)
fun1(u)",
@"var.u=1
fun1 <var.u>");
        }

        [TestMethod]
        public void Can_transpile_explicite_global_variable_with_macro_in_name()
        {
            TranspileCodeBlockCheck(
@"var(name_<argv(0)>,1)
var(name_<argv(0)>,2)",
@"var.name_<argv[0]>=1
var.name_<argv[0]>=2");
        }

        [TestMethod]
        public void Ignores_global_variable_name_trailing_whitespace()
        {
            TranspileCodeBlockCheck(
@"var(is_blunt    ,1)
var(u,<eval(is_blunt)>)",
@"var.is_blunt    =1
var.u=<eval(<var.is_blunt>)>");
        }
    }
}

