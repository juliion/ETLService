using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Models;

namespace ETLService.Extractors
{
    public interface IExtractor
    {
        public List<Transaction> Extract();
    }
}
