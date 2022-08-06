using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Models
{
    [Serializable]
    public class Service
    {
        public string Name { get; set; }
        public Payers Payers { get; set; }
        public decimal Total { get; set; }
    }
}
