using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Models
{
    public class Transaction
    {
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Payment { get; set; }
        public DateTime Date { get; set; }
        public long AccountNumber { get; set; }
        public string Service { get; set; }
    }
}
