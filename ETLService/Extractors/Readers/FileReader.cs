using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Models;
using ETLService.Converters;

namespace ETLService.Extractors.Readers
{
    public abstract class FileReader : IExtractor
    {
        protected readonly string _filename;
        protected TransactionConverter _converter;

        protected FileReader(string filename, TransactionConverter converter)
        {
            _filename = filename;
            _converter = converter;
        }

        public abstract List<Transaction> Extract();
    }
}
