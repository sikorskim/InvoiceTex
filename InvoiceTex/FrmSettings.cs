using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceTex
{
    public partial class FrmSettings : Form
    {
        public FrmSettings(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
            getCompany();
        }
        Settings settings;

        void startup()
        {
            getCompany();
        }

        void getCompany()
        {
            Company company = settings.Company;
            textBox1.Text = company.FullName;
            textBox2.Text = company.Name;
            textBox3.Text = company.NIP;
            textBox4.Text = company.Street;
            textBox5.Text = company.BuldingNo;
            textBox6.Text = company.PostalCode;
            textBox7.Text = company.City;
            textBox8.Text = company.Phone;
            textBox9.Text = company.Email;
            textBox10.Text = company.Website;
            textBox11.Text = company.BankName;
            textBox12.Text = company.BankAccount;
            textBox13.Text = company.IssuerName;
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
