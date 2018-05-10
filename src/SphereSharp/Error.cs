using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp
{
    public class Error
    {
        public Error(string message, int line, int column)
        {
            Message = message;
            Line = line;
            Column = column;
        }

        public string Message { get; }
        public int Line { get; }
        public int Column { get; }
    }
}
