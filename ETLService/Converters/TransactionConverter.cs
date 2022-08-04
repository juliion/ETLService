using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using ETLService.Models;

namespace ETLService.Converters
{
    public class TransactionConverter
    {
        private readonly Dictionary<string, int> _fieldsIndices;
        public TransactionConverter()
        {
            _fieldsIndices = new Dictionary<string, int>()
            {
                ["name"] = 0,
                ["surname"] = 1,
                ["city"] = 2,
                ["payment"] = 6,
                ["date"] = 7,
                ["accountNum"] = 8,
                ["service"] = 9
            };
        }
        public Transaction StrToTransaction(string strTransaction)
        {
            string[] fields = strTransaction.Split(new char[] { ' ', ',', '“', '”' }, StringSplitOptions.RemoveEmptyEntries);
            Transaction transaction = new Transaction()
            {
                Name = fields[_fieldsIndices["name"]] + " " + fields[_fieldsIndices["surname"]],
                City = fields[_fieldsIndices["city"]],
                Payment = decimal.Parse(fields[_fieldsIndices["payment"]], CultureInfo.InvariantCulture),
                Date = DateTime.Parse(fields[_fieldsIndices["date"]]),
                AccountNumber = long.Parse(fields[_fieldsIndices["accountNum"]]),
                Service = fields[_fieldsIndices["service"]]
            };
            return transaction;
        }
    }
}
