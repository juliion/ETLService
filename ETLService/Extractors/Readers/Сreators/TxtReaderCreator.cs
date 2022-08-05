using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Converters;
using ETLService.Metalog;

namespace ETLService.Extractors.Readers.Сreators
{
    public class TxtReaderCreator : FileReaderCreator
    {
        public override FileReader CreateReader(string filename, ref MetaLog metaLog)
        {
            return new TxtReader(filename, new TransactionConverter(), ref metaLog);
        }
    }
}
