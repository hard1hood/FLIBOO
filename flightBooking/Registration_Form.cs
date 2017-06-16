using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.OracleClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace flightBooking
{
    public partial class Registration_Form : Form
    {
        Check check = new Check();

        public Registration_Form()
        {
            InitializeComponent();
        }
        
        Form1 f1 = new Form1();
        private string header = "Registration";
        private string fname="";
        private string lname="";
        private string mail="";
        private string oldpass = "";
        public Registration_Form(string header, string fname, string lname, string mail, string oldpass)
        {
            this.oldpass = oldpass;
            this.header = header;
            this.fname = fname;
            this.lname = lname;
            this.mail = mail;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool reg_completed = true;

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

            if (!check.IsValidEmail(email.Text))
            {
                email_label.ForeColor = Color.Red;
                reg_completed = false;
            }
            else
                email_label.ForeColor = Color.FromArgb(64, 64, 64);

            if (mail == "")
            {
                if (!check.CheckEmail(email.Text))
                {
                    email_label.ForeColor = Color.Red;
                    reg_completed = false;
                    MessageBox.Show("User with this e-mail already excists.");
                }
                else
                {
                    email_label.ForeColor = Color.FromArgb(64, 64, 64);
                }

            }
            else
            {
                if (mail.Trim() != email.Text.Trim())
                {
                    if (!check.CheckEmail(email.Text))
                    {
                        email_label.ForeColor = Color.Red;
                        reg_completed = false;
                        MessageBox.Show("User with this e-mail already excists.");
                    }
                    else
                    {
                        email_label.ForeColor = Color.FromArgb(64, 64, 64);
                    }
                }
                else
                    email_label.ForeColor = Color.FromArgb(64, 64, 64);
            }


            if (!check.CheckPass(pass1.Text))
            {
                pass1lbl.ForeColor = Color.Red;
                passwrong.Text = "Password must consist of 5-15 symbols";
                passwrong.Visible = true;
                reg_completed = false;
            }
            else
            {
                pass1lbl.ForeColor = Color.FromArgb(64, 64, 64);
                passwrong.Visible = false;
            }

            if (!check.PassConfirm(pass1.Text, pass2.Text) && check.CheckPass(pass1.Text))
            {
                pass2lbl.ForeColor = Color.Red;
                passwrong.Text = "Passwords must match";
                passwrong.Visible = true;
                reg_completed = false;
            }
            else if (!check.CheckPass(pass1.Text) && !check.PassConfirm(pass1.Text, pass2.Text))
            {
                pass2lbl.ForeColor = Color.FromArgb(64, 64, 64);
                passwrong.Visible = false;
            }
            if (reg_completed)
            {
                name1lbl.ForeColor = Color.FromArgb(64, 64, 64);
                name2lbl.ForeColor = Color.FromArgb(64, 64, 64);
                pass1lbl.ForeColor = Color.FromArgb(64, 64, 64);
                pass2lbl.ForeColor = Color.FromArgb(64, 64, 64);
                email_label.ForeColor = Color.FromArgb(64, 64, 64);
               
                if (header == "Registration")
                {
                    AddClient( email.Text,
                        pass1.Text, name1.Text, name2.Text);
                    res_label.Text = "Success!";
                    res_label.ForeColor = Color.ForestGreen;
                    res_label.Visible = true;
                    next_button.Visible = true;
                }
                else
                {
                    AcceptionForm af = new AcceptionForm(oldpass);
                    af.Owner = this;
                    af.Show();
                    
                }

            }
            else
            {
                res_label.Text = "Oops!";
                res_label.ForeColor = Color.Red;
                res_label.Visible = true;
            }

        }
        

        public void acceptation()
        {
            EditClient(mail, name1.Text, name2.Text, pass1.Text, email.Text);
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                main.useredited(name1.Text,name2.Text, email.Text);
            }
            res_label.Text = "Success!";
            res_label.ForeColor = Color.ForestGreen;
            res_label.Visible = true;
            next_button.Visible = true;
        }
        
        public void EditClient(string email,string fname, string lname, string pass, string newemail)
        {
            using (OracleConnection connection = new OracleConnection(f1.connectionstring))
            {
                OracleCommand cmd = new OracleCommand();
                //cmd.Parameters.Clear();
                cmd.Connection = connection;
                cmd.CommandText = "EDIT_CLIENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("EMAIL", OracleType.VarChar).Value=email;
                cmd.Parameters.Add("NEWEMAIL", OracleType.VarChar).Value = newemail;
                cmd.Parameters.Add("FNAME", OracleType.VarChar).Value = fname;
                cmd.Parameters.Add("LNAME", OracleType.VarChar).Value = lname;
                cmd.Parameters.Add("PASS", OracleType.VarChar).Value = pass;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public void AddClient( string email, string pass, string fname, string lname)
        {
            string queryString =
                " INSERT INTO CLIENTS (CLIENT_EMAIL, CLIENT_PASS, CLIENT_FIRSTNAME, CLIENT_LASTNAME) values ('" + email+"', '"+pass+"', '"+fname+ "', '" + lname + "')";
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

        private void next_button_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Registration_Form_Load(object sender, EventArgs e)
        {
            if(header!="Registration")
            {
                label2.Text = header;
                name1.Text = fname;
                name2.Text = lname;
                email.Text = mail;

            }
        }
    }
}
