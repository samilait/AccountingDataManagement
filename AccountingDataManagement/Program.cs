using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDataManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName;
            DataReader dataReader = new DataReader(new DateTime(2017, 5, 31));
            fileName = @"C:\Users\Sami\Documents\HaKi\transaction.def";
            dataReader.ReadTransactionDef(fileName);
            fileName = @"C:\Users\Sami\Documents\HaKi\Harjun Kiekko ry  PAAKIRJA TP1 koko kausi clean v2.txt";
            dataReader.ReadRawData(fileName);
            Ledger ledger = dataReader.ParseAccountsTransactions(new Ledger(new DateTime(2017, 5, 31)));

            fileName = @"C:\Users\Sami\Documents\HaKi\ledger.csv";
            ledger.WriteLedgerToFile(fileName);
        }
    }
}
