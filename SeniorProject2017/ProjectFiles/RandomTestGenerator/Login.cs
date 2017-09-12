using System;
using System.Data;
using System.Windows.Forms;
//Add the MySql Library
using MySql.Data.MySqlClient;

namespace RandomTestGenerator
{
    public partial class Login : Form
    {
        private static MySqlConnection m_db_conn;
        private static MySqlCommand m_sql_command;
        private static MySqlDataReader m_sql_reader;
        private static MySqlDataAdapter m_sql_adapt;
        private static DataTable m_data_table;

        public int count = 0;
        public static string g_UserName;
        public static byte g_FirstLogin;

        public Login()
        {
            InitializeComponent();
            l_username.Select();
        }

        static void Reset_SQL()
        {
            // Reset all of the database stuff
            m_db_conn = null;
            m_sql_reader = null;
            m_sql_command = null;
            m_sql_adapt = null;
            m_data_table = null;
        }

        // Click login event
        private void l_CheckUser_Click(object sender, EventArgs e)
        {
            // If the username textbox or password textbox is empty
            if (l_username.Text.ToString() == "" || l_password.Text.ToString() == "")
            {
                MessageBox.Show("Please provide a username and password");
                return;
            }

            m_db_conn = DBConnect.OpenConnection();
            count = 0;
            // Check the number of users
            // This is just to test the database and make sure we
            // have the correct info
            try
            {
                m_sql_command = new MySqlCommand("SELECT * FROM user_login", m_db_conn);
                m_sql_adapt = new MySqlDataAdapter(m_sql_command);
                m_data_table = new DataTable();
                m_sql_adapt.Fill(m_data_table);
                count = m_data_table.Rows.Count;
                Console.WriteLine("Count: " + count);

                if (m_data_table == null)
                {
                    // Then alert the user and prompt them to the registration screen
                    MessageBox.Show("Congradulations you are the first user of this application.  Click the 'OK' button to go to the registration form.");
                    Registration registration_form = new Registration();
                    Hide();
                    registration_form.Show();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error + "\n");
            }

            // Once we are done close the connection
            m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            m_db_conn = DBConnect.OpenConnection();
            // Retrieve saltValue from the database
            string saltValue = "";
            try
            {
                m_sql_command = new MySqlCommand("SELECT * FROM user_login WHERE UserName = @find_username", m_db_conn);
                m_sql_command.Parameters.AddWithValue("@find_username", l_username.Text.ToString());
                m_sql_reader = m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (m_sql_reader.Read())
                {
                    saltValue = m_sql_reader.GetString(m_sql_reader.GetOrdinal("Salt"));
                    g_FirstLogin = m_sql_reader.GetByte(m_sql_reader.GetOrdinal("FirstLogin"));
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error + "\n");
            }
            // Once we are done close the connection
            m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            m_db_conn = DBConnect.OpenConnection();
            // Set up a command with the given query and associate
            // this with the current connection.
            try
            {
                m_sql_command = new MySqlCommand("SELECT * FROM user_login WHERE UserName = @find_username AND Password = @find_password", m_db_conn);
                m_sql_command.Parameters.AddWithValue("@find_username", l_username.Text.ToString());
                m_sql_command.Parameters.AddWithValue("@find_password", ProgramSecurity.hashPassword(l_password.Text.ToString(), saltValue));

                m_sql_adapt = new MySqlDataAdapter(m_sql_command);
                m_data_table = new DataTable();
                m_sql_adapt.Fill(m_data_table);
                count = m_data_table.Rows.Count;

                // If count is equal to 1 then we found a match on the database
                // If FirstLogin == 0 then it is False which means that this user
                // has logged in before
                if (count >= 1 && g_FirstLogin == 0)
                {
                    g_UserName = l_username.Text.ToString();
                    Hide();
                    UserTests user_options = new UserTests();
                    user_options.Show();
                }
                // If it is the user's first time loggin in
                // then we send them to go change their password
                else if (count >= 1 && g_FirstLogin == 1)
                {
                    MessageBox.Show("According to our system this is your first logging in.  Click OK to create a new password\n");
                    g_UserName = l_username.Text.ToString();
                    Hide();
                    PasswordReset reset_form = new PasswordReset();
                    reset_form.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed, Please try again!\n");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error + "\n");
            }
            // Once we are done close the connection
            m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
        }

        // If the cancel button is clicked
        private void l_cancel_Click(object sender, EventArgs e)
        {
            // Set the form to the start screen
            StartScreen start_form = new StartScreen();
            // Close the current form
            Hide();
            // Show the new form
            start_form.Show();
        }

        private void l_forgot_Click(object sender, EventArgs e)
        {
            // Set the Login to 2
            g_FirstLogin = 2;
            Hide();
            PasswordReset reset_form = new PasswordReset();
            reset_form.Show();
        }

        /*DONE*/
        /*
       private void Login_FormClosed(object sender, FormClosedEventArgs e)

        NAME
                Login_FormClosed - process that closes the application based on a click.

        SYNOPSIS
                void Login_FormClosed(object sender, EventArgs e)
                    sender         --> the object that is sending the click request.
                    e              --> the arguement being passed in.

        DESCRIPTION
                The purpose of this function is to exit the application.  If the user
                selects the "X" in the top right - hand corner the entire 
                application will be closed.

        RETURNS
                This function does not return anything

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}