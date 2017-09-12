using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data.SqlClient;
using System.Windows.Forms;
//Add the MySql Library
using MySql.Data.MySqlClient;

namespace RandomTestGenerator
{
    class DBConnect
    {
        public static MySqlConnection m_sql_connection;
        //static private string connectionString;
        static private string server;
        static private string port;
        static private string database;
        static private string uid;
        static private string password;

        //Initialize values
        /**/
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
        static public string ConnString()
        {
            server = "localhost";
            // server = "SMarhefka";
            port = "3306";
            database = "seniorproject";
            // uid = "AppUser";
            uid = "root";
            // password = "AppPass2017!";
            password = "Senior2017!";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" +
                "DATABASE=" + database + ";" + "UID=" + uid + ";" +
                "PASSWORD=" + password + ";";

            return connectionString;
        }

        /**/
        /*
        public static MySqlConnection OpenConnection()

        NAME
            OpenConnection - process that determines connect to database.

        SYNOPSIS
            MySqlConnection OpenConnection()
                This function does not take in any arguements

        DESCRIPTION
            The purpose of this function is to open the connection to the 
            database 

        RETURNS
            This function returns a MySqlConnection object

        AUTHOR
            Svetlana Marhefka

        DATE
            6:27pm 3/29/2017

        */
        /**/
        public static MySqlConnection OpenConnection()
        {
            try
            {
                string m_serverConnection = ConnString();
                m_sql_connection = new MySqlConnection(m_serverConnection);
                Console.WriteLine("Database connection opened successfully\n");
                m_sql_connection.Open();
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }
            return m_sql_connection;
        }

        /**/
        /*
        public static MySqlConnection CloseConnection()

        NAME
            CloseConnection - process that determines connect to database.

        SYNOPSIS
            MySqlConnection CloseConnection()
                This function does not take in any arguements

        DESCRIPTION
            The purpose of this function is to close the connection  

        RETURNS
            This function returns a MySqlConnection object

        AUTHOR
            Svetlana Marhefka

        DATE
            6:27pm 3/29/2017

        */
        /**/
        public static MySqlConnection CloseConnection()
        {
            try
            {
                m_sql_connection.Close();
                m_sql_connection.Dispose();
                Console.WriteLine("Database connection closed successfully\n");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return m_sql_connection;
        }
    }
}
