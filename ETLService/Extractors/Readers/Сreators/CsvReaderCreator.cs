using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Converters;

namespace ETLService.Extractors.Readers.Сreators
{
    public class CsvReaderCreator : FileReaderCreator
    {
        public override FileReader CreateReader(string filename)
        {
            return new CsvReader(filename, new TransactionConverter());
        }
    }
}
