using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTex
{
    class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VatRate { get; set; }
    }
}
