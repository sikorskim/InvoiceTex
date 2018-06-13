using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTex
{
    public class Contractor
    {
        public int Id { get; set; }
        public string NIP { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuldingNo { get; set; }
        public string PostalCode { get; set; }

        public string FullAddress
        {
            get { return "ul. " + Street + " " + BuldingNo + ", " + PostalCode + " " + City; }
        }
    }
    }
