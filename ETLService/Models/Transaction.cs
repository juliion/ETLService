using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Models
{
    public class Transaction
    {
        public string City { get; set; }
        public Services Services { get; set; }
        public decimal Total { get; set; }
    }
}
