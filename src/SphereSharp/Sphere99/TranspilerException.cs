using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99
{

    [Serializable]
    public class TranspilerException : Exception
    {
        public TranspilerException() { }
        public TranspilerException(string message) : base(message) { }
        public TranspilerException(string message, Exception inner) : base(message, inner) { }
        protected TranspilerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
