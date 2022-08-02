using ETLService.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Loaders.Writers
{
    public class JSONWriter : FileWriter
    {
        public JSONWriter(string filename) : base(filename)
        {
        }

        public override void Load(List<TransactionsByCity> transactionsByCities)
        {
            string jsonString = JsonSerializer.Serialize(transactionsByCities);
            File.WriteAllText(_filename, jsonString);
        }
    }
}
