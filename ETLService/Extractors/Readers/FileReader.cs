using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Models;
using ETLService.Converters;
using ETLService.Metalog;

namespace ETLService.Extractors.Readers
{
    public abstract class FileReader : IExtractor
    {
        protected readonly string _filename;
        protected TransactionConverter _converter;
        protected MetaLog _metaLog; 
        protected FileReader(string filename, TransactionConverter converter, ref MetaLog metaLog)
        {
            _filename = filename;
            _converter = converter;
            _metaLog = metaLog;
        }

        public abstract List<Transaction> Extract();
    }
}
