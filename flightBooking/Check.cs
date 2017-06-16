using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace flightBooking
{
    class Check
    {
        public bool CheckName(string str)
        {
            int i;
            if (str == ""||str.Length>50)
                return false;
            for(i=0;i<str.Length;i++)
            {
                if(!((str[i]>64&&str[i]<91)||(str[i] > 96 && str[i] < 123)||str[i]==32))
                {
                    return false;
                }
            }
            return true;
        }

        public bool TimeCheck(string time1, string time2, DateTime d1, DateTime d2)
        {
            string[] T1 = time1.Split(':');
            string[] T2 = time2.Split(':');

            if (Convert.ToInt32(T1[0]) > 23 || Convert.ToInt32(T2[0]) > 23 || Convert.ToInt32(T1[1]) > 59 || Convert.ToInt32(T2[1]) > 59)
            {
                return false;
            }
            else
            {
                if (d2.Ticks - d1.Ticks >= 3600000)
                    return true;
                else
                {
                    if (Convert.ToInt32(T1[0]) < Convert.ToInt32(T2[0]))
                        return true;
                    else if (Convert.ToInt32(T1[0]) > Convert.ToInt32(T2[0]))
                        return false;
                    else
                        if (Convert.ToInt32(T1[1]) >= Convert.ToInt32(T2[1]))
                        return false;
                    else return true;
                }
            }
        }

        public bool CheckIntValue(string v)
        {
            int a;
            if (Int32.TryParse(v, out a))
                return true;
            else
                return false;
        }

        Form1 f1 = new Form1();

        public bool CheckFlightNum(string fl_num)
        {

            string queryString =
                "SELECT FLIGHT_NUMBER FROM FLIGHTS";
            using (OracleConnection connection = new OracleConnection(f1.connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(0).Trim().ToUpper() == fl_num.Trim().ToUpper())
                        {
                            return false;
                        }
                    }
                    return true;
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
                
            }
        }

        public bool CheckFlightNum(string fl_num, string s)
        {

            string queryString =
                "SELECT FLIGHT_NUMBER FROM FLIGHTS";
            using (OracleConnection connection = new OracleConnection(f1.connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(0).Trim().ToUpper() == fl_num.Trim().ToUpper()&& reader.GetString(0).Trim().ToUpper()!=s.Trim().ToUpper())
                        {
                            return false;
                        }
                    }
                    return true;
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }

            }
        }

        public bool CheckEmail(string e)
        {

            string queryString =
                "SELECT CLIENT_EMAIL FROM CLIENTS";
            using (OracleConnection connection = new OracleConnection(f1.connectionstring))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(0).Trim().ToUpper() == e.Trim().ToUpper())
                        {
                            return false;
                        }
                    }
                    return true;
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }

            }
        }

        public bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return (strIn!="" && Regex.IsMatch(strIn,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"));
        }
        public bool CheckPass(string str)
        {
            return (str.Length >= 5 && str.Length <= 15);
        }
        public bool PassConfirm(string pass1, string pass2)
        {
            return pass1 == pass2;
        }
        public bool DateCheck(DateTime d)
        {
            return (d > DateTime.Today);
        }
    }
}
