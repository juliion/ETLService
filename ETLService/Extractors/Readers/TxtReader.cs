using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ETLService.Models;
using ETLService.Converters;
using ETLService.Metalog;

namespace ETLService.Extractors.Readers
{
    public class TxtReader : FileReader
    {
        public TxtReader(string filename, TransactionConverter converter, ref MetaLog metaLog) : base(filename, converter, ref metaLog)
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
                    try
                    {
                        Transaction transaction = _converter.StrToTransaction(line);
                        transactions.Add(transaction);
                        _metaLog.ParsedLines++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        _metaLog.FoundErrors++;
                        if (!_metaLog.InvalidFiles.Contains(_filename))
                            _metaLog.InvalidFiles.Add(_filename);
                    }
                }
            }
            return transactions;
        }
    }
}
