using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTex
{
    class InvoiceItem
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal ValueNetto { get { return Quantity * Item.UnitPrice; } }
        public decimal VATValue { get { return ValueNetto * Item.VatRate / 100; } }
        public decimal ValueBrutto { get { return ValueNetto + VATValue; } }
    }
}
