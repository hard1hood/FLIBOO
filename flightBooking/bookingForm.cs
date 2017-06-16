using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.IO;

namespace flightBooking
{
    public partial class bookingForm : Form
    {
        Check check = new Check();
        private bool auth;
        private string s1;
        private string s2;
        private string flight_num;
        private string se="none";
        private int flight_price;

        public bookingForm(string flight_num, int flight_price)
        {
            this.flight_price = flight_price;
            this.flight_num = flight_num;
            InitializeComponent();
            dateTimePicker2.MinDate = DateTime.Today;
        }

        public bookingForm(string se,string flight_num,bool auth, string s1, string s2, int flight_price)
        {
            this.flight_price = flight_price;
            this.se = se;
            this.flight_num = flight_num;
            this.auth=auth;
            this.s1 = s1;
            this.s2 = s2;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool reg_completed = true;
            string g;

            if (!check.CheckName(name1.Text))
            {
                name1lbl.ForeColor = Color.Red;
                reg_completed = false;
            }
            else
                name1lbl.ForeColor = Color.FromArgb(64, 64, 64);

            if (!check.CheckName(name2.Text))
            {
                name2lbl.ForeColor = Color.Red;
                reg_completed = false;
            }
            else
                name2lbl.ForeColor = Color.FromArgb(64, 64, 64);

            if (!check.CheckName(citizenship.Text))
            {
                citizenshiplbl.ForeColor = Color.Red;
                reg_completed = false;
            }
            else
                citizenshiplbl.ForeColor = Color.FromArgb(64, 64, 64);

           
            if (passnum.Text=="" || passnum.Text.Length!=8)
            {
                passnumlbl.ForeColor = Color.Red;
                reg_completed = false;
            }

            if (reg_completed)
            {
                name1lbl.ForeColor = Color.FromArgb(64, 64, 64);
                name2lbl.ForeColor = Color.FromArgb(64, 64, 64);
                citizenship.ForeColor = Color.FromArgb(64, 64, 64);
                passnumlbl.ForeColor = Color.FromArgb(64, 64, 64);
                res_label.Text = "Success!";
                res_label.ForeColor = Color.ForestGreen;
                res_label.Visible = true;
                if (male.Checked)
                    g = "m";
                else if (female.Checked)
                    g = "f";
                else g = "";
                
                string s = dateTimePicker2.Value.Day + "/" + dateTimePicker2.Value.Month + "/" + dateTimePicker2.Value.Year;
                
                int n = FindBookNum();
                AddBooking(name1.Text, name2.Text, citizenship.Text, g, passnum.Text,s ,n,se);

                string bn = String.Format("Your booking number is: "+n);
                MessageBox.Show(bn);
                next_button.Visible = true;
            }
            else
            {
                res_label.Text = "Oops!";
                res_label.ForeColor = Color.Red;
                res_label.Visible = true;
            }
        }
        Form1 f1 = new Form1();

        public int FindBookNum()
        {
            string queryString =
                 "SELECT B_NUM FROM BOOKED_FLIGHT";
            int max=0, i = 0;

            using (OracleConnection connection = new OracleConnection(f1.connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(0) > max)
                            max = reader.GetInt32(0);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
                return max+1;
            }
        }

        public void AddBooking( string first, string last, string citizen, string gender, string pass, string val, int b_num, string e_adress)
        {
            string queryString =
                " INSERT INTO BOOKED_FLIGHT (B_F_NAME,B_L_NAME,B_CITIZENSHIP,B_GENDER,B_PASSPORT,B_PASS_VALID,B_NUM,B_FLIGHT,BOOKED_FLIGHT_C_EMAIL,B_F_PRICE) values ('" +
                first + "', '" + last + "', '" + citizen + "', '" + gender + "','" + pass + "','" + val + "'," + 
                b_num + ",'" + flight_num + "','" + e_adress + "'," + flight_price + ")";
            using (OracleConnection connection = new OracleConnection(f1.connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString);
                command.Connection = connection;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void res_label_Click(object sender, EventArgs e)
        {

        }

        private void next_button_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void birthdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bookingForm_Load(object sender, EventArgs e)
        {
            if (auth)
            {
                name1.Text = s1;
                //MessageBox.Show(f1.f_name);
                name2.Text = s2;
            }
            else { this.se = "none"; }
            //MessageBox.Show(f1.f_name);
            string[] lines = System.IO.File.ReadAllLines(@"countries.txt");
            citizenship.Items.AddRange(lines);

        }
    }
}
