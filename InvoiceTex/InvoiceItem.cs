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
        public decimal ValueNetto { get { return getValueNetto(); } }
        public decimal VATValue { get { return getVATValue(); } }
        public decimal ValueBrutto { get { return getValueBrutto();  } }

        decimal getValueNetto()
        {
            return Quantity * Item.UnitPrice;
        }

        decimal getVATValue()
        {
            return ValueNetto * Item.VatRate / 100;
        }

        decimal getValueBrutto()
        {
            return ValueNetto + VATValue;
        }
    }
}
