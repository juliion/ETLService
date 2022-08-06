using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Models;

namespace ETLService.Loaders
{
    public interface ILoader
    {
        public void Load(List<TransactionsByCity> transactionsByCities);
    }
}
