using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace flightBooking
{
    public partial class Form1 : Form
    {

        public Form1()
        {
           
            InitializeComponent();
            setconn();
            dateTimePicker1.MinDate = DateTime.Today;
        }

        public string connectionstring, username ;
        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void news_Click(object sender, EventArgs e)
        {
            // news.ForeColor = Color.DodgerBlue;

        }

        private void setconn()
        {
            StreamReader sr = new StreamReader(@"connection.txt");
            connectionstring = sr.ReadLine();
            sr.Close();
            StreamReader sr1 = new StreamReader(@"username.txt");
            username = sr1.ReadLine();
            sr1.Close();


        }


        private void contacts_label_Click(object sender, EventArgs e)
        {

        }

        private void regonline_label_Click(object sender, EventArgs e)
        {
           
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {

        }

        private void exit_label_MouseHover(object sender, EventArgs e)
        {

        }

        private void regonline_label_MouseHover(object sender, EventArgs e)
        {

        }

        private void contacts_label_MouseHover(object sender, EventArgs e)
        {

        }

        private void news_MouseLeave(object sender, EventArgs e)
        {

        }

        private void contacts_label_MouseLeave(object sender, EventArgs e)
        {

        }

        private void regonline_label_MouseLeave(object sender, EventArgs e)
        {

        }

        private void exit_label_MouseLeave(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }




        private void button3_Click(object sender, EventArgs e)
        {

            int BrowserVer, RegVal;

            // get the installed IE version
            using (WebBrowser Wb = new WebBrowser())
                BrowserVer = Wb.Version.Major;

            // set the appropriate IE version
            
                RegVal = 11001;
            

            // set the actual key
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, RegistryValueKind.DWord);
            Key.Close();
            dataGridView1.Rows.Clear();
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Please, enter searching parametres!");

            }
            else
            {
                dataGridView1.Visible = true;
                OutSuitableFlights(comboBox1.Text, comboBox3.Text, Convert.ToInt32(dateTimePicker1.Value.Day), Convert.ToInt32(dateTimePicker1.Value.Month), comboBox2.Text);
                bookbutton.Visible = true;
                bookbutton.Enabled = false;
            }

            
            try
            {
                string from = comboBox1.SelectedItem.ToString();
                from.Replace(" ", "+");
                string to = comboBox3.SelectedItem.ToString();
                to.Replace(" ", "+");
                StringBuilder direction = new StringBuilder();
                direction.Append("http://maps.google.com/maps/dir/");

                if (from != string.Empty)
                {
                    direction.Append(from + "/");
                }

                if (to != string.Empty)
                {
                    direction.Append(to + "/");
                }
                //direction.Append("data=!3e4");
              
                webBrowser1.Navigate(direction.ToString());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration_Form rf = new Registration_Form();
            rf.Show();
        }
        public bool authorised=false;
        public string f_name, l_name, passw;
        public int fl_price;
        private void button1_Click(object sender, EventArgs e)
        {
            if (email.Text != "" && pass.Text != "" && email.Text.Length < 50 && pass.Text.Length < 50)
            {
                if (IsRegistered( email.Text, pass.Text))
                {
                    MessageBox.Show("Success! welcome as user!");
                    acc_label.Visible = true;
                    panel_unauthorised.Visible = false;
                    panel_authorised.Visible = true;
                    authorised = true;
                    Authorise(email.Text);
                    se = email.Text;
                    fname_lbl.Text = f_name;
                    lname_lbl.Text = l_name;
                    email_lbl.Text = se;
                    

                }
                else if (IsCompRegistered( email.Text, pass.Text))
                {
                    MessageBox.Show("Success! welcome as Aviacompany!");
                    flightsedit.Visible = true;
                    panel_authorised.Visible = true;
                    panel_unauthorised.Visible = false;
                    mytab.SelectedIndex = 2;
                    main_page.Visible = false;
                    passw = pass.Text;
                    ////////////////////////////////////////////////////////////////////////////////////////
                    OutCompFlights();

                }
                else MessageBox.Show("Failed. No such user registered.");

            }
            else if (email.Text == "" || pass.Text == "")
                MessageBox.Show("Empty enter!");
            else
                MessageBox.Show("Enter should be less then 50 symbols!");
        }
        public string comp;


        public void Authorise(string email)
        {
            string queryString =
                 "SELECT * FROM CLIENTS";
            
            using (OracleConnection connection = new OracleConnection(connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {

                        if (reader.GetString(0).Trim() == email.Trim())
                        {
                            f_name = reader.GetString(2).Trim();
                            passw = reader.GetString(1).Trim();
                            l_name = reader.GetString(3).Trim();
                        }
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
            }
        }

        public bool IsRegistered( string email, string pass)
        {

            string queryString =
                "SELECT * FROM CUST_DATA";
            using (OracleConnection connection = new OracleConnection(connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {

                        if (reader.GetString(0).Trim() == email.Trim() && reader.GetString(1).Trim() == pass.Trim())
                        {

                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
        }

        private void OutCompFlights()
        {
            dataGridView3.Rows.Clear();
            string queryString =
                "SELECT * FROM COMPFLIGHTS";
            using (OracleConnection connection = new OracleConnection(connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    int i = 0;
                    while (reader.Read())
                    {

                        if (reader.GetString(0).Trim().ToUpper() == comp.ToUpper().Trim())
                        {
                            dataGridView3.Rows.Insert(i, reader.GetString(1).Trim(), reader.GetString(2).Trim(), reader.GetString(3).Trim(),
                                reader.GetString(4).Trim(), reader.GetString(5).Trim(), reader.GetInt32(6),
                                reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9));
                            i++;
                        }

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
            }
        }

        public bool IsCompRegistered( string email, string pass)
        {

            string queryString =
                "SELECT * FROM COMP_DATA";
            using (OracleConnection connection = new OracleConnection(connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {

                        if (reader.GetString(0).Trim() == email.Trim() && reader.GetString(1).Trim() == pass.Trim())
                        {
                            comp = reader.GetString(2).Trim();

                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
        }

        private void OutSuitableFlights(string from, string to, int day, int month, string type)
        {
            int pr;
            string[] fr = from.Split(',');
            from = fr[0];
            string[] t = to.Split(',');
            to = t[0];
            string queryString =
                "SELECT * FROM SUITABLE_FLIGHTS";
            using (OracleConnection connection = new OracleConnection(connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    int i = 0;
                    while (reader.Read())
                    {

                        if (reader.GetString(3).Trim() == from.Trim() && reader.GetString(6).Trim() == to.Trim()&& reader.GetInt32(11) == day&& reader.GetInt32(10)==month)
                        {
                            if(type=="economy")
                            {
                                pr = reader.GetInt32(8);
                            }
                            else
                                pr = reader.GetInt32(9);
                            dataGridView1.Rows.Insert(i, reader.GetString(0).Trim(), reader.GetString(1).Trim(), reader.GetString(2).Trim()+", "+ 
                                reader.GetString(3).Trim(), reader.GetDateTime(4), reader.GetString(5).Trim()+" ,"+ 
                                reader.GetString(6).Trim(), reader.GetDateTime(7),pr.ToString());
                            i++;
                        }
                        
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
            }
        }

        private void OutBookingHistory(string email)
        {
            dataGridView2.Rows.Clear();
            string queryString =
                "SELECT B_NUM, DEPARTURE, FLIGHT_DEPARTURE_TIME,ARRIVAL,B_F_PRICE,BOOKED_FLIGHT_C_EMAIL FROM BOOKINGHISTORY";
            using (OracleConnection connection = new OracleConnection(connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    int i = 0;
                    while (reader.Read())
                    {
                       
                            if (reader.GetString(5).Trim() == email.Trim())
                            {
                                dataGridView2.Rows.Insert(i, reader.GetInt32(0), reader.GetString(1).Trim(), reader.GetDateTime(2),
                                    reader.GetString(3).Trim(), reader.GetInt32(4));
                                i++;
                            }
                        
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
            dataGridView2.Visible = true;
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            panel_unauthorised.Visible = true;
            panel_authorised.Visible = false;
            acc_label.Visible = false;
            flightsedit.Visible = false;
            main_page.Visible = true;
            mytab.SelectedIndex = 0;
            authorised = false;
            email.Text = "";
            pass.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setconn();
            FillAirportCollection("FROM");
            FillAirportCollection("TO");
            dataGridView1.MultiSelect = false;
            dataGridView2.MultiSelect = false;
            dataGridView3.MultiSelect = false;

            //------fill class collection
            comboBox2.Items.Add("economy");
            comboBox2.Items.Add("business");
            


        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void main_page_MouseHover(object sender, EventArgs e)
        {

        }

        private void main_page_MouseLeave(object sender, EventArgs e)
        {

        }

        private void search_page_Click(object sender, EventArgs e)
        {

        }

        private void label_click(object sender, EventArgs e)
        {
            Label label = sender as Label;

            label.BackColor = Color.DodgerBlue;
            for (int k = 0; k < 54; k++)
                if (l[k].Name != label.Name)
                    l[k].BackColor = Color.LightSkyBlue;
        }

        public Label[] l = new Label[54];
        

        private void choose_button_Click(object sender, EventArgs e)
        {
            
        }

        private void addflightbutton_Click(object sender, EventArgs e)
        {
            addFlightForm aff = new addFlightForm(comp);
            aff.Owner = this;
            aff.Show();
        }

        public void added()
        {
            dataGridView3.Rows.Clear();
            OutCompFlights();
        }
        private void main_page_Click(object sender, EventArgs e)
        {
            mytab.SelectedIndex = 0;
            flightsedit.ForeColor = Color.FromArgb(64, 64, 64);
            main_page.ForeColor = Color.DodgerBlue;
            acc_label.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void acc_label_Click(object sender, EventArgs e)
        {
            
            mytab.SelectedIndex = 1;
            Authorise(se);
            fname_lbl.Text = f_name;
            lname_lbl.Text = l_name;
            email_lbl.Text = se;
            flightsedit.ForeColor = Color.FromArgb(64, 64, 64);
            main_page.ForeColor = Color.FromArgb(64, 64, 64);
            acc_label.ForeColor = Color.DodgerBlue;
            OutBookingHistory(se);
        }

        private void flightsedit_Click(object sender, EventArgs e)
        {
            mytab.SelectedIndex = 2;
            flightsedit.ForeColor = Color.DodgerBlue;
            main_page.ForeColor = Color.FromArgb(64, 64, 64);
            acc_label.ForeColor = Color.FromArgb(64, 64, 64);
        }

        public string fl_num, se;
        private void bookbutton_Click(object sender, EventArgs e)
        {
            fl_num = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            fl_price = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value);
            bookingForm bf = new bookingForm(se,fl_num,authorised,f_name,l_name,fl_price);
            bf.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FillAirportCollection(string direction)
        {
            string[] List = new string[100];
            int i = 0,j; string s;
            bool povtor=false; i = 0;
            string queryString =
                "SELECT AIRPORT_CITY, AIRPORT_COUNTRY FROM AVAILABLE_" + direction+"_AIRPORT";
            using (OracleConnection connection = new OracleConnection(connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        povtor = false;
                        s = reader.GetString(0).Trim() + ", " + reader.GetString(1).Trim();
                        for (j = 0; j < i; j++)
                        {
                            
                            if (s==List[j])
                            {
                                povtor = true;
                                break;
                            }
                        }

                        if (!povtor)
                        {
                            List[i] = s;
                            i++;
                        }
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
                string[] myList = new string[i];
                for(i=0;i<myList.Length;i++)
                {
                    myList[i] = List[i];
                }
                if(direction =="FROM")comboBox1.Items.AddRange(myList);
                else comboBox3.Items.AddRange(myList);
            }
        }

        private void departure()
        {
            string[] List = new string[100];
            int i = 0, j; string s;
            bool povtor = false; i = 0;
            string queryString =
                "SELECT FLIGHT_DEPARTURE FROM FLIGHTS";
            using (OracleConnection connection = new OracleConnection(connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        povtor = false;
                        s = reader.GetString(0).Trim();
                        for (j = 0; j < i; j++)
                        {

                            if (s == List[j])
                            {
                                povtor = true;
                                break;
                            }
                        }

                        if (!povtor)
                        {
                            List[i] = s;
                            i++;
                        }
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
                string[] myList = new string[i];
                for (i = 0; i < myList.Length; i++)
                {
                    myList[i] = List[i];
                }
                string res="";
                for(i=0;i<myList.Length;i++)
                {
                    res = res + " " + myList[i];
                }
                MessageBox.Show(res);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void DelFlight(string f_num)
        {
            using (OracleConnection connection = new OracleConnection(connectionstring))
            {
                OracleCommand cmd = new OracleCommand();
                //cmd.Parameters.Clear();
                cmd.Connection = connection;
                cmd.CommandText = "DELFLIGHT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("FN", OracleType.Char).Value = f_num;
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

        private void editpersinfobutton_Click(object sender, EventArgs e)
        {
            Registration_Form rf = new Registration_Form("Edit personal info", f_name, l_name, se, passw);
            rf.Owner = this;
            rf.Show();
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            editflightbutton.Enabled = true;
            delflightbutton.Enabled = true;
        }

        private void delflightbutton_Click(object sender, EventArgs e)
        {
            AcceptionForm af = new AcceptionForm(passw,"Enter password to delete");
            af.Owner = this;
            af.Show();
        }
        public void delfl()
        {
            DelFlight(dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[0].Value.ToString());
            dataGridView3.Rows.Clear();
            OutCompFlights();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            departure();
        }

        private void editflightbutton_Click(object sender, EventArgs e)
        {
            addFlightForm af = new addFlightForm(comp,dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[0].Value.ToString());
            af.Owner = this;
            af.Show();
        }


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bookbutton.Enabled = true;
        }

        private void search_panel_Paint(object sender, PaintEventArgs e)
        {

        }
        public void useredited(string f, string l, string e)
        {
            fname_lbl.Text = f;
            lname_lbl.Text = l;
            email_lbl.Text = e;
        }
    }
}
