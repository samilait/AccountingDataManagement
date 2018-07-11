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
            this.Id = id;
            this.Description = description;
            this.Date = date;
            this.Debit = debit;
            this.Kredit = kredit;
            this.Balance = balance;
        }

        public override string ToString()
        {
            return id + "," + description + "," + date.ToString("dd.MM.yyyy") + "," + debit + "," + kredit + "," + balance;
        }

        public string Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public DateTime Date { get => date; set => date = value; }
        public double Debit { get => debit; set => debit = value; }
        public double Kredit { get => kredit; set => kredit = value; }
        public double Balance { get => balance; set => balance = value; }
    }
}
