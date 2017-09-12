using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
//Add the MySql Library
using MySql.Data;
using MySql.Data.MySqlClient;

namespace RandomTestGenerator
{
    class EmailUser
    {
        static MySqlConnection m_db_conn = null;
        // static DataTable m_data_table = null;
        static MySqlCommand m_sql_command = null;
        // static MySqlDataAdapter m_sql_adapt = null;
        static MySqlDataReader m_sql_reader = null;


        static public void SendEmail(string a_firstName, string a_lastName, string a_email, string a_userName, string a_temp_pass)
        {
            string m_subject = "";
            string body_text = "";

            Byte m_firstLogin = Login.g_FirstLogin;
            if (m_firstLogin == 2)
            {
                // Subject of the Email
                m_subject = "Reset Password - " + a_firstName.ToString() + " " + a_lastName.ToString();

                // Body of the email
                body_text = "Dear " + a_firstName.ToString() + ",<br /><br />" + "You requested a password reset.<br /><br />" + "Please find below your login information:<br />" + "Username: " + a_userName.ToString() + "<br />" + "Temporary Password: " + a_temp_pass.ToString() + "<br /><br />" + "Please note that you will be asked to change your password after you login with the given password.<br /><br />" + "Sincerely," + "<br />" + "Svetlana Marhefka";
            }
            else
            {
                // Subject of the Email
                m_subject = "Registration Confirmation - " + a_firstName.ToString() + " " + a_lastName.ToString();

                // Body of the email
                body_text = "Dear " + a_firstName.ToString() + ",<br /><br />" + "Thank you for registering for the Random Test Generator!<br /><br />" + "Please find below your login information:<br />" + "Username: " + a_userName.ToString() + "<br />" + "Temporary Password: " + a_temp_pass.ToString() + "<br /><br />" + "Please note that you will be asked to change your password after you login with the given password.<br /><br />" + "Thank you for your interest in the Random Test Generator!<br /><br />" + "Sincerely," + "<br />" + "Svetlana Marhefka";
            }

            try
            {
                // *MEANS UNCOMMENT!
                // Creates a new email object
                MailMessage message = new MailMessage();
                // Sender Email Address
                String m_senderadd = "lisalini456@gmail.com";
                message.From = new MailAddress(m_senderadd, "Svetlana Marhefka");
                // Reciever Email Address
                message.To.Add(new MailAddress(a_email.ToString()));

                message.Subject = m_subject;

                string m_body = body_text.ToString();
                message.Body = m_body;
                message.IsBodyHtml = true;

                // Now Connect to the e-mail server
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                // client.Port = 465;
                client.Port = 587;
                // Setting Credential of Gmail Account
                string m_sender = "lisalini456@gmail.com";
                string m_pass = "EmailAccount2017!";
                client.Credentials = new System.Net.NetworkCredential(m_sender, m_pass);
                // client.UseDefaultCredentials = false;
                // Enabling secured Connection
                client.EnableSsl = true;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                Console.WriteLine("I am here\n");
                //Sending Email
                client.Send(message);
                MessageBox.Show("Please check your e-mail for login information\n You will be redirected to the login screen");

                // Cursor.Current = Cursors.WaitCursor;
                // Cursor.Current = Cursors.Default;
                // Free the memory
                message = null;
            }
            // Catching any errors
            catch (Exception m_error)
            {
                MessageBox.Show(m_error.ToString());
            }
        }

        public static string FindEmail(string a_UserName)
        {
            string l_email = "";
            string m_query = "SELECT * FROM seniorproject.user_login WHERE UserName = @find_username";
            m_db_conn = DBConnect.OpenConnection();
            try
            {
                m_sql_command = new MySqlCommand(m_query, m_db_conn);
                m_sql_command.Parameters.AddWithValue("@find_username", a_UserName);
                m_sql_reader = m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (m_sql_reader.Read())
                {
                    l_email = m_sql_reader.GetString(m_sql_reader.GetOrdinal("SchoolEmail"));
                }
                m_db_conn = DBConnect.CloseConnection();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error + "\n");
            }
            return l_email;
        }
    }
}