using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Loaders;
using ETLService.Models;

namespace ETLService.Loaders.Writers
{
    public abstract class FileWriter : ILoader
    {
        protected readonly string _filename;

        protected FileWriter(string filename)
        {
            _filename = filename;
        }

        public abstract void Load(List<TransactionsByCity> transactionsByCities);
    }
}
