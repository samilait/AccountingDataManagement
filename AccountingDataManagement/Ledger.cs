using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AccountingDataManagement
{
    class Ledger
    {
        private DateTime statusDate;
        private Dictionary<int, Account> accounts;

        public Ledger(DateTime statusDate)
        {
            this.statusDate = statusDate;
            this.accounts = new Dictionary<int, Account>();
        }

        public void AddAccount(int accountId, Account account)
        {
            this.accounts.Add(accountId, account);
        }

        public void WriteLedgerToFile(string fileName)
        {

            StringBuilder sb = new StringBuilder();

            foreach (int id in accounts.Keys)
            {
                Account account = accounts[id];
                foreach (Transaction transaction in account.Transactions)
                {
                    string str = account.Id + "," + account.Name + "," + transaction.ToString();
                    sb.AppendLine(str);
                }
            }

            File.WriteAllText(fileName, sb.ToString());

        }

    }
}
