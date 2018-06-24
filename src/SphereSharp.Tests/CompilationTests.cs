using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests
{
    [TestClass]
    public class CompilationTests
    {
        [TestMethod]
        public void Collects_global_variables_from_VarNames_section()
        {
            var compilation = new Compilation();
            compilation.AddWorldSaveFile("sphereworld.scp",
@"[VarNames]
lightlevel=1
middle=165
spawnuid=#040000013
nastaveni_splitShot_delay=450
");

            compilation.DefinitionRepository.IsGlobalVariable("lightlevel").Should().BeTrue();
        }
    }
}
