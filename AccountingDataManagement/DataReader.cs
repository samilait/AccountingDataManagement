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
        private List<string> rawData;
        private string accountLeadingChar;
        private int accountIdLength;
        private List<Account> accounts;

        public DataReader()
        {
            rawData = new List<string>();
            accountIdLength = 4;
            accountLeadingChar = "-";
            accounts = new List<Account>();
        }

        public void ReadRawData(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);

            while (!reader.EndOfStream)
            {
                rawData.Add(reader.ReadLine());
            }

            reader = null;
        }

        public void GetAccounts()
        {
            // Account id's and names

            // Prepare account leading row check string
            string accountLeadingCheck = "";
            for (int i = 0; i < accountIdLength; i++)
            {
                accountLeadingCheck += accountLeadingChar;
            }

            // Get account id's and names
            for (int i = 0; i < rawData.Count; i++)
            {
                string row = rawData[i];
                if (row.Length > 0)
                {
                    if (row.Substring(0, accountIdLength).Equals(accountLeadingCheck))
                    {
                        string accountRow = rawData[i + 1];
                        int accountId = Convert.ToInt32(accountRow.Substring(0, accountIdLength));
                        string accountName = accountRow.Substring(accountIdLength, accountRow.Length - accountIdLength).Trim();

                        Account account = new Account(accountId, accountName);
                        accounts.Add(account);

                    }
                }
            }

        }

    }
}
