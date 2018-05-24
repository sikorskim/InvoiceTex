using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTex
{
    class Invoice
    {
        public string Number { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string IssuePlace { get; set; }
        public Company Company { get; set; }
        public Contractor Contractor { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
