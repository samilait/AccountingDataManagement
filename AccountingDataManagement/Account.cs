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
            this.id = id;
            this.name = name;
            this.transactions = new List<Transaction>();
        }

        public double StartBalance { get => startBalance; set => startBalance = value; }
        public double EndBalance { get => endBalance; set => endBalance = value; }

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

    }
}
