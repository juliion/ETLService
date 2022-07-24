using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Models
{
    public class Payer
    {
        public string Name { get; set; }
        public decimal Payment { get; set; }
        public DateTime Data { get; set; }
        public long AccountNumber { get; set; }
    }
}
