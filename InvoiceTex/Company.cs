using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTex
{
    class Company : Contractor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string REGON { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string IssuerName { get; set; }
    }
}
