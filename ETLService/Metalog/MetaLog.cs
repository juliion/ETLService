using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Metalog
{
    [Serializable]
    public class MetaLog
    {
        public int ParsedFiles { get; set; }
        public int ParsedLines { get; set; }
        public int FoundErrors { get; set; }
        public List<string> InvalidFiles { get; set; } = new List<string>();
    }
}
