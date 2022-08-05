using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Converters;
using ETLService.Metalog;

namespace ETLService.Extractors.Readers.Сreators
{
    public class CsvReaderCreator : FileReaderCreator
    {
        public override FileReader CreateReader(string filename, ref MetaLog metaLog)
        {
            return new CsvReader(filename, new TransactionConverter(), ref metaLog);
        }
    }
}
