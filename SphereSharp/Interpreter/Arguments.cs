using SphereSharp.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SphereSharp.Interpreter
{
    public class Arguments : IEnumerable<string>
    {
        private List<string> values = new List<string>();
        private Dictionary<int, string> txtValues = new Dictionary<int, string>();

        public Arguments()
        {
        }

        internal void Add(string text)
        {
            values.Add(text);
        }

        public void AddTxt(int id, string value)
        {
            txtValues[id] = value;
        }

        public string ArgS(int index) => values[index];
        public int ArgInt(int index) => int.Parse(values[index]);

        public IEnumerator<string> GetEnumerator() => values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => values.GetEnumerator();

        public int ArgN { get; set; }
        public int Count => values.Count;

        internal string ArgTxt(int id) => txtValues[id];
    }
}
