using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTex
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DueDate { get; set; }
    }
}
