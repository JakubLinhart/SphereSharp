using SphereSharp.Cli.Accounts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli
{
    public class ListAccountsCommand
    {
        public void List(ListAccountsOptions options)
        {
            var compilation = new Compilation();
            compilation.AddAccountsSaveFile(options.Input, File.ReadAllText(options.Input));
            var visitor = new AccountsVisitor();
            visitor.Visit(compilation.CompiledAccountSaveFile.ParsedTree);

            foreach (var account in visitor.Accounts.OrderBy(a => a.LastConnectDate))
            {
                Console.WriteLine($"\"{account.Name}\",{account.Email},\"{account.LastConnectDate:yyyy-MM-dd}\"");
            }
        }
    }
}
