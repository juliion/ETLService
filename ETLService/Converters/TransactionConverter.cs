using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using ETLService.Models;

namespace ETLService.Converters
{
    public enum FieldsIndices
    {
        nameInd = 0,
        surnameInd = 1,
        cityInd = 2,
        payentInd = 6,
        dateInd = 7,
        accountNumInd = 8,
        serviceInd = 9
    }
    public class TransactionConverter
    {
        public Transaction StrToTransaction(string strTransaction)
        {
            string[] fields = strTransaction.Split(new char[] { ' ', ',', '“', '”' }, StringSplitOptions.RemoveEmptyEntries);
            Transaction transaction = new Transaction()
            {
                Name = fields[(int)FieldsIndices.nameInd] + " " + fields[(int)FieldsIndices.surnameInd],
                City = fields[(int)FieldsIndices.cityInd],
                Payment = decimal.Parse(fields[(int)FieldsIndices.payentInd], CultureInfo.InvariantCulture),
                Date = DateTime.ParseExact(fields[(int)FieldsIndices.dateInd], "yyyy-mm-dd", CultureInfo.InvariantCulture),
                AccountNumber = long.Parse(fields[(int)FieldsIndices.accountNumInd]),
                Service = fields[(int)FieldsIndices.serviceInd]
            };
            return transaction;
        }
    }
}
