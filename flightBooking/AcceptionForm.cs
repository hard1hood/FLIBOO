using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace flightBooking
{
    public partial class AcceptionForm : Form
    {
        private string  pass,text="";

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public bool accepted;

        public AcceptionForm(string pass)
        {
            this.pass = pass;
            InitializeComponent();
        }
        public AcceptionForm(string pass, string text)
        {
            this.text = text;
            this.pass = pass;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != pass)
            {
                label2.Visible = true;
                accepted = false;
            }  
            else
            {
                accepted = true;
                MessageBox.Show("Success!");
                this.Hide();
                if (text == "")
                {
                    Registration_Form main = this.Owner as Registration_Form;
                    if (main != null)
                    {
                        main.acceptation();
                    }
                }
                else
                {
                    Form1 main = this.Owner as Form1;
                    if (main != null)
                    {
                        main.delfl();
                    }
                }
            }
        }

        private void AcceptionForm_Load(object sender, EventArgs e)
        {
            if(text!="")
            {
                label1.Text = text;
            }
        }
    }
}
