using System;
using System.Windows.Forms;
//Add the MySql Library
using MySql.Data.MySqlClient;

namespace RandomTestGenerator
{
    public partial class PasswordReset : Form
    {
        // The global varaible FirstName
        private static string g_FirstName;
        // The global varaible LastName
        private static string g_LastName;
        // The global variable ResetUsername
        private static string g_resetUsername;
        private static Byte g_resetLogin;

        static MySqlConnection m_db_conn;
        // static DataTable m_data_table = null;
        static MySqlCommand m_sql_command;
        // static MySqlDataAdapter m_sql_adapt = null;
        static MySqlDataReader m_sql_reader;

        public PasswordReset()
        {
            InitializeComponent();
            g_resetLogin = Login.g_FirstLogin;
            SetupForm();
        }

        private void SetupForm()
        {
            // If the username from the Login Screen is not empty
            // If the user is loging in for the first time
            if (Login.g_UserName != "" && g_resetLogin == 1)
            {
                try
                {
                    // We know to get to this point they entered
                    // their temporary pasword correctly so we can
                    // just retrieve their username from the intial
                    // login screen
                    this.reset_username.ReadOnly = true;
                    g_resetUsername = Login.g_UserName;
                    // We are going to automatically put in the username
                    this.reset_username.Text += g_resetUsername;
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: " + error);
                }
            }
            else if(g_resetLogin == 2)
            {
                this.reset_username.ReadOnly = false;
                this.r_updatePassword.Text = "";
                this.r_updatePassword.Text += "Reset Password";
                this.reset_desc.Text = "";
                this.reset_desc.Text += "Please provide your username or First and Last Name";
                this.label_pass.Text = "";
                this.label_pass.Text += "First Name:";
                this.label_conn.Text = "";
                this.label_conn.Text += "Last Name:";

                g_resetUsername = "";
            }
        }

        /*DONE*/
        /*
        private void l_cancel_Click(object sender, EventArgs e)

        NAME
                l_cancel_Click - process that displays the previous form based on a click.

        SYNOPSIS
                void l_cancel_Click(object sender, EventArgs e)
                    sender         --> the object that is sending the click request.
                    e              --> the arguement being passed in.

        DESCRIPTION
                The purpose of this function is to go back to the the previous screen.  If the user
                selects "Cancel" from the current screen (the password reset screen) the 
                current screen will be closed and a new start screen will be created.

        RETURNS
                This function does not return anything

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private void l_cancel_Click(object sender, EventArgs e)
        {
            // Set the form to the start screen
            StartScreen start_form = new StartScreen();
            // Close the current form
            Hide();
            // Show the new form
            start_form.Show();
        }


        private void r_updatePassword_Click(object sender, EventArgs e)
        {
            // If the user is logging in for the first time
            // we just need to update the password
            if (g_resetLogin == 1)
            {
                // Check to make sure that the new password is 
                // the same as the confirmation text box
                if (this.reset_pass.Text.ToString() != this.reset_conn.Text.ToString())
                {
                    // If not then display an error message
                    MessageBox.Show("The passwords do not match\n");
                }

                // Generate a new salt value
                string m_newSalt = ProgramSecurity.generateSalt();
                // Hash the password the user gave us
                string m_intputPass = ProgramSecurity.hashPassword( this.reset_pass.Text.ToString(), m_newSalt);
                // Set the localLogin to 0 meaning this isn't
                // their first time logging in
                Byte localLogin = 0;
                // Update the table in the database
                UpdatePass(m_newSalt, m_intputPass, localLogin);
                // Cleanup variables
                m_newSalt = "";
                m_intputPass = "";

                // Assuming that the update went well go
                // to the user options screeen
                Hide();
                UserTests user_options = new UserTests();
                user_options.Show();
            }
            // If the user forgot their password then I want
            // the user to enter their username so that
            // there is something to compare against.
            if (g_resetLogin == 2)
            {
                g_resetUsername = "";

                // Check to make sure that the user has enetered
                // either their username or first and last name
                if (this.reset_username.Text.ToString() == "" && (this.reset_pass.Text.ToString() == "" && this.reset_conn.Text.ToString() == ""))
                {
                    MessageBox.Show("Please provide ether your:\nusername\nOR\nFirst and Last Name\n");
                }

                string m_userEmail = "";
                // If the username is filled in
                if (this.reset_username.Text.ToString() != "")
                {
                    // Set the global username to what was filled
                    // in
                    g_resetUsername = this.reset_username.Text.ToString();
                    // Want to find the first and last
                    // name based on the username
                    FindFullName(g_resetUsername);
                    Console.WriteLine("g_FirstName: " + g_FirstName);
                    Console.WriteLine("g_LastName: " + g_LastName);
                }
                // Otherwise set the first and last name
                else
                {
                    // Clear First and Last Name
                    g_FirstName = "";
                    g_LastName = "";
                    // Set First and Last Name
                    g_FirstName = this.reset_pass.Text.ToString();
                    g_LastName = this.reset_conn.Text.ToString();
                    FindUserName(g_FirstName, g_LastName);
                }
                // Find the email associated with username
                m_userEmail = EmailUser.FindEmail(g_resetUsername).ToString();
                // Generate a new salt value
                string m_newSalt = ProgramSecurity.generateSalt();
                // Generate a new random password for the email
                string m_newPassEmail = ProgramSecurity.generatePassword();
                //  Hash the random password
                string m_newPass = ProgramSecurity.hashPassword(m_newPassEmail, m_newSalt);
                // set the byte to 1 so that the user has to reset
                // their password
                Byte m_firstLogin = 1;
                // Update the table with the new values
                UpdatePass(m_newSalt, m_newPass, m_firstLogin);
                // Send the temporary password to the user
                EmailUser.SendEmail(g_FirstName, g_LastName, m_userEmail, g_resetUsername, m_newPassEmail);

                // Clean everything up
                g_FirstName = "";
                g_LastName = "";
                m_userEmail = "";
                g_resetUsername = "";
                m_newPassEmail = "";
                // Switch to the initial login screen
                SwitchToLogin();
            }
        }


        private static void UpdatePass(string a_inputSalt, string a_inputPass, Byte a_firstLogin)
        {
            try
            {
                string m_query = "Update user_login SET Password = @new_password, Salt = @new_salt, FirstLogin = @update_FirstLogin WHERE UserName = @username";
                // Open the connection
                m_db_conn = DBConnect.OpenConnection();
                m_sql_command = new MySqlCommand(m_query, m_db_conn);
                // Update the password
                m_sql_command.Parameters.AddWithValue("@new_password", a_inputPass);
                // Update the salt value
                m_sql_command.Parameters.AddWithValue("@new_salt", a_inputSalt.ToString());
                // Update the first login
                m_sql_command.Parameters.AddWithValue("@update_FirstLogin", a_firstLogin);
                m_sql_command.Parameters.AddWithValue("@username", g_resetUsername);
                m_sql_command.ExecuteNonQuery();
                m_db_conn = DBConnect.CloseConnection();

                m_query = "";
                a_inputSalt = "";
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error);
            }
        }

        
        private void FindFullName(string a_username)
        {
            string m_query = "SELECT FirstName,  LastName FROM user_login WHERE UserName = @a_username";
            // Open the connection
            m_db_conn = DBConnect.OpenConnection();
            try
            {
                m_sql_command = new MySqlCommand(m_query, m_db_conn);
                // Check the last name
                m_sql_command.Parameters.AddWithValue("@a_username", a_username);
                m_sql_reader = m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (m_sql_reader.Read())
                {
                    g_FirstName = m_sql_reader.GetString(m_sql_reader.GetOrdinal("FirstName"));
                    g_LastName = m_sql_reader.GetString(m_sql_reader.GetOrdinal("LastName"));
                }
                m_db_conn = DBConnect.CloseConnection();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error + "\n");
            }
        }

        private void FindUserName(string a_firstName, string a_lastName)
        {
            g_resetUsername = "";
            string m_query = "SELECT UserName FROM user_login WHERE FirstName = @a_firstName AND LastName = @a_lastName";
            // Open the connection
            m_db_conn = DBConnect.OpenConnection();
            try
            {
                m_sql_command = new MySqlCommand(m_query, m_db_conn);
                // Check the first name
                m_sql_command.Parameters.AddWithValue("@a_firstName", a_firstName);
                // Check the last name
                m_sql_command.Parameters.AddWithValue("@a_lastName", a_lastName);
                m_sql_reader = m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (m_sql_reader.Read())
                {
                    g_resetUsername = m_sql_reader.GetString(m_sql_reader.GetOrdinal("UserName"));
                }
                m_db_conn = DBConnect.CloseConnection();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error + "\n");
            }
        }

        /*DONE*/
        /*
        private void SwitchToLogin()

        NAME
                SwitchToLogin - process that displays a new login form.

        SYNOPSIS
                void SwitchToLogin()
                    This function does not take in any arguements

        DESCRIPTION
                The purpose of this function is to exit to the current screen (the password reset
                screen) and show a new login page.

        RETURNS
                This function does not return anything

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private void SwitchToLogin()
        {
            // Hide current screen
            Hide();
            Login new_login = new Login();
            new_login.Show();
        }

        /*DONE*/
        /*
       private void PasswordReset_FormClosed(object sender, FormClosedEventArgs e)

        NAME
                PasswordReset_FormClosed - process that closes the application based on a click.

        SYNOPSIS
                void PasswordReset_FormClosed(object sender, EventArgs e)
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
        private void PasswordReset_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
