using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
// Allows us to use regular expressions
using System.Text.RegularExpressions;
using System.Security.Cryptography;
//Add the MySql Library
using MySql.Data;
using MySql.Data.MySqlClient;


namespace RandomTestGenerator
{
    public partial class Registration : Form
    {

        private MySqlConnection m_db_conn = null;
        private DataTable m_data_table = null;
        private MySqlCommand m_sql_command = null;
        private MySqlDataAdapter m_sql_adapt = null;

        // Generate a temporary password
        private static string g_tempassword = ProgramSecurity.generatePassword();
        // A value of zero is considered false
        private Byte instructorBool = 0;
        // Set first login bool to true
        private Byte FLoginBool = 1;
        private bool is_complete = false;
        private int duplicate_count = 0;

        private string g_userName;

        /*DONE*/
        /*
        public Registration()

        NAME
                Registration - Initializes the form.

        SYNOPSIS
                void Registration()
                    This function does not take in any arguements

        DESCRIPTION
                The purpose of this function is to initialize the registration form.

        RETURNS
                This function does not return anything

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        public Registration()
        {
            InitializeComponent();
        }

        /*DONE*/
        /*
        private void R_Newuser_Click(object sender, EventArgs e)

        NAME
            R_Newuser_Click - process that processes information from the form

        SYNOPSIS
            void St_Exit_Click(object sender, EventArgs e)
                sender         --> the object that is sending the click request.
                e              --> the arguement being passed in.

        DESCRIPTION
            The purpose of this function is to check all of the input fields and make sure
            that the user has filled out every field.  Once all of the information has been 
            verified an e-mail is sent to the user with a new username and temporary password
        
        RETURNS
            This function does not return anything

        AUTHOR
            Svetlana Marhefka

        DATE
            6:27pm 3/29/2017

        */
        /**/
        private void R_Newuser_Click(object sender, EventArgs e)
        {
            string m_firstName = "";
            string m_lastName = "";
            string m_schoolName = "";
            string m_userName = "";
            string m_email = "";

            // If the form is not complete
            if (IsComplete() == false)
            {
                // Alert user to what is missing
                try
                {
                    if (r_fname.Text.ToString() == "")
                    {
                        MessageBox.Show("Please enter your first name\n");
                    }
                    else if (r_lname.Text.ToString() == "")
                    {
                        MessageBox.Show("Please enter your last name\n");
                    }
                    else if (r_school.Text.ToString() == "")
                    {
                        MessageBox.Show("Please enter your school name\n");
                    }
                    else if (r_email.Text.ToString() == "")
                    {
                        MessageBox.Show("Please enter an email\n");
                    }
                    else if (!r_instructor.Checked && !r_student.Checked)
                    {
                        MessageBox.Show("Please select if you are a student or an instructor.\n");
                    }
                }
                catch (Exception e_message)
                {
                    MessageBox.Show("Error: " + e_message.ToString());
                }
            }
            else
            {
                m_firstName = r_fname.Text.ToString();
                m_lastName = r_lname.Text.ToString();
                m_schoolName = r_email.Text.ToString();
                // Verify that the user has entered their email address correctly
                if (IsValidEmail(r_email.Text.ToString()) == true)
                {
                    // If the email is valid then set it to the e-mail variable
                    m_email = r_email.Text.ToString();
                }
                else
                {
                    // Otherwise let the user know that their email address is invalid
                    MessageBox.Show("The email entered is invalid.  Please re-enter your email.\n", "Warning", MessageBoxButtons.OK);
                }
                if (r_instructor.Checked == true)
                {
                    // Then set the bitValue to 1 which sql will take as true
                    instructorBool = 1;
                }
                else if (r_student.Checked == true)
                {
                    // Then set the bitValue to 1 which sql will take as false
                    instructorBool = 0;
                }
                // Generate the username
                m_userName = m_firstName.Substring(0, 1).ToLower().Trim() + m_lastName.ToLower().Trim();
                // Check for duplicate users
                CheckUserDuplicates(m_firstName, m_lastName, m_userName, m_schoolName, m_email);
                // Switch to the start screen
                Switch_Screens();
            }
        }

        /*DONE*/
        /*
        private void R_Cancel_Click(object sender, EventArgs e)

        NAME
                R_Cancel_Click - process that displays the previous form based on a click.

        SYNOPSIS
                void St_Exit_Click(object sender, EventArgs e)
                    sender         --> the object that is sending the click request.
                    e              --> the arguement being passed in.

        DESCRIPTION
                The purpose of this function is to exit to the initial start screen.  If the user
                selects "Cancel" from the current screen (the registration screen) the 
                current screen will be closed and a new start screen will be created.

        RETURNS
                This function does not return anything

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private void R_Cancel_Click(object sender, EventArgs e)
        {
            // Set the form to the start screen
            StartScreen start_form = new StartScreen();
            // Hide the current form
            this.Hide();
            // Show a new login screen
            start_form.Show();
        }

        /*DONE*/
        /*
        private void Switch_Screens()

        NAME
                Switch_Screens - process that displays a new login form.

        SYNOPSIS
                void Switch_Screens()
                    This function does not take in any arguements

        DESCRIPTION
                The purpose of this function is to exit to the current screen (the registration
                page) and show a new login page.  This function is called once a new user has
                been verified.

        RETURNS
                This function does not return anything

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private void Switch_Screens()
        {
            // Set the form to the login page screen
            Login login_form = new Login();
            // Hide the current form
            this.Hide();
            // Show the new form
            login_form.Show();
        }

        /*DONE*/
        /*
        private bool IsComplete()

        NAME
                IsComplete - process that determines if the registration form is complete.

        SYNOPSIS
                bool IsComplete()
                    This function does not take in any arguements

        DESCRIPTION
               

        RETURNS
                This function returns true if the registration form has been completely
                filled out.  Otherwise this functions returns false.

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private bool IsComplete()
        {
            try
            {
                // If every field is filled then the form is
                // considered complete
                if (!(r_fname.Text.ToString() == "") && !(r_lname.Text.ToString() == "") && !(r_school.Text.ToString() == "") && !(r_email.Text.ToString() == "") && (r_instructor.Checked == true) || (r_student.Checked == true))
                {
                    is_complete = true;
                }
                // Or else tell the user what is missing
                else
                {
                    is_complete = false;
                }
            }
            catch
            {
                is_complete = false;
            }
            return is_complete;
        }

        /*DONE*/
        /*
        private void InsertIntoUserInfo(string a_username, string a_firstName, string a_lastName, string a_school, string a_email, string a_tempass, byte a_instrtype)

        NAME
            InsertIntoUserInfo - Process that works with user information

        SYNOPSIS
            void InsertIntoUserInfo
                a_username         --> the user's username.
                a_firstName        --> the user's firstname.
                a_lastName         --> the user's lastname.
                a_school           --> the user's school name.
                a_email            --> the user's school e-mail address.
                a_tempass          --> the user's temporary password
                a_instrtype        --> the user's classification

        DESCRIPTION
            The purpose of this function is to insert the user info into the user_info table
            which is stored in the mysql database
        RETURNS
            This function returns nothing

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private void InsertIntoUserInfo(string a_username, string a_firstName, string a_lastName, string a_school, string a_email, byte a_instrtype, string a_hashpass, string a_saltval)
        {
            try
            {

                m_sql_command = new MySqlCommand("INSERT INTO user_login (UserName, FirstName, LastName, School, SchoolEmail, Instructor, FirstLogin, Password, Salt) VALUES (@insert_username, @insert_firstname, @insert_lastname, @insert_schoolname, @insert_schoolemail, @insert_instrtype, @insert_firstlogin, @insert_password, @insert_salt)", m_db_conn);
                m_sql_command.Parameters.AddWithValue("@insert_username", a_username);
                m_sql_command.Parameters.AddWithValue("@insert_firstname", a_firstName);
                m_sql_command.Parameters.AddWithValue("@insert_lastname", a_lastName);
                m_sql_command.Parameters.AddWithValue("@insert_schoolname", a_school);
                m_sql_command.Parameters.AddWithValue("@insert_schoolemail", a_email);
                m_sql_command.Parameters.AddWithValue("@insert_instrtype", a_instrtype);
                m_sql_command.Parameters.AddWithValue("@insert_firstlogin", FLoginBool);
                m_sql_command.Parameters.AddWithValue("@insert_password", a_hashpass);
                m_sql_command.Parameters.AddWithValue("@insert_salt", a_saltval);
                m_sql_command.ExecuteNonQuery();
            }
            catch (Exception m_error)
            {
                MessageBox.Show(m_error.ToString());
            }

        }
        /**/
        /*
        private void CheckUserDuplicates(string a_firstName, string a_lastName, string a_userName, string a_schoolName, string a_email)

        NAME
                CheckUserDuplicates - Process that works with information.

        SYNOPSIS
            void CheckUserDuplicates(string a_firstName, string a_lastName, string a_userName, string a_schoolName, string a_email)
                a_firstName        --> the user's firstname.
                a_lastName         --> the user's lastname.
                a_userName         --> the user's username.
                a_school           --> the user's school name.
                a_email            --> the user's school e-mail address.

        DESCRIPTION
            The purpose of this function is to make sure that the a new user is not a duplicate

        RETURNS
            This function returns nothing

        AUTHOR
            Svetlana Marhefka

        DATE
            6:27pm 3/29/2017

        */
        /**/
        private void CheckUserDuplicates(string a_firstName, string a_lastName, string a_userName, string a_schoolName, string a_email)
        {
            // Check to see if the user information is already
            // in the user_login table
            bool user_duplicate = IsUserDuplicate(a_firstName, a_lastName, a_userName, a_email);
            // Case where the user is already in the user_login table
            if (user_duplicate == true)
            {
                // Alert the user that they are already in the database
                MessageBox.Show("It appears that you are already in the system please try loging in\n");
            }
            // If the user information from the form is not the 
            // same as the information found in the database then
            // we just have to change the username
            else
            {
                string jquery = "";
                int jquery_count = 1;
                jquery = "SELECT UserName FROM user_login WHERE UserName = @find_username";
                bool username_duplicate = IsUsernameDuplicate(jquery, a_userName, jquery_count);
                // Increment jquery_count
                jquery_count++;
                // If there are duplicate usernames
                if (username_duplicate == true)
                {
                    // Reset the query
                    jquery = "";
                    // Look for all usernames that are similar to the
                    // duplicate username
                    jquery = "SELECT UserName FROM user_login WHERE UserName LIKE @find_username";
                    // Print out the query
                    Console.WriteLine(jquery.ToString());
                    username_duplicate = IsUsernameDuplicate(jquery, a_userName, jquery_count);
                    a_userName = a_userName + duplicate_count.ToString();
                }
                // Generate Salt Value
                string l_saltval = ProgramSecurity.generateSalt();
                // Create a local instance of the temp_password
                // Hash the Generated Password
                string l_hashpass = ProgramSecurity.hashPassword(g_tempassword, l_saltval);

                // Changes userName so that it can be from the local scope
                // to a global scope
                g_userName = a_userName;
                // If there are no duplicates usernames
                m_db_conn = DBConnect.OpenConnection();
                // Insert user information into the user_info table
                InsertIntoUserInfo(g_userName, a_firstName, a_lastName, a_schoolName, a_email, instructorBool, l_hashpass, l_saltval);
                m_db_conn = DBConnect.CloseConnection();

                // Generate the e-mail
                EmailUser.SendEmail(a_firstName, a_lastName, a_email, g_userName, g_tempassword);

                // Clean up temporary password variable
                g_tempassword = "";

            }
        }
        
        /**/
        /*
        private bool IsUsernameDuplicate(string a_jquery, string a_username, int a_querycount)
        
        NAME
            IsUsernameDuplicate - process that determines if the registration form is complete.

        SYNOPSIS
            bool IsUsernameDuplicate(string a_jquery, string a_username, int a_querycount)
                a_userName         --> the user's username.
        DESCRIPTION
               

        RETURNS
            This function returns true if the input username is a duplicate
            Otherwise this functions returns false.

        AUTHOR
            Svetlana Marhefka

        DATE
            6:27pm 3/29/2017

        */
        /**/
        private bool IsUsernameDuplicate(string a_jquery, string a_username, int a_querycount)
        {
            // Assume that we start with an empty database
            bool is_duplicate = false;
            // Check to see if we can connect to the database
            m_db_conn = DBConnect.OpenConnection();
            // Set up a command with the following query
            // Want to look in user_login and check to see if we have any duplicates
            m_sql_command = new MySqlCommand(a_jquery.ToString(), m_db_conn);
            m_sql_command.Parameters.AddWithValue("@find_username", a_username);
            m_sql_adapt = new MySqlDataAdapter(m_sql_command);

            m_data_table = new DataTable();
            m_sql_adapt.Fill(m_data_table);
            int count = 0;
            count = m_data_table.Rows.Count;
            Console.WriteLine("Count: " + count);
            // Check to see if the database returned anything
            if (m_data_table == null)
            {
                MessageBox.Show("There is no data stored in the database\n");
                is_duplicate = false;
            }
            else
            {

                if (count > 0)
                {
                    Console.WriteLine("Duplicate username found in user_login\n");
                    is_duplicate = true;
                    if (a_querycount == 2)
                    {
                        duplicate_count = m_data_table.Rows.Count;
                        Console.WriteLine("duplicate_count: " + duplicate_count);
                    }
                }
                else
                {
                    Console.WriteLine("No duplicates fund\n");
                    is_duplicate = false;
                }
            }
            m_db_conn = DBConnect.CloseConnection();
            return is_duplicate;
        }

        /*DONE*/
        /*
        private bool IsComplete()

        NAME
                IsComplete - process that determines if the registration form is complete.

        SYNOPSIS
                bool IsComplete()
                    This function does not take in any arguements

        DESCRIPTION
               

        RETURNS
                This function returns true if the registration form has been completely
                filled out.  Otherwise this functions returns false.

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private bool IsUserDuplicate(string a_firstName, string a_lastName, string a_username, string a_email)
        {
            // Assume that we start with an empty database
            bool is_duplicate = false;
            // Check to see if we can connect to the database
            m_db_conn = DBConnect.OpenConnection();
            // Set up a command with the following query
            // Want to look in user_login and check to see if we have any duplicates
            m_sql_command = new MySqlCommand("SELECT * FROM user_login WHERE FirstName = @find_firstname AND LastName = @find_lastname AND UserName = @find_username AND SchoolEmail = @find_email", m_db_conn);
            m_sql_command.Parameters.AddWithValue("@find_username", a_username);
            m_sql_command.Parameters.AddWithValue("@find_firstname", a_firstName);
            m_sql_command.Parameters.AddWithValue("@find_lastname", a_lastName);
            m_sql_command.Parameters.AddWithValue("@find_email", a_email);

            m_sql_adapt = new MySqlDataAdapter(m_sql_command);
            m_data_table = new DataTable();
            m_sql_adapt.Fill(m_data_table);
            int count = 0;
            count = m_data_table.Rows.Count;
            if (count > 0)
            {
                Console.WriteLine("Duplicate found in user_login\n");
                is_duplicate = true;
            }
            else
            {
                Console.WriteLine("No duplicates found in user_login\n");
                is_duplicate = false;
            }
            m_db_conn = DBConnect.CloseConnection();
            return is_duplicate;
        }

        /*DONE*/
        /*
        private bool IsValidEmail(string a_email)

        NAME
            IsValidEmail - process that determines if the registration form is complete.

        SYNOPSIS
            bool IsValidEmail(string a_email)
                a_email            --> the user's school e-mail address.

        DESCRIPTION
            The purpose of this function is to make sure that the e-mail address that
            the user has entered is valid

        RETURNS
            This function returns true if the registration form has been completely
            filled out.  Otherwise this functions returns false.

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private bool IsValidEmail(string a_email)
        {
            try
            {
                var valid_email = new System.Net.Mail.MailAddress(a_email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*DONE*/
        /*
       private void Registration_FormClosed(object sender, FormClosedEventArgs e)

        NAME
                Registration_FormClosed - process that closes the application based on a click.

        SYNOPSIS
                void Registration_FormClosed(object sender, EventArgs e)
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
        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
