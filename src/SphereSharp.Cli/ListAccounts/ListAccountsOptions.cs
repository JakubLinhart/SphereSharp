using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli
{
    [Verb("list-accounts")]
    public class ListAccountsOptions
    {
        [Value(0, Required = true, HelpText = "Input sphereaccu.scp file.")]
        public string Input { get; set; }
    }
}
