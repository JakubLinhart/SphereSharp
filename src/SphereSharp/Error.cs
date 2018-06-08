using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp
{
    public class Error
    {
        public Error(string message, int line, int column, string fileName=null)
        {
            Message = message;
            Line = line;
            Column = column;
            FileName = fileName;
        }

        public string Message { get; }
        public int Line { get; }
        public int Column { get; }
        public string FileName { get; }
    }
}
