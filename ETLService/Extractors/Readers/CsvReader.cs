using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ETLService.Models;
using ETLService.Converters;

namespace ETLService.Extractors.Readers
{
    public class CsvReader : FileReader
    {
        public CsvReader(string filename, TransactionConverter converter) : base(filename, converter)
        {
        }

        public override List<Transaction> Extract()
        {
            List<Transaction> transactions = new List<Transaction>();
            using (StreamReader reader = new StreamReader(_filename))
            {
                string line;
                bool isHeader = true;
                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                        isHeader = false;
                    else
                    {
                        Transaction transaction = _converter.StrToTransaction(line);
                        transactions.Add(transaction);
                    }
                }
            }
            return transactions;
        }
    }
}
