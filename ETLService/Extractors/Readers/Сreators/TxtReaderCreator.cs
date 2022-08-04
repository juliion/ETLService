using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Converters;

namespace ETLService.Extractors.Readers.Сreators
{
    public class TxtReaderCreator : FileReaderCreator
    {
        public override FileReader CreateReader(string filename)
        {
            return new TxtReader(filename, new TransactionConverter());
        }
    }
}
