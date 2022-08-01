using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ETLService.Models;
using ETLService.Converters;

namespace ETLService.Extractors.Readers
{
    public class TxtReader : FileReader
    {
        public TxtReader(string filename, TransactionConverter converter) : base(filename, converter)
        {
        }

        public override List<Transaction> Extract()
        {
            List<Transaction> transactions = new List<Transaction>();
            using (StreamReader reader = new StreamReader(_filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Transaction transaction = _converter.StrToTransaction(line);
                    transactions.Add(transaction);
                }
            }
            return transactions;
        }
    }
}
