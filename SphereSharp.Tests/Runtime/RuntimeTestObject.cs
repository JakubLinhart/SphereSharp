using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Runtime
{
    public abstract class RuntimeTestObject
    {
        private StringBuilder outputBuilder = new StringBuilder();

        public string GetOutput() => outputBuilder.ToString();

        public void WriteLine(string text) => outputBuilder.AppendLine(text);
    }
}
