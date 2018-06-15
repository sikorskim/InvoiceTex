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
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal d = 0;
            decimal.TryParse(textBox1.Text, out d);
            getValueInWords(d);
        }

        void getValueInWords(decimal d)
        {
            string inputString = d.ToString("0.00");
            string[] parts;
            int i1;
            int i2;

            try
            {
                parts = inputString.Split(',');
                i1 = int.Parse(parts[0]);
                i2 = int.Parse(parts[1]);
            }
            catch (FormatException)
            {
                parts = inputString.Split('.');
                i1 = int.Parse(parts[0]);
                i2 = int.Parse(parts[1]);
            }

            textBox3.Text = i1.ToString();
            textBox4.Text = i2.ToString();

            string valueInWords = "";

            if (i1 >= 1 && i1 <= 9)
            {
                valueInWords = getOnes(i1);
            }
            else if (i1 >= 10 && i1 <= 19)
            {
                valueInWords = getTeens(i1);
            }
            else if (i1 >= 20 && i1 <= 99)
            {
                valueInWords = getDoubles(i1);
            }
            else if (i1 >= 100 && i1 <= 999)
            {
                valueInWords = getTriplets(i1);
            }
            else if (i1 >= 1000 && i1 <= 99999)
            {
                valueInWords = getThousands(i1);
            }

            textBox5.Text = valueInWords;
        }

        string getOnes(int i)
        {
            string[] ones = { "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć" };
            return ones[i - 1];
        }

        string getTeens(int i)
        {
            string[] teens = { "dziesięć", "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemanście", "dziewiętnaście" };
            return teens[i - 10];
        }

        string getDoubles(int i)
        {
            string[] doubles = { "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
            string output = doubles[i / 10 - 2];
            int i1 = i % 10;
            if (i1 != 0)
            {
                output += " " + getOnes(i1);
            }

            return output;
        }

        string getTriplets(int i)
        {
            string[] triplets = { "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
            string output = triplets[i / 100 - 1];
            int i1 = i % 100;
            if (i1 != 0)
            {
                if (i1 >= 10 && i1 <= 19)
                {
                    output += " " + getTeens(i1);
                }
                if (i1 >= 20 && i1 <= 99)
                {
                    output += " " + getDoubles(i1);
                }
            }
            return output;
        }

        string getThousands(int i)
        {
            string[] thousands = { "tysiąc", "tysiące", "tysięcy" };
            string output = "";
            int i1 = i / 1000;
            if (i1 == 1)
            {
                output = thousands[0];
            }
            else if (i1 >= 2 && i1 <= 4)
            {
                output = getOnes(i1) + " " + thousands[1];
            }
            else if (i1 >= 5 && i1 <= 9)
            {
                output = getOnes(i1) + " " + thousands[2];
            }
            else if (i1 >= 10 && i1 <= 19)
            {
                output = getTeens(i1) + " " + thousands[2];
            }
            else if (i1 >= 20 && i1 <= 99)
            {
                output = getDoubles(i1);
                int i2 = Int32.Parse(i1.ToString()[1].ToString());
                if (i2 >= 2 && i2 <= 4)
                {
                    output += " " + thousands[1];
                }
                else
                {
                    output += " " + thousands[2];
                }
            }
            return output;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal d = 0;
                decimal.TryParse(textBox1.Text, out d);
                getValueInWords(d);
            }
        }
    }
}
