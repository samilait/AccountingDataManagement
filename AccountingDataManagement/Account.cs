using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDataManagement
{
    class Account
    {
        private int id;
        private string name;
        private double startBalance;
        private double endBalance;
        private List<Transaction> transactions;

        public Account(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Transactions = new List<Transaction>();
        }

        public double StartBalance { get => startBalance; set => startBalance = value; }
        public double EndBalance { get => endBalance; set => endBalance = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        internal List<Transaction> Transactions { get => transactions; set => transactions = value; }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

    }
}
