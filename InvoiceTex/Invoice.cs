using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InvoiceTex
{
    class Invoice
    {
        public string Number { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public string IssuePlace { get; set; }
        public Company Company { get; set; }
        public Contractor Contractor { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public decimal TotalVATValue { get { return getTotalVatValue(); } }
        public decimal TotalValueNetto { get { return getTotalValueNetto(); } }
        public decimal TotalValueBrutto { get { return getTotalValueBrutto(); } }
        public string TotalValueInWords { get { return getTotalValueInWords(); } }

        public static string getNumber()
        {
            return "FV/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
        }

        decimal getTotalVatValue()
        {
            return InvoiceItems.Sum(p => p.VATValue);
        }

        decimal getTotalValueNetto()
        {
            return InvoiceItems.Sum(p => p.ValueNetto);
        }

        decimal getTotalValueBrutto()
        {
            return InvoiceItems.Sum(p => p.ValueBrutto);
        }

       public static string getTotalValueInWords()
        {
            string[] value;


             //   TotalValueBrutto.ToString().Split('.');
            //TotalValueBrutto.ToString().Split(',');


            //string beforePoint = ;

            string valueInWords="";

            string[] ones = { "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć" };
            string[] teens = { "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemanście", "dziewiętnaście" };
            string[] doubles = { "dziesięć", "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
            string[] triplets = { "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
            string[] thousands = {"tysiąc", "tysięcy" };
            string[] millions = { "milion", "milionów" };



            return valueInWords;
        }

        public void generate()
        {
            string path = "invoiceTemplate.xml";
            XDocument doc = XDocument.Load(path);
            XElement root = doc.Element("Template");

            string header = root.Element("Header").Value;
            header = string.Format(header, Company.Name, Company.FullAddress, Company.NIP, Company.REGON, Company.Phone, Company.Email, Company.Website, Company.BankName, Company.BankAccount);

            string cityOfIssue = root.Element("DatePlace").Value;
            string dateOfIssue = DateOfIssue.ToShortDateString();
            string dateOfDelivery = DateOfDelivery.ToShortDateString();
            cityOfIssue = string.Format(cityOfIssue, dateOfIssue, IssuePlace, dateOfDelivery);

            string title = root.Element("Title").Value;
            title = string.Format(title, Number);

            string sellerBuyer = root.Element("SellerBuyer").Value;
            sellerBuyer = string.Format(sellerBuyer, Company.FullName, Company.FullAddress, Company.NIP, Contractor.FullName, Contractor.FullAddress, Contractor.NIP);

            header += cityOfIssue += title += sellerBuyer;

            string invoiceItemsTable = root.Element("InvoiceItemsTableHeader").Value;
            string invoiceItemsSummary = root.Element("InvoiceItemTableSummary").Value;
            string invoiceItem = root.Element("InvoiceItem").Value;

            int i = 1;
            foreach (InvoiceItem item in InvoiceItems)
            {
                string newItem = string.Format(invoiceItem, i, item.Item.Name, item.Item.UnitOfMeasure, item.Quantity, item.Item.UnitPrice.ToString("0.00"), item.ValueNetto.ToString("0.00"), item.Item.VatRate.ToString("0.00"), item.VATValue.ToString("0.00"), item.ValueBrutto.ToString("0.00"));
                Console.WriteLine(newItem);
                invoiceItemsTable +=newItem;
                i++;
            }
            invoiceItemsSummary = string.Format(invoiceItemsSummary, TotalValueNetto.ToString("0.00"), TotalVATValue.ToString("0.00"), TotalValueBrutto.ToString("0.00"));
            invoiceItemsTable += invoiceItemsSummary;

            string taxTableHeader = root.Element("TaxTableHeader").Value;
            string tax = root.Element("Tax").Value;
            string taxTableSummary = root.Element("TaxTableSummary").Value;            
            string taxTable = taxTableHeader + tax + taxTableSummary;

            string priceSummary = root.Element("PriceSummary").Value;
            priceSummary = string.Format(priceSummary, TotalValueBrutto.ToString("0.00"), "słownie złotych", "groszy");
            string paymentMethod = root.Element("PaymentMethod").Value;
            paymentMethod = string.Format(paymentMethod, PaymentMethod.Name, DateOfIssue.AddDays(PaymentMethod.DueDate).ToShortDateString());
            string issuer = root.Element("Issuer").Value;
            issuer = string.Format(issuer, Company.IssuerName);
            string footer = paymentMethod + issuer;


            string output = header + invoiceItemsTable + taxTable + priceSummary + footer;

            output = output.Replace("~^~^", "{{");
            output = output.Replace("^~^~", "}}");
            output = output.Replace("~^", "{");
            output = output.Replace("^~", "}");
            File.WriteAllText("out.tex", output);

            Process process = new Process();
            process.StartInfo.FileName = "pdflatex";
            process.StartInfo.Arguments = "out.tex";
            process.Start();
            //process.Dispose();
        }
    }
}