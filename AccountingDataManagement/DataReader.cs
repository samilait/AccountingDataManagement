using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AccountingDataManagement
{
    class DataReader
    {
        private DateTime statusDate;
        private List<string> rawData;
        private string accountLeadingChar;
        private string accountLeadingCheck;
        private int accountIdLength;
        private Dictionary<int, Account> accounts;
        private char defSeparator;
        private Dictionary<string, int[]> transactionDef;

        public DataReader(DateTime statusDate)
        {
            this.statusDate = statusDate;
            rawData = new List<string>();
            accountIdLength = 4;
            accountLeadingChar = "-";
            accountLeadingCheck = GetAccountLeadingString();

            accounts = new Dictionary<int, Account>();
            defSeparator = ',';
            transactionDef = new Dictionary<string, int[]>();
        }

        public void ReadTransactionDef(string fileName)
        {
            // Read transaction def into dictionary: var, start position, end position
            StreamReader reader = new StreamReader(fileName);
            string header = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(defSeparator);
                int[] pos = new int[2];
                pos[0] = Convert.ToInt32(line[1]);
                pos[1] = Convert.ToInt32(line[2]);
                transactionDef.Add(line[0], pos);
            }

            reader = null;
        }

        public void ReadRawData(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);

            while (!reader.EndOfStream)
            {
                string row = reader.ReadLine();
                if (row.Length > 0)
                {
                    rawData.Add(row);
                }
            }

            reader = null;
        }

        public string GetAccountLeadingString()
        {
            string accountLeadingCheck = "";
            for (int i = 0; i < accountIdLength; i++)
            {
                accountLeadingCheck += accountLeadingChar;
            }

            return accountLeadingCheck;

        }

        public Ledger ParseAccountsTransactions(Ledger ledger)
        {
            Account account = null;
            int accountId = 0;

            // Get account id's and names
            for (int i = 0; i < rawData.Count; i++)
            {

                string prevRow = rawData[Math.Max(0, i - 1)];
                string row = rawData[i];

                // New account
                if (prevRow.Substring(0, accountIdLength).Equals(accountLeadingCheck))
                {
                    accountId = Convert.ToInt32(row.Substring(0, accountIdLength));
                    string accountName = row.Substring(accountIdLength, row.Length - accountIdLength).Trim();
                    account = new Account(accountId, accountName);
                    continue;
                }

                // Account start balance
                if (account != null && row.Trim().Substring(0, 9).Equals("ALKUSALDO"))
                {
                    string tmp = GetDefString("balance", row).Trim();
                    double startBalance = 0.0;
                    if (!tmp.Equals("---"))
                    {
                        startBalance = Convert.ToDouble(tmp);
                    }
                    account.StartBalance = startBalance;
                    continue;
                }

                if (row.Trim().Substring(0, 5).Equals("KAUSI"))
                {
                    continue;
                }

                // End balance and add account to account list
                if (row.Trim().Substring(0, 5).Equals("VUOSI"))
                {
                    string tmp = GetDefString("balance", row).Trim();
                    double endBalance = 0.0;
                    if (!tmp.Equals("---"))
                    {
                        endBalance = Convert.ToDouble(tmp);
                    }
                    account.EndBalance = endBalance;
                    ledger.AddAccount(accountId, account);
                    continue;
                }

                // Add transaction to account
                if (account != null && !row.Substring(0, 4).Equals("----"))
                {
                    string tmp;
                    string id = GetDefString("id", row).Trim();
                    string description = GetDefString("description", row).Trim();
                    DateTime date = GetDate(row);

                    tmp = GetDefString("debet", row).Trim();
                    double debit = tmp.Equals("") ? 0.0 : Convert.ToDouble(tmp);
                    tmp = GetDefString("kredit", row).Trim();
                    double credit = tmp.Equals("") ? 0.0 : Convert.ToDouble(tmp);
                    tmp = GetDefString("balance", row).Trim();
                    double balance = (tmp.Equals("---") || tmp.Equals("")) ? 0.0 : Convert.ToDouble(tmp);

                    Transaction transaction = new Transaction(id, description, date, debit, credit, balance);
                    account.AddTransaction(transaction);

                }

            }

            return ledger;

        }

        public DateTime GetDate(string row)
        {
            string[] ddMM = GetDefString("date", row).Split('.');
            int month = Convert.ToInt32(ddMM[1]);
            int day = Convert.ToInt32(ddMM[0]);

            if (month > statusDate.Month)
            {
                return new DateTime(statusDate.Year, month, day);
            }
            else
            {
                return new DateTime(statusDate.Year + 1, month, day);
            }
        }

        public string GetDefString(string def, string row)
        {
            return row.Substring((transactionDef[def])[0], (transactionDef[def])[1] - (transactionDef[def])[0]);
        }

    }
}
