using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.Accounts
{
    public class AccountsVisitor : sphereScript99BaseVisitor<string>
    {
        private List<Account> accounts = new List<Account>();
        private Account currentAccount;

        public IEnumerable<Account> Accounts => accounts;

        public override string VisitAccountSection([NotNull] sphereScript99Parser.AccountSectionContext context)
        {
            var accountName = context.accountSectionHeader().accountSectionName().GetText();

            currentAccount = new Account { Name = accountName };
            var result = base.VisitAccountSection(context);
            accounts.Add(currentAccount);
            currentAccount = null;

            return result;
        }

        public override string VisitPropertyAssignment([NotNull] sphereScript99Parser.PropertyAssignmentContext context)
        {
            var name = context.propertyName().GetText();

            switch (name.ToLower())
            {
                case "email":
                    currentAccount.Email = context.propertyValue().GetText();
                    break;
                case "lastconnectdate":
                    currentAccount.LastConnectDate = DateTime.Parse(context.propertyValue().GetText().Trim('"'));
                    break;
            }

            return base.VisitPropertyAssignment(context);
        }
    }
}
