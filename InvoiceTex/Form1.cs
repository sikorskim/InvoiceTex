using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        List<InvoiceItem> invoiceItems;

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

            invoiceItems = new List<InvoiceItem>();

            textBox2.Text = Invoice.getNumber();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        void generateInvoiceTest()
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
            sellerBuyer = string.Format(sellerBuyer, settings.Company.FullName, settings.Company.FullAddress, settings.Company.NIP, settings.Contractors.FirstOrDefault().FullName, settings.Contractors.FirstOrDefault().FullAddress, settings.Contractors.FirstOrDefault().NIP);

            string invoiceItemsTableHeader = root.Element("InvoiceItemsTableHeader").Value;
            string invoiceItemsSummary = root.Element("InvoiceItemTableSummary").Value;
            string invoiceItem = root.Element("InvoiceItem").Value;
            invoiceItem = string.Format(invoiceItem, "1", settings.Items.FirstOrDefault().Name, settings.Items.FirstOrDefault().UnitOfMeasure, "1", settings.Items.FirstOrDefault().UnitPrice, settings.Items.FirstOrDefault().UnitPrice, settings.Items.FirstOrDefault().VatRate, "23,00", "123,00");

            string taxTableHeader = root.Element("TaxTableHeader").Value;
            string tax = root.Element("Tax").Value;
            string taxTableSummary = root.Element("TaxTableSummary").Value;
            string taxTable = taxTableHeader + tax + taxTableSummary;

            string priceSummary = root.Element("PriceSummary").Value;
            string paymentMethod = root.Element("PaymentMethod").Value;

            string output = header + datePlace + title + sellerBuyer + invoiceItemsTableHeader + invoiceItem + invoiceItemsSummary + taxTable + priceSummary + paymentMethod + issuer;




            output = output.Replace("~^~^", "{{");
            output = output.Replace("^~^~", "}}");
            output = output.Replace("~^", "{");
            output = output.Replace("^~", "}");
            File.WriteAllText("out.tex", output);

            Process process = new Process();
            process.StartInfo.FileName = "pdflatex";
            process.StartInfo.Arguments = "out.tex";
            process.Start();
            process.Dispose();
            // wait to avoid System.ComponentModel.Win32Exception: 'The system cannot find the file specified' for out.pdf file
            Thread.Sleep(1000);
            openPdf();
        }

        void openPdf()
        {
            Process.Start(@"out.pdf");
        }

        void addInvoiceItem()
        {
            dataGridView1.DataSource = null;
            InvoiceItem invoiceItem = new InvoiceItem();
            invoiceItem.Item = (Item)comboBox3.SelectedItem;
            invoiceItem.Quantity = (int)numericUpDown1.Value;

            invoiceItems.Add(invoiceItem);
            dataGridView1.DataSource = invoiceItems;            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            addInvoiceItem();
        }

        void generateInvoice()
        {
            Invoice invoice = new Invoice();
            invoice.Company = settings.Company;
            invoice.Contractor = (Contractor)comboBox1.SelectedItem;
            invoice.DateOfIssue = dateTimePicker1.Value;
            invoice.DateOfDelivery = dateTimePicker2.Value;
            invoice.IssuePlace = textBox1.Text;
            invoice.Number = textBox2.Text;
            invoice.PaymentMethod = (PaymentMethod)comboBox2.SelectedItem;
            invoice.InvoiceItems = invoiceItems;
            invoice.generate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            generateInvoice();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.Show();
        }
    }
}
