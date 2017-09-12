using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace RandomTestGenerator
{
    public partial class GenTest : Form
    {
        private MySqlConnection m_db_conn;
        private MySqlCommand m_sql_command;
        private MySqlDataReader m_sql_reader;
        private MySqlDataAdapter m_sql_adapt;
        private DataTable m_data_table;

        // Initialize the global TestId
        private static readonly int g_InTestId = UserTests.g_testId_Gen;
        // Set the m_UserName to the login username
        private readonly string g_UserName = Login.g_UserName;
        // Set the number of questions
        private static int g_question_number;
        // Set the number of versions
        private static int g_version;
        // Set the current id of the question
        private static int g_curr_qid;
        ArrayList Random_Qids = new ArrayList();

        //private static Panel Panel2;
        //private PrintPreviewDialog printPreview = new PrintPreviewDialog();

        public GenTest()
        {
            InitializeComponent();
            //Panel2 = this.GenPanel;
            // Get the test name with the correct id
        }

        private void Reset_SQL()
        {
            // Reset all of the database stuff
            this.m_db_conn = null;
            this.m_sql_reader = null;
            this.m_sql_command = null;
            this.m_sql_adapt = null;
            this.m_data_table = null;
        }

        /*
        private static void doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Panel grd = Panel2;
            Bitmap bmp = new Bitmap(grd.Width, grd.Height, grd.CreateGraphics());
            grd.DrawToBitmap(bmp, new Rectangle(0, 0, grd.Width, grd.Height));
            RectangleF bounds = e.PageSettings.PrintableArea;
            float l_top = bounds.Left;
            float factor = ((float)bmp.Height / (float)bmp.Width);
            e.Graphics.DrawImage(bmp, (bounds.Left - 10), l_top, (bounds.Width - 25), factor * bounds.Width);
        }
        */
        /*
        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //float x = e.MarginBounds.Left;
            //float y = e.MarginBounds.Top;
            float x = e.PageSettings.PrintableArea.Left;
            float y = e.PageSettings.PrintableArea.Top;
            Bitmap bmp = new Bitmap(this.GenPanel.ClientRectangle.Width, this.GenPanel.ClientRectangle.Height);
            this.GenPanel.DrawToBitmap
            (bmp, new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height));
            e.Graphics.DrawImage((Image)bmp, x, y, e.PageSettings.PrintableArea.Width, e.PageSettings.PrintableArea.Height);
        }
        */

        private void button1_Click(object sender, System.EventArgs e)
        {
            /*
            System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
            doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPage);
            this.printPreview.Document = doc;
            this.printPreview.ShowDialog();
            doc.Print();
             */
        }

        private void Gen_Button_Click(object sender, EventArgs e)
        {
            Test_Title(g_InTestId);
            // Get the maximum number of questions
            int l_max_questions = Max_Questions();
            // Check the user input
            if (Check_User_Input(l_max_questions) == true)
            {
                for (int index = 1; index < g_version; index++)
                {
                    Gen_Rndm_Test(l_max_questions, g_question_number);
                }
            }
        }

        private void Gen_Rndm_Test(int a_max_quest, int a_question_num)
        {
            g_curr_qid = 1;
            IEnumerable<int> random_numbers = Enumerable.Range(1, a_max_quest).OrderBy(x => Guid.NewGuid()).Take(a_question_num);
            // For every number generated  
            foreach (int rnd_qid in random_numbers)
            {
                // Print the random number
                Console.WriteLine(rnd_qid);
                this.Random_Qids.Add(rnd_qid);
                // Get the Text associated with the number
                string l_qtxtbox = Question_Text(rnd_qid);
                SuspendLayout();
                // Add a row
                if (g_curr_qid > 1)
                {
                    this.Gen_Table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }
                // get the last row of the table
                int l_lastRow = this.Gen_Table.RowCount++;
                // create a new richtextbox
                // Initialize a new RichTextBox Object
                RichTextBox PrintBox = new RichTextBox()
                {
                    Name = "PrintBox" + g_curr_qid.ToString(),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(10, 5, 10, 5),
                    Font = new Font("Modern No. 20", 11),
                    AutoSize = true,
                    WordWrap = true
                };

                PrintBox.Text += g_curr_qid.ToString() + ". \t" + l_qtxtbox + "\n";
                // Add the question control
                this.Gen_Table.Controls.Add(PrintBox);
                this.Gen_Table.SetCellPosition
                (PrintBox, new TableLayoutPanelCellPosition(0, l_lastRow - 1));


                // At this point get the max number of options
                int l_max_options = Max_Options(rnd_qid);
                // Initialize a count variable
                int l_count = 0;
                // Initialize the radio button options
                string[] l_options = { "A. \t", "B. \t", "C. \t", "D. \t", "E. \t", "F. \t", "G. \t", "H. \t" };
                // if the max number is > 2 then we want to use the random function
                // to randomize the answers
                if (l_max_options > 2)
                {
                    // Create something to produce random numbers
                    IEnumerable<int> random_options = Enumerable.Range(1, l_max_options).OrderBy(x => Guid.NewGuid()).Take(l_max_options);
                    // For each random option
                    foreach (int option in random_options)
                    {
                        // Print out the number of the option
                        Console.WriteLine(option);
                        string l_opt_label = l_options[l_count];
                        // Get the answer value that is associated with that answer id
                        string l_option = AnswerText_Text(rnd_qid, option);

                        this.Gen_Table.Controls["PrintBox" + g_curr_qid].Text += l_opt_label + l_option.ToString() + "\n";

                        l_count++;
                    }
                }
                else
                {
                    for (int i_option = 0; i_option < l_max_options; i_option++)
                    {

                        string l_opt_label = l_options[l_count];
                        // Get the answer value that is associated with that answer id
                        string l_option = AnswerText_Text(rnd_qid, i_option);

                        this.Gen_Table.Controls["PrintBox" + g_curr_qid].Text += l_opt_label + l_option.ToString() + "\n";

                        l_count++;
                    }
                }
                // incremenet the current question id count by 1
                g_curr_qid++;
            }
        }

        private bool Check_User_Input(int a_max_questions)
        {
            bool complete;
            if (this.questions.Value < 0)
            {
                DialogResult result = MessageBox.Show
                ("Number of questions cannot be less than 0.  Press the OK button to set the number of questions to the minimum (5 questions) or CANCEL to reset the value\n",
                "Warning", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.questions.Value = 5;
                    g_question_number = 5;
                    complete = true;
                }
                complete = false;
            }
            else if (this.questions.Value == 0)
            {
                DialogResult result = MessageBox.Show
                ("Number of questions cannot be 0.  Press the OK button to set the number of questions to the minimum (5 questions) or CANCEL to reset the value\n",
                "Warning", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.questions.Value = 5;
                    g_question_number = 5;
                    complete = true;
                }
                complete = false;
            }
            else if (this.questions.Value > a_max_questions)
            {
                DialogResult result = MessageBox.Show
                ("Number of questions cannot be larger than " + a_max_questions + ".  Press the OK button to set the number of questions to the minimum (5 questions) or CANCEL to reset the value\n",
                "Warning", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.questions.Value = 5;
                    g_question_number = 5;
                    complete = true;
                }
                complete = false;
            }
            else if (this.versions.Value < 0 || this.versions.Value == 0 || this.versions.Value > 5)
            {
                MessageBox.Show
                ("Invalid value for number of version. Please choose a number between 1 - 5");
                complete = false;
            }
            else
            {
                g_question_number = Convert.ToInt32(this.questions.Value);
                g_version = Convert.ToInt32(this.versions.Value);
                complete = true;
            }
            return complete;
        }

        private int Max_Questions()
        {
            string l_query = @"SELECT max(QuestionId) AS NQuestionId FROM question_table " +
                             @"WHERE UserName = @input_username and TestId = @input_testid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            int l_max = 0;
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand(l_query, this.m_db_conn);
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_username", this.g_UserName.ToString());
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_testid", g_InTestId);
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_max = this.m_sql_reader.GetInt32
                        (this.m_sql_reader.GetOrdinal("NQuestionId"));
                    }
                    catch
                    {
                        l_max = 0;
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show("Error: " + l_error + "\n");
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            return l_max;
        }

        private int Max_Options(int a_question_id)
        {
            int l_quest_id = a_question_id;

            const string l_query = @"SELECT max(AnswerId) AS NAnswerId FROM answer_table "
                                   + @"WHERE UserName = @input_username AND TestId = @input_testid "
                                   + "AND QuestionId = @input_questionid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            int l_option_max = 0;
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand(l_query, this.m_db_conn);
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_username", this.g_UserName.ToString());
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_testid", g_InTestId);
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_questionid", l_quest_id);
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_option_max = this.m_sql_reader.GetInt32
                        (this.m_sql_reader.GetOrdinal("NAnswerId"));
                    }
                    catch
                    {
                        l_option_max = 0;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error + "\n");
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            return l_option_max;
        }

        private string Test_Title(int a_testid)
        {
            int l_quest_id = a_testid;

            string l_query = @"SELECT TestName FROM test_info " +
                             @"WHERE ﻿UserName = @input_username AND TestId = @input_testid ";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            string l_result = "";
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand(l_query, this.m_db_conn);
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_username", this.g_UserName.ToString());
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_testid", g_InTestId);
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_result = this.m_sql_reader.GetString
                        (this.m_sql_reader.GetOrdinal("QuestionText"));
                    }
                    catch
                    {
                        l_result = "";
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show("Error: " + l_error + "\n");
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
            return l_result;
        }

        private string Question_Text(int a_question_id)
        {
            int l_quest_id = a_question_id;

            string l_query = @"SELECT QuestionText FROM question_table " +
                             @"WHERE UserName = @input_username AND TestId = @input_testid " + "AND QuestionId = @input_questionid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            string l_result = "";
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand(l_query, this.m_db_conn);
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_username", this.g_UserName.ToString());
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_testid", g_InTestId);
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_questionid", l_quest_id);
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_result = this.m_sql_reader.GetString
                        (this.m_sql_reader.GetOrdinal("QuestionText"));
                    }
                    catch
                    {
                        l_result = "";
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show("Error: " + l_error + "\n");
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
            return l_result;
        }

        private string Question_Answer(int a_question_id)
        {
            int l_quest_id = a_question_id;

            string l_query = @"SELECT RightAnswer FROM question_table " +
                             @"WHERE UserName = @input_username AND TestId = @input_testid " + "AND QuestionId = @input_questionid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            string l_result = "";
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand(l_query, this.m_db_conn);
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_username", this.g_UserName.ToString());
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_testid", g_InTestId);
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_questionid", l_quest_id);
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_result = this.m_sql_reader.GetString
                        (this.m_sql_reader.GetOrdinal("RightAnswer"));
                    }
                    catch
                    {
                        l_result = "";
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show("Error: " + l_error + "\n");
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
            return l_result;
        }

        private string AnswerText_Text(int a_question_id, int a_answerid)
        {
            int l_quest_id = a_question_id;

            string l_query = @"SELECT AnswerValue FROM answer_table " +
                             @"WHERE UserName = @input_username AND TestId = @input_testid " + "AND QuestionId = @input_questionid AND AnswerId = @input_answerid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            string l_result = "";
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand(l_query, this.m_db_conn);
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_username", this.g_UserName.ToString());
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_testid", g_InTestId);
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_questionid", l_quest_id);
                // Add the answerid parameter
                this.m_sql_command.Parameters.AddWithValue
                ("@input_answerid", a_answerid);
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_result = this.m_sql_reader.GetString
                        (this.m_sql_reader.GetOrdinal("AnswerValue"));
                    }
                    catch
                    {
                        l_result = "";
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show("Error: " + l_error + "\n");
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
            return l_result;
        }

        string details;
        private void Write_To_Doc(string data)
        {
            try
            {
                // create a new microsoft office word object
                Microsoft.Office.Interop.Word.Application l_word_application = new Microsoft.Office.Interop.Word.Application();
                l_word_application.Visible = false;
                // don't show the word animation
                l_word_application.ShowAnimation = false;

                Object l_missing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Document l_writer = l_word_application.Documents.Add
                (ref l_missing, ref l_missing, ref l_missing, ref l_missing);

                l_writer.Content.SetRange(0, 0);
                l_writer.Content.Text = data + Environment.NewLine;

                Object l_filename = @"C:\User Data\TestTest.docx";
                l_writer.Save();
                l_writer.Close(ref l_missing, ref l_missing, ref l_missing);
                l_writer = null;
                l_word_application.Quit(ref l_missing, ref l_missing, ref l_missing);
                l_word_application = null;

                MessageBox.Show("Document saved in " + l_filename.ToString());
            }
            catch (Exception l_error)
            {
                Console.WriteLine("Error: " + l_error + "\n");
                throw;
            }

        }


        private void Send_Word_Click(object sender, EventArgs e)
        {
            int l_count = 0;
            foreach (Control cntl in this.GenPanel.Controls)
            {
                if (cntl is RichTextBox)
                {
                    l_count++;
                    details += cntl.Text;
                }
            }
            Write_To_Doc(details);
        }

        private void GenTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
