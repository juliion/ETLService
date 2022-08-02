using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Models
{
    [Serializable]
    public class TransactionsByCity
    {
        public string City { get; set; }
        public List<Service> Services { get; set; }
        public decimal Total { get; set; }
    }
}
