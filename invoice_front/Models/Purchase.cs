using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoice_front.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public string PurchaseNo { get; set; }
        public decimal Amount { get; set; }
        public string PIC { get; set; }
    }
}
