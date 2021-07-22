using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoice_front.Models
{
    public class AllData
    {
        public int Number { get; set; }
        public List<Currency> ListOfCurrency { get; set; }
        public List<Language> ListOfLanguage { get; set; }
        public List<UOM> ListOfUOM { get; set; }
        public List<Customer> ListOfCustomer { get; set; }
    }
}
