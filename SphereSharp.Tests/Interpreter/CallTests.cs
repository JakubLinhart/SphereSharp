using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Interpreter
{
    [TestClass]
    public class CallTests
    {
        [TestMethod]
        public void Can_call_user_defined_function_with_argv_arguments()
        {
            string funcSrc = @"
[FUNCTION dialogclose]
src.CloseDialog(<argv(0)>, <argv(1)>)";

            var builder = new TestEvaluator();
            builder
                .SetSrc(builder.TestObjBase)
                .AddFunction(funcSrc)
                .Create();

            builder.EvaluateCodeBlock("dialogclose(d_test, 123)");

            var output = builder.TestObjBase.GetOutput();
            output.Should().Contain("closedialog d_test, 123");
        }

        [TestMethod]
        public void Can_call_user_defined_function_with_args_argument()
        {
            string funcSrc = @"
[FUNCTION dialogclose]
src.CloseDialog(<args>, 123)";

            var builder = new TestEvaluator();
            builder
                .SetSrc(builder.TestObjBase)
                .AddFunction(funcSrc)
                .Create();

            builder.EvaluateCodeBlock("dialogclose(d_test, 123)");

            var output = builder.TestObjBase.GetOutput();
            output.Should().Contain("closedialog d_test, 123");
        }

        [TestMethod]
        public void Can_use_argv_as_index_for_indexed_symbols()
        {
            var evaluator = new TestEvaluator();
            var funcSrc = @"
[FUNCTION myfunc]
src.sysmessage(<symbol[argv(0)]>)
";

            evaluator.SetSrc(evaluator.TestObjBase)
                .AddFunction(funcSrc)
                .AddNameDef("symbol[0]", "index zero")
                .AddNameDef("symbol[1]", "index one")
                .Create();

            evaluator.EvaluateCodeBlock("myfunc(1)");
            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage index one");
        }

        [TestMethod]
        public void Defnames_are_case_insensitive()
        {
            var evaluator = new TestEvaluator();

            evaluator.SetSrc(evaluator.TestObjBase)
                .AddNameDef("Symbol[0]", "index zero")
                .AddNameDef("symbol[1]", "index one")
                .SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("src.sysmessage(<symbol[0]>)");
            evaluator.EvaluateCodeBlock("src.sysmessage(<Symbol[1]>)");
            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage index zero");
            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage index one");
        }

        [TestMethod]
        public void Can_pass_macro_argument()
        {
            var evaluator = new TestEvaluator();

            evaluator
                .AddNameDef("def_class[0]", "argument1")
                .SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("TAG(class,<def_class[0]>)");

            string output = evaluator.TestObjBase.GetOutput();
            output.Should().Contain("tag class, argument1");
        }

        [TestMethod]
        public void Can_pass_literal_argument_with_macro_only()
        {
            var evaluator = new TestEvaluator();

            evaluator
                .AddNameDef("def_class[0]", "argument1")
                .SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("TAG(class,\"<def_class[0]>\")");

            string output = evaluator.TestObjBase.GetOutput();
            output.Should().Contain("tag class, argument1");
        }

        [TestMethod]
        public void Can_pass_literal_argument_with_macro()
        {
            var evaluator = new TestEvaluator();

            evaluator
                .AddNameDef("def_class[0]", "argument1")
                .SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("TAG(class,\"arg_<def_class[0]>\")");

            string output = evaluator.TestObjBase.GetOutput();
            output.Should().Contain("tag class, arg_argument1");
        }

        [TestMethod]
        public void Can_pass_literal_argument_with_nested_macros()
        {
            var evaluator = new TestEvaluator();

            evaluator
                .AddNameDef("def_class[0]", "argument1")
                .AddNameDef("macro", "def_class")
                .SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("TAG(class,\"arg_<<macro>[0]>\")");

            string output = evaluator.TestObjBase.GetOutput();
            output.Should().Contain("tag class, arg_argument1");
        }
    }
}
