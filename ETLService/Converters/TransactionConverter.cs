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
            string name = fields[_fieldsIndices["name"]] + " " + fields[_fieldsIndices["surname"]];
            string city = fields[_fieldsIndices["city"]];
            decimal payment;
            DateTime date;
            long accountNum;
            string service = fields[_fieldsIndices["service"]];

            bool nameIsValid = name != " ";
            bool cityIsValid = city != " ";
            bool paymentParsed = decimal.TryParse(fields[_fieldsIndices["payment"]], NumberStyles.Any, CultureInfo.InvariantCulture, out payment);
            bool dateParsed = DateTime.TryParse(fields[_fieldsIndices["date"]], out date);
            bool accountNumParsed = long.TryParse(fields[_fieldsIndices["accountNum"]], out accountNum);
            bool serviceIsValid = service != " ";

            bool isValid = nameIsValid && cityIsValid && paymentParsed && dateParsed && accountNumParsed && serviceIsValid;

            if (isValid)
            {
                return new Transaction()
                {
                    Name = name,
                    City = city,
                    Payment = payment,
                    Date = date,
                    AccountNumber = accountNum,
                    Service = service
                };
            }
            else
                throw new Exception("Row isn't valid");
        }
    }
}
