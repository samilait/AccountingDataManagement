using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDataManagement
{
    class Transaction
    {
        private string id, description;
        private DateTime date;
        private double debit, kredit, balance;

        public Transaction(string id, string description, DateTime date, double debit, double kredit, double balance)
        {
            this.id = id;
            this.description = description;
            this.date = date;
            this.debit = debit;
            this.kredit = kredit;
            this.balance = balance;
        }

    }
}
