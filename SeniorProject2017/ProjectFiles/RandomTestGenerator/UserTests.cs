using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RandomTestGenerator
{
    public partial class UserTests : Form
    {

        
        private static MySqlConnection m_db_conn;
        private static MySqlCommand m_sql_command;
        private static MySqlDataReader m_sql_reader;
        private static MySqlDataAdapter m_sql_adapt;
        private static DataTable m_data_table;

        private static string m_UserName;
        public static int g_testId;
        public static int g_testId_Gen;
        public static bool g_edit;

        public UserTests()
        {
            InitializeComponent();
        }

        private static void Reset_SQL()
        {
            // Reset all of the database stuff
            m_db_conn = null;
            m_sql_reader = null;
            m_sql_command = null;
            m_sql_adapt = null;
            m_data_table = null;
        }

        private void UserOptions_Load(object sender, EventArgs e)
        {
            
            // TODO: This line of code loads data into the 'seniorProjectDataSet.test_info' table. You can move, or remove it, as needed.
            // this.test_infoTableAdapter.Fill(this.seniorProjectDataSet.test_info);
            string m_query;
            m_UserName = Login.g_UserName;
            // Initialize instructor to false
            // ReSharper disable once RedundantAssignment
            byte m_instructor = 0;

            m_instructor = CheckInstructor(m_UserName);
            // If m_instructor returns 0
            if (m_instructor == 0)
            {
                m_query = @"SELECT TestName, Course, Professor, Semester, Year, CreatedTime FROM test_info";
            }
            else
            {
                this.AllUserTests.Columns[3].Visible = false;
                m_query = @"SELECT TestName, Course, Semester, Year, CreatedTime FROM test_info";
            }

            m_db_conn = DBConnect.OpenConnection();
            try
            {
                m_sql_adapt = new MySqlDataAdapter(m_query, m_db_conn);
                using ( m_data_table = new DataTable())
                {
                    m_sql_adapt.Fill( m_data_table);
                    this.AllUserTests.DataSource = m_data_table;
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

        private static Byte CheckInstructor(string a_UserName)
        {
            const string m_query = "SELECT Instructor FROM user_login WHERE UserName = @a_UserName";
            byte l_result = 0;
            // Open the connection
            m_db_conn = DBConnect.OpenConnection();
            try
            {
                m_sql_command = new MySqlCommand(m_query, m_db_conn);
                // Check the first name
                m_sql_command.Parameters.AddWithValue("@a_UserName", a_UserName);
                m_sql_reader = m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (m_sql_reader.Read())
                {
                    l_result = m_sql_reader.GetByte(m_sql_reader.GetOrdinal("Instructor"));
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
            return l_result;
        }

        private void create_new_Click(object sender, EventArgs e)
        {
            Hide();
            CreateTest create_test = new CreateTest();
            create_test.Show();
        }

        private void Del_Row_Click(object sender, EventArgs e)
        {
            string l_query = "";
            int l_row_index = this.AllUserTests.CurrentCell.RowIndex;
            string l_testName = this.AllUserTests.Rows[l_row_index].Cells["Col1"].Value.ToString();
            int l_testId = Retrieve_Test_Id( l_testName );
            l_query = @"DELETE FROM test_info WHERE ﻿UserName = @input_username " +
                      @" AND TestId = @input_testid";
            Delete_SQL(l_testId, l_query);
            l_query = @"DELETE FROM question_table WHERE UserName = @input_username " +
                      @" AND TestId = @input_testid";
            Delete_SQL(l_testId, l_query);
            l_query = @"DELETE FROM answer_table WHERE UserName = @input_username " +
                      @" AND TestId = @input_testid";
            Delete_SQL(l_testId, l_query);
            this.AllUserTests.Rows.RemoveAt(l_row_index);
        }

        private static int Retrieve_Test_Id( string a_testName )
        {
            int l_result = 0;
            const string l_query = "SELECT TestId FROM test_info WHERE ﻿UserName = @input_username" +
                @" AND TestName = @input_testName";
            // Open the connection
            m_db_conn = DBConnect.OpenConnection();
            try
            {
                m_sql_command = new MySqlCommand(l_query, m_db_conn);
                // Add the username parameter
                m_sql_command.Parameters.AddWithValue
                ("@input_username", m_UserName.ToString());
                // Check the first name
                m_sql_command.Parameters.AddWithValue("@input_testName", a_testName);
                m_sql_reader = m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (m_sql_reader.Read())
                {
                    l_result = m_sql_reader.GetInt32(m_sql_reader.GetOrdinal("TestId"));
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
            return l_result;
        }

        private void Delete_SQL(int a_TestId, string a_query)
        {
            // Open the SQL connection
            m_db_conn = DBConnect.OpenConnection();
            try
            {
                // Create the sql query that will look up
                m_sql_command = new MySqlCommand(a_query, m_db_conn);
                // Add the username parameter
                m_sql_command.Parameters.AddWithValue
                ("@input_username", m_UserName.ToString());
                // Add the testid parameter
                m_sql_command.Parameters.AddWithValue("@input_testId", a_TestId);
                // Gives use the number of rows affected
                int l_rows_affected = m_sql_command.ExecuteNonQuery();
            }
            catch (Exception l_error)
            {
                MessageBox.Show("Error: " + l_error + "\n");
            }
            // Once we are done close the connection
            m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
        }

        private void Edit_Test_Click(object sender, EventArgs e)
        {
            int l_row_index = this.AllUserTests.CurrentCell.RowIndex;
            string l_testName = this.AllUserTests.Rows[l_row_index].Cells["Col1"].Value.ToString();
            g_testId = Retrieve_Test_Id(l_testName);
            g_edit = true;
            Hide();
            CreateTest create_test = new CreateTest();
            create_test.Show();
        }

        private void Gen_Click(object sender, EventArgs e)
        {
            int l_row_index = this.AllUserTests.CurrentCell.RowIndex;
            string l_testName = this.AllUserTests.Rows[l_row_index].Cells["Col1"].Value.ToString();
            g_testId_Gen = Retrieve_Test_Id(l_testName);
            Hide();
            GenTest gen_test = new GenTest();
            gen_test.Show();
        }

        private void UserTests_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
