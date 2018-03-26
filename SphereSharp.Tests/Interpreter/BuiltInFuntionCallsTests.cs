using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SphereSharp.Tests.Interpreter
{
    [TestClass]
    public class BuiltInFuntionCallsTests
    {
        private TestEvaluator evaluator;

        [TestInitialize]
        public void Initialize()
        {
            evaluator = new TestEvaluator();
            evaluator
                .SetArgO(evaluator.TestGump)
                .SetSrc(evaluator.TestObjBase)
                .Create();
        }

        [TestMethod]
        public void Can_call_dialog_without_arguments()
        {
            evaluator.EvaluateCodeBlock("src.DIALOG D_RACEclass_background");

            evaluator.TestObjBase.GetOutput().Should().Contain("dialog D_RACEclass_background");
        }

        [TestMethod]
        public void Can_call_dialog_with_arguments()
        {
            evaluator.EvaluateCodeBlock("src.DIALOG(D_RACEclass_background, 1, 2, 3)");

            evaluator.TestObjBase.GetOutput().Should().Contain("dialog D_RACEclass_background, 1, 2, 3");
        }

        [TestMethod]
        public void Can_call_closedialog()
        {
            evaluator.EvaluateCodeBlock("src.closedialog(D_RACEclass_background, 0)");

            evaluator.TestObjBase.GetOutput().Should().Contain("closedialog D_RACEclass_background, 0");
        }

        [TestMethod]
        public void Can_call_gumppic()
        {
            evaluator.SetDefault(evaluator.TestGump)
                .Create();
            evaluator.EvaluateCodeBlock("gumppic 510 110 5536");

            evaluator.TestGump.GetOutput().Should().Contain("gumppic 510, 110, 5536");
        }

        [TestMethod]
        public void Can_call_setlocation()
        {
            evaluator.SetDefault(evaluator.TestGump)
                .Create();

            evaluator.EvaluateCodeBlock("argo.SetLocation(0,0)");

            evaluator.TestGump.GetOutput().Should().Contain("setlocation 0, 0");
        }

        [TestMethod]
        public void Can_call_resizepic()
        {
            evaluator.SetDefault(evaluator.TestGump)
                .Create();

            evaluator.EvaluateCodeBlock("resizepic 0 0 2600 640 480");

            evaluator.TestGump.GetOutput().Should().Contain("resizepic 0, 0, 2600, 640, 480");
        }

        [TestMethod]
        public void Can_call_texta()
        {
            evaluator.SetDefault(evaluator.TestGump)
                .Create();

            evaluator.EvaluateCodeBlock("argo.texta(180,233,1000,\"test text\")");

            evaluator.TestGump.GetOutput().Should().Contain("texta 180, 233, 1000, test text");
        }

        [TestMethod]
        public void Can_call_textentry()
        {
            evaluator.SetDefault(evaluator.TestGump)
                .Create();

            evaluator.EvaluateCodeBlock("argo.textentry(340,233,300,30,1000,100,10)");

            evaluator.TestGump.GetOutput().Should().Contain("textentry 340, 233, 300, 30, 1000, 100, 10");
        }

        [TestMethod]
        public void Can_call_settext()
        {
            evaluator.SetDefault(evaluator.TestGump)
                .Create();

            evaluator.EvaluateCodeBlock("argo.settext(10,\"some text\")");

            evaluator.TestGump.GetOutput().Should().Contain("settext 10, some text");
        }

        [TestMethod]
        public void Can_call_htmlgump()
        {
            evaluator.SetDefault(evaluator.TestGump)
                .Create();

            evaluator.EvaluateCodeBlock("HTMLGUMP(20,20,600,200,0,0,0)");

            evaluator.TestGump.GetOutput().Should().Contain("htmlgump 20, 20, 600, 200");
        }

        [TestMethod]
        public void Can_call_htmlgumpa()
        {
            evaluator.SetDefault(evaluator.TestGump)
                .Create();

            evaluator.EvaluateCodeBlock(@"HTMLGUMPa(365,215,110,160,""test"",0,0)");

            evaluator.TestGump.GetOutput().Should().Contain(@"htmlgumpa 365, 215, 110, 160, ""test""");
        }

        [TestMethod]
        public void Can_call_strlen()
        {
            var result = evaluator.EvaluateCall(@"strlen(asdf)");
            result.Should().Be("4");
        }

        [TestMethod]
        public void Can_call_sysmessage()
        {
            evaluator.EvaluateCodeBlock("src.sysmessage asdf,qwer");
            string output = evaluator.TestObjBase.GetOutput();

            output.Should().Contain("sysmessage asdf,qwer");
        }

        [TestMethod]
        public void Can_set_tag()
        {
            evaluator.SetDefault(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("tag(class,something)");
            string output = evaluator.TestObjBase.GetOutput();
            output.Should().Contain("tag class, something");
        }

        [TestMethod]
        public void Can_read_tag()
        {
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("tag(class,something)");
            evaluator.EvaluateCodeBlock("src.sysmessage(<tag(class)>)");
            string output = evaluator.TestObjBase.GetOutput();
            output.Should().Contain("sysmessage something");
        }

        [TestMethod]
        public void Can_read_tag_using_dot_notation()
        {
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("tag(class,something)");
            evaluator.EvaluateCodeBlock("src.sysmessage(<tag.class>)");
            string output = evaluator.TestObjBase.GetOutput();
            output.Should().Contain("sysmessage something");
        }

        [TestMethod]
        public void Can_remove_tag()
        {
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("tag(class,something)");
            evaluator.EvaluateCodeBlock("tag.remove(class)");
            evaluator.EvaluateCodeBlock("src.sysmessage(<tag.class>)");

            string output = evaluator.TestObjBase.GetOutput();
            output.Should().Contain("sysmessage");
            output.Should().NotContain("sysmessage something");
        }

        [TestMethod]
        public void Can_call_strcmpi_for_equal_strings()
        {
            var result = evaluator.EvaluateCall("strcmpi(asdf, asdf)");
            result.Should().Be("0");
        }

        [TestMethod]
        public void Can_call_strcmpi_for_equal_strings_with_different_casing()
        {
            var result = evaluator.EvaluateCall("strcmpi(asdf, ASDF)");
            result.Should().Be("0");
        }

        [TestMethod]
        public void Can_call_strcmpi_for_non_equal_strings()
        {
            var result = evaluator.EvaluateCall("strcmpi(asdf, qwer)");
            result.Should().NotBe("0");
        }

        [TestMethod]
        public void Can_define_and_read_arg()
        {
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"arg(seznamnations, asdf)
src.sysmessage(<arg(seznamnations)>)
");
            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage asdf");
        }

        [TestMethod]
        public void Can_define_and_read_indexed_arg()
        {
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"arg(seznamnations[0], asdf)
src.sysmessage(<arg(seznamnations[0])>)
");
            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage asdf");
        }
    }
}
