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
            DataReader dataReader = new DataReader();
            fileName = @"C:\Users\Sami\Documents\HaKi\transaction.def";
            dataReader.ReadTransactionDef(fileName);
            fileName = @"C:\Users\Sami\Documents\HaKi\Harjun Kiekko ry  PAAKIRJA TP1 koko kausi clean.txt";
            dataReader.ReadRawData(fileName);
            dataReader.GetAccounts();
        }
    }
}
