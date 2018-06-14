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
            string inputString = textBox1.Text;
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

            string[] ones = { "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć" };
            string[] teens = { "dziesięć", "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemanście", "dziewiętnaście" };
            string[] doubles = { "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
            string[] triplets = { "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
            string[] thousands = { "tysiąc", "tysięcy" };
            string[] millions = { "milion", "milionów" };

            if (i1 > 0 && i1 <= 9)
            {
                valueInWords = ones[i1 - 1];
            }
            else if (i1 >= 10 && i1 <= 19)
            {
                valueInWords = teens[i1 - 10];
            }
            else if (i1 >= 20 && i1 <= 99)
            {
                valueInWords = doubles[i1 / 10 - 2];

                int i11 = i1 % 10;
                if (i11 != 0)
                {
                    valueInWords += " " + ones[i11 - 1];
                }
            }
            else if (i1 >= 100 && i1 <= 999)
            {
                valueInWords = triplets[i1 / 100 - 1];

                int i11 = i1 % 100;
                if (i11 != 0)
                {
                    if (i11 >= 10 && i11 <= 19)
                    {
                        valueInWords += " "+ teens[i11 - 10];
                    }
                    if (i11 >= 20 && i11 <= 99)
                    {
                        valueInWords += " " + doubles[i11 / 10 - 2];

                        int i111 = i11 % 10;
                        if (i111 != 0)
                        {
                            valueInWords += " " + ones[i111 - 1];
                        }
                    }
                }           
                textBox5.Text = valueInWords;
            }
        }
    }
}
