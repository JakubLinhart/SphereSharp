using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli
{
    public class TranspileCommandFileNameHandler
    {
        private TranspileOptions transpileOptions;

        public TranspileCommandFileNameHandler(TranspileOptions transpileOptions)
        {
            this.transpileOptions = transpileOptions;
        }

        public string GetOututFileNameFromInput(string inputFileName)
        {
            string outputFilePath = string.IsNullOrEmpty(transpileOptions.OutputPath) ? inputFileName
                : Path.Combine(transpileOptions.OutputPath,
                    inputFileName.Substring(transpileOptions.InputPath.Length, inputFileName.Length - transpileOptions.InputPath.Length));

            if (!string.IsNullOrEmpty(transpileOptions.OutputSuffix))
            {
                if (transpileOptions.OutputSuffix.StartsWith("."))
                    outputFilePath += transpileOptions.OutputSuffix;
                else
                    outputFilePath = outputFilePath + "." + transpileOptions.OutputSuffix;
            }

            return outputFilePath;
        }
    }
}
