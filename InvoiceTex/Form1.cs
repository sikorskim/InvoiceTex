using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InvoiceTex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            startup();
        }

        Settings settings;

        void startup()
        {
            Text = "Faktura";

            settings = new Settings("settings.xml");
            settings.importFromXML();

            comboBox1.DataSource = settings.Contractors;
            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "Id";

            comboBox2.DataSource = settings.PaymentMethods;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";

            comboBox3.DataSource = settings.Items;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "Id";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string path = "invoiceTemplate.xml";
            XDocument doc = XDocument.Load(path);
            XElement root = doc.Element("Template");

            string header = root.Element("Header").Value;
            header = string.Format(header, settings.Company.Name, settings.Company.Street, settings.Company.NIP, settings.Company.REGON, settings.Company.Phone, settings.Company.Email, settings.Company.Website, settings.Company.BankName, settings.Company.BankAccount);

            string datePlace = root.Element("DatePlace").Value;
            string dateOfIssue = dateTimePicker1.Value.ToShortDateString();
            string dateOfDelivery = dateTimePicker2.Value.ToShortDateString();
            string issueCity = "Śrem";
            datePlace = string.Format(datePlace, dateOfIssue, issueCity, dateOfDelivery);

            string title = root.Element("Title").Value;
            string invoiceNumber = "FV/2018/5/1";
            title = string.Format(title, invoiceNumber);

            string issuer = root.Element("Issuer").Value;
            issuer = string.Format(issuer, settings.Company.IssuerName);

            string sellerBuyer = root.Element("SellerBuyer").Value;
            sellerBuyer = string.Format(sellerBuyer, settings.Company.FullName,settings.Company.FullAddress, settings.Company.NIP, settings.Contractors.FirstOrDefault().FullName, settings.Contractors.FirstOrDefault().FullAddress, settings.Contractors.FirstOrDefault().NIP);

            string invoiceItem = root.Element("InvoiceItem").Value;
            invoiceItem = string.Format(invoiceItem, "1", settings.Items.FirstOrDefault().Name, settings.Items.FirstOrDefault().UnitOfMeasure, "1", settings.Items.FirstOrDefault().UnitPrice, settings.Items.FirstOrDefault().UnitPrice, settings.Items.FirstOrDefault().VatRate, "23,00", "123,00");
            
            string output = header + datePlace + title +sellerBuyer+invoiceItem+issuer;
            output = output.Replace("~^~^", "{{");
            output = output.Replace("^~^~", "}}");
            output =output.Replace("~^", "{");
            output=output.Replace("^~", "}");
            File.WriteAllText("out.tex", output);
        }
    }
}
