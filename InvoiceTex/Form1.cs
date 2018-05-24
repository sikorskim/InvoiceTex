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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            settings = new Settings("settings.xml");
            settings.importFromXML();
            comboBox1.DataSource = settings.Contractors;
            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "Id";
        }

        Settings settings;
    }
}
