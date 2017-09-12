

using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//Add the MySql Library
using MySql.Data.MySqlClient;

namespace RandomTestGenerator
{
    public partial class CreateTest : Form
    {
        // Global information that will be used when we need to use the database
        private MySqlConnection m_db_conn;
        private DataTable m_data_table;
        private MySqlCommand m_sql_command;
        private MySqlDataAdapter m_sql_adapt;
        private MySqlDataReader m_sql_reader;

        // Global controls that will be used
        private Button m_add_answer;
        private Button m_rem_answer;
        private Panel m_panel;
        private Panel PrevPanel;

        // Set the creatation time
        private DateTime m_created_time;

        // Set the m_UserName to the login username
        private readonly string m_user_name = Login.g_UserName;

        // Initialize the global TestId
        private static int g_test_id;

        // This is the global question count
        private static int g_quest_count = 1;

        // Set the radio button count (count the number of radiobuttons created)
        private static int g_radio_buttons;

        // Create a new global variable which will keep track of the question id
        private static int g_ques_id = 1;

        // Create a edit varaible which is set from the UserTests form
        private static bool g_edit_test = UserTests.g_edit;
        private static readonly int g_incoming_testid = UserTests.g_testId;

        /*DONE*/
        /*
        public CreateTest()

        NAME
            CreateTest - process that initializes the user form.

        SYNOPSIS
            CreateTest()
        
        DESCRIPTION
            The purpose of this function is to initialize the UserTest form
            
        RETURNS
            This function returns nothing

        AUTHOR
            Svetlana Marhefka
        
        DATE
            6:27pm 3/29/2017
        */
        /**/
        public CreateTest()
        {
            InitializeComponent();
            Set_Creation_Time();
            this.SplitC1.Visible = false;
            if (Check_Edit() == true)
            {
                // Reset Question Count
                g_quest_count = 1;
                Get_Previous_Form_Info( g_incoming_testid );
                Cont_Click( this.c_insert_test, EventArgs.Empty );
            }
        }

        /*DONE*/
        /*
        private void Set_Creation_Time()

        NAME
            Set_Creation_Time - process that initializes the user form.

        SYNOPSIS
            void Set_Creation_Time()
        
        DESCRIPTION
            The purpose of this function is to set the time created of a new test
            
        RETURNS
            This function returns nothing

        AUTHOR
            Svetlana Marhefka
        
        DATE
            6:27pm 3/29/2017
        */
        /**/
        private void Set_Creation_Time()
        {
            this.m_created_time = DateTime.Now;
        }

        /*DONE*/
        /*
        private void Reset_SQL()

        NAME
            Reset_SQL - process that initializes the SQL variables.

        SYNOPSIS
            void Reset_SQL()
        
        DESCRIPTION
            The purpose of this function is to initialize all the SQL variables to NULL
            
        RETURNS
            This function returns nothing

        AUTHOR
            Svetlana Marhefka
        
        DATE
            6:27pm 3/29/2017
        */
        /**/
        private void Reset_SQL()
        {
            // Reset all of the database stuff
            this.m_db_conn = null;
            this.m_sql_reader = null;
            this.m_sql_command = null;
            this.m_sql_adapt = null;
            this.m_data_table = null;
        }

        /*DONE*/
        /*
        private void Reset_SQL()

        NAME
            Reset_SQL - process that initializes the SQL variables.

        SYNOPSIS
            void Reset_SQL()
        
        DESCRIPTION
            The purpose of this function is to initialize all the SQL variables to NULL
            
        RETURNS
            This function returns nothing

        AUTHOR
            Svetlana Marhefka
        
        DATE
            6:27pm 3/29/2017
        */
        /**/
        private void Cont_Click( object sender, EventArgs e )
        {
            // this is the preview on an edit call
            if (g_edit_test == true && Check_User_Input() == true)
            {
                // Allow the user to see the next screens
                this.SplitC1.Visible = true;
                // find the largest number of questions in the database
                int l_max_qid = Max_Questions();
                SuspendLayout();
                // Console.WriteLine(g_quest_count);
                // Go through each question
                while (g_quest_count <= l_max_qid)
                {
                    Console.WriteLine( g_quest_count );
                    // Add a new question
                    Add_New_Question();
                    Console.WriteLine( g_quest_count );
                    int l_prev_count = g_quest_count - 1;
                    // Find question text associated with question id
                    string l_qtxt = Question_Text( l_prev_count );
                    // Find the correct answer associated with the question id
                    string l_qans = Question_Answer( l_prev_count );
                    // Want to fill in the textbox associated with that question
                    this.TLayPan.Controls["QTxtBox" + (l_prev_count)].Text += l_qtxt;
                    // Want to find the number of options in answer table that is associated with a
                    // particular question
                    int l_max_opt = Max_Options( l_prev_count );
                    // Want to fill in the options associated with that question id
                    for ( int l_count = 1; l_count <= l_max_opt; l_count++ )
                    {
                        // Add an answer option
                        add_answer_Click
                        ( this.TLayPan.Controls["oPan" + (l_prev_count).ToString()]
                        .Controls["add" + (g_quest_count - 1).ToString()],
                        EventArgs.Empty );
                        // find the answer text associated with the question id and answer id
                        string l_atxt = AnswerText_Text( g_quest_count - 1, l_count );
                        // Set the text of the answer option
                        this.TLayPan.Controls["oPan" + (l_prev_count).ToString()]
                        .Controls["txtbox" + (l_count - 1).ToString()].Text += l_atxt;
                        // If option txt is the same as the correct answer
                        if (l_atxt.Trim().ToString() == l_qans.Trim().ToString())
                        {
                            // Set the current control
                            Control l_control =
                            this.TLayPan.Controls["oPan" + (l_prev_count).ToString()]
                            .Controls["txtbox" + (l_count - 1).ToString()];
                            // Set the previous control
                            Control l_next_control =
                            this.TLayPan.Controls["oPan" + (l_prev_count).ToString()]
                            .GetNextControl( l_control, false );
                            // if l_next_control is a radiobutton
                            if (l_next_control is RadioButton)
                            {
                                ((RadioButton) l_next_control).Checked = true;
                            }
                        }
                    }
                    //Console.WriteLine(g_quest_count);
                    //int l_arg_questid = (g_quest_count - 1);
                    //Send_To_Preview(l_arg_questid);
                }
                ResumeLayout();
                g_edit_test = false;
            }
            else if (Check_User_Input() == true)
            {
                this.SplitC1.Visible = true;
                Console.WriteLine( g_quest_count );
                // Add a new question
                Add_New_Question();
            }
        }

        private static bool Check_Edit()
        {
            if (g_edit_test == true)
            {
                return true;
            }
            return false;
        }

        private void Get_Previous_Form_Info( int a_income_testid )
        {
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();

            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand
                ( "SELECT * FROM test_info WHERE ﻿UserName = @input_username AND TestId = @input_testId",
                this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testname parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_testId", a_income_testid.ToString() );
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Check to see if the reader has any data
                if (this.m_sql_reader.Read())
                {
                    this.input1.Text += this.m_sql_reader.GetString
                    ( this.m_sql_reader.GetOrdinal( "TestName" ) );
                    this.input2.Text += this.m_sql_reader.GetString
                    ( this.m_sql_reader.GetOrdinal( "Professor" ) );
                    this.input3.Text += this.m_sql_reader.GetString
                    ( this.m_sql_reader.GetOrdinal( "Course" ) );
                    this.input4.Text += this.m_sql_reader.GetString
                    ( this.m_sql_reader.GetOrdinal( "Semester" ) );
                    this.input5.Value = this.m_sql_reader.GetInt32
                    ( this.m_sql_reader.GetOrdinal( "Year" ) );
                }
                // Set the global TestId to the TestId in the database
                g_test_id = g_incoming_testid;
            }
            catch (Exception error)
            {
                MessageBox.Show( "Error: " + error + "\n" );
            }
            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
        }

        private bool Check_User_Input()
        {
            bool l_comp;
            // Check to make sure that all fields are filled in
            if (this.input1.Text.Length == 0)
            {
                MessageBox.Show( @"Please fill in the Test Name: field\n" );
                l_comp = false;
            }
            else if (this.input2.Text.Length == 0)
            {
                MessageBox.Show( @"Please fill in the 'Professor:' field\n" );
                l_comp = false;
            }
            else if (this.input3.Text.Length == 0)
            {
                //MessageBox.Show( @"Please fill in the 'Course:' field" );
                l_comp = false;
            }
            else if (this.input4.Text.Length == 0 || this.input4.Text.Length > 1)
            {
                MessageBox.Show
                ( @"Please fill in the 'Semester (Spring/Fall):' field \n" +
                  "Enter either S (for Spring) or F (for Fall)\n" );
                l_comp = false;
            }
            else
            {
                l_comp = true;
            }
            return l_comp;
        }

        private void Add_New_Question()
        {
            if (g_quest_count > 1)
            {
                Console.WriteLine( g_quest_count );
                int l_arg_questid = (g_quest_count - 1);
                Send_To_Preview( l_arg_questid );
            }

            this.TLayPan.SuspendLayout();
            Add_Q_Part1();
            Create_Panel();
            this.TLayPan.ResumeLayout();
        }

        private void Add_Q_Part1()
        {
            int i_col = 0;
            if (g_quest_count > 1)
            {
                this.TLayPan.RowStyles.Add( new RowStyle( SizeType.AutoSize ) );
            }

            int lastRow = this.TLayPan.RowCount++;
            // Initialize a new Label Object
            Label QLbl = new Label
            {
                Name = "QLbl" + g_quest_count,
                Text = g_quest_count.ToString() + @".",
                Font = new Font( "Modern No. 20", 11 ),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                AutoSize = true,
                Margin = new Padding( 10, 10, 10, 10 )
            };
            // Initialize a new RichTextBox Object
            RichTextBox QTxtBox = new RichTextBox()
            {
                Name = "QTxtBox" + g_quest_count.ToString(),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Margin = new Padding( 0, 10, 10, 5 ),
                Font = new Font( "Modern No. 20", 11 ),
                WordWrap = true
            };

            this.TLayPan.Controls.Add( QLbl );
            this.TLayPan.SetCellPosition
            ( QLbl, new TableLayoutPanelCellPosition( i_col, lastRow - 1 ) );

            i_col++;
            this.TLayPan.Controls.Add( QTxtBox );
            Height = Get_Txtbox_Height
            ( this.TLayPan.Controls["QTxtBox" + g_quest_count] );
            this.TLayPan.SetCellPosition
            ( QTxtBox, new TableLayoutPanelCellPosition( i_col, lastRow - 1 ) );
            this.TLayPan.SuspendLayout();

            QTxtBox.SuspendLayout();
            QTxtBox.TextChanged += new EventHandler( Qtxtbox_TextChanged );
            QTxtBox.ResumeLayout();
            this.TLayPan.ResumeLayout();
        }

        private void Create_Panel()
        {
            this.TLayPan.SuspendLayout();
            this.TLayPan.RowStyles.Add( new RowStyle( SizeType.AutoSize ) );
            int lastRow = this.TLayPan.RowCount++;

            this.m_panel = new Panel()
            {
                Name = "oPan" + g_quest_count.ToString(),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                Margin = new Padding( 10, 5, 10, 15 ),
                BackColor = Color.AntiqueWhite
            };

            this.TLayPan.Controls.Add( this.m_panel );

            this.TLayPan.SetCellPosition
            ( this.m_panel, new TableLayoutPanelCellPosition( 1, lastRow - 1 ) );
            this.TLayPan.SetColumnSpan( this.m_panel, 2 );
            this.TLayPan.ResumeLayout();

            Add_Buttons();
            g_quest_count++;
        }

        private void Add_Buttons()
        {
            this.m_panel.SuspendLayout();
            /*********************************************************/
            // Create a new button in our Panel
            // This button will be used to add an option
            this.m_add_answer = new Button
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Left = 10,
                Top = 5,
                Text = @"+",
                Name = "add" + g_quest_count.ToString(),
                Font = new Font( "Microsoft Sans Serif", 8f, FontStyle.Bold ),
                Width = 35,
                TabIndex = 0
            };
            this.m_panel.Controls.Add( this.m_add_answer );
            /*********************************************************/
            // Create a another button in our Panel
            // This button will be used to delete an option
            this.m_rem_answer = new Button
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Location =
                new Point( this.m_add_answer.Right + 5, this.m_add_answer.Top ),
                Text = @"-",
                Name = "rem" + g_quest_count.ToString(),
                Font = new Font( "Microsoft Sans Serif", 8f, FontStyle.Bold ),
                Width = 35
            };
            this.m_add_answer.TabIndex = 1;
            this.m_panel.Controls.Add( this.m_rem_answer );
            /*********************************************************/
            //Create a new label:
            Label l_note = new Label
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Location =
                new Point
                ( this.m_rem_answer.Right + 5,
                this.m_rem_answer.Margin.Top + this.m_rem_answer.Top ),
                Text = @"Note: Please SELECT the CORRECT answer",
                Font = new Font( "Modern No. 20", 10 ),
                AutoSize = true
            };
            this.m_panel.Controls.Add( l_note );
            this.m_panel.ResumeLayout();
            // reset m_Radiobuttons in every new panel
            g_radio_buttons = 0;
            //If the "+" button is clicked
            this.m_add_answer.Click += add_answer_Click;
            //If the "-" button is clicked
            this.m_rem_answer.Click += rem_answer_Click;
            /*********************************************************/
        }

        private static int Get_Txtbox_Height( object sender )
        {
            ((RichTextBox) sender).SuspendLayout();
            Font tempFont = ((RichTextBox) sender).Font;
            int textLength = ((RichTextBox) sender).Text.Length;
            int textLines = ((RichTextBox) sender).GetLineFromCharIndex( textLength ) + 1;
            int l_margin = ((RichTextBox) sender).Bounds.Height -
                           ((RichTextBox) sender).ClientSize.Height;
            ((RichTextBox) sender).Height =
            TextRenderer.MeasureText( " ", tempFont ).Height * textLines + l_margin + 2;
            ((RichTextBox) sender).ResumeLayout();
            int l_height = ((RichTextBox) sender).Height;
            return l_height;
        }

        private void Qtxtbox_TextChanged( object sender, EventArgs e )
        {
            ((RichTextBox) sender).SuspendLayout();
            Font tempFont = ((RichTextBox) sender).Font;
            int textLength = ((RichTextBox) sender).Text.Length;
            int textLines = ((RichTextBox) sender).GetLineFromCharIndex( textLength ) + 1;
            int margin = ((RichTextBox) sender).Bounds.Height -
                         ((RichTextBox) sender).ClientSize.Height;
            ((RichTextBox) sender).Height =
            TextRenderer.MeasureText( " ", tempFont ).Height * textLines + margin + 2;

            Console.WriteLine( ((RichTextBox) sender).Name );
            string control_int = ((RichTextBox) sender).Name.Substring
            ( (((RichTextBox) sender).Name.Length - 1), 1 );
            int l_cntl_int = Convert.ToInt32( control_int );
            if (this.PrePan != null &&
                this.PrePan.Controls["PQuesBox" + l_cntl_int.ToString()] != null)
            {
                Control l_control =
                this.PrePan.Controls["PQuesBox" + l_cntl_int.ToString()];
                Font l_tempFont = ((RichTextBox) l_control).Font;
                int l_textLength = ((RichTextBox) l_control).Text.Length;
                int l_textLines = ((RichTextBox) l_control).GetLineFromCharIndex
                                  ( l_textLength ) + 1;
                int l_margin = ((RichTextBox) l_control).Bounds.Height -
                               ((RichTextBox) l_control).ClientSize.Height;
                ((RichTextBox) l_control).Height =
                TextRenderer.MeasureText( " ", l_tempFont ).Height * l_textLines +
                l_margin + 2;

                this.PrePan.Controls["PQuesBox" + l_cntl_int.ToString()].Text = "";
                this.PrePan.Controls["PQuesBox" + l_cntl_int.ToString()].Text +=
                ((RichTextBox) sender).Text;
            }

            this.TLayPan.ResumeLayout();
        }

        private void add_answer_Click( object sender, EventArgs e )
        {
            // Find out which "add button" is sending us the call
            Console.WriteLine( ((Button) sender).Name );
            int l_bcount = ((Button) sender).Name.Length;
            string l_bnum = ((Button) sender).Name.Substring( l_bcount - 1, 1 );
            int b_number = Convert.ToInt32( l_bnum );

            // retrieves the number of textboxes
            int l_txtnumber = Find_Num_Options( b_number );

            g_radio_buttons = l_txtnumber;
            // Initialize the radio button options
            string[] optionNames = {"A", "B", "C", "D", "E", "F", "G", "H"};
            // This sets our max to 8 options
            if (g_radio_buttons < 8)
            {
                const int l_padding = 10;
                RadioButton option = new RadioButton();
                TextBox txtbox = new TextBox();
                //Set the name for option as option and radionum count
                option.Name = "option" + g_radio_buttons.ToString();
                option.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                option.TextAlign = ContentAlignment.MiddleLeft;
                option.Font = new Font( "Modern No. 20", 10 );
                option.Width = 30;
                //Set the name for o_input as o_input and radionum count
                txtbox.Name = "txtbox" + g_radio_buttons.ToString();
                txtbox.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                txtbox.Font = new Font( "Modern No. 20", 10 );
                txtbox.Width = this.m_panel.Width - (option.Width + (3 * l_padding));
                // if it is the first time that the user clicked on the add button then
                // we can just add it to the panel
                if (g_radio_buttons == 0)
                {
                    option.Location = new Point
                    ( l_padding, this.m_add_answer.Bottom + l_padding );
                    option.Text += optionNames[g_radio_buttons];
                    txtbox.Location = new Point
                    ( option.Right + l_padding, option.Location.Y );
                    Console.WriteLine( this.m_panel.Name );
                    this.TLayPan.Controls["oPan" + b_number.ToString()].Controls.Add
                    ( option );
                    this.TLayPan.Controls["oPan" + b_number.ToString()].Controls.Add
                    ( txtbox );
                    txtbox.Select();
                    g_radio_buttons++;
                }
                else
                {
                    Console.WriteLine( this.m_panel.Controls.Count );
                    try
                    {
                        // If the previous textbox is not empty
                        Control l_PanCon =
                        this.TLayPan.Controls["oPan" + b_number.ToString()];
                        if (l_PanCon.Controls["txtbox" + (g_radio_buttons - 1).ToString()]
                            .Text.ToString() != "")
                        {
                            int prevtxtbox_bottom =
                            l_PanCon.Controls["txtbox" + (g_radio_buttons - 1).ToString()]
                            .Bottom;
                            option.Location = new Point
                            ( l_padding, prevtxtbox_bottom + l_padding );
                            option.Text += optionNames[g_radio_buttons];
                            txtbox.Location = new Point
                            ( option.Right + l_padding, option.Location.Y );
                            l_PanCon.Controls.Add( option );
                            l_PanCon.Controls.Add( txtbox );
                            txtbox.Select();
                        }
                        else
                        {
                            MessageBox.Show
                            ( @"Please provide an answer for option " +
                              optionNames[g_radio_buttons - 1].ToString() + "\n" );
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show( "Error: " + error.ToString() );
                    }
                }
            }
        }

        private void rem_answer_Click( object sender, EventArgs e )
        {
            // Find out which "rem button" is sending us the call
            Console.WriteLine( ((Button) sender).Name );
            int l_bcount = ((Button) sender).Name.Length;
            string l_bnum = ((Button) sender).Name.Substring( l_bcount - 1, 1 );
            int b_number = Convert.ToInt32( l_bnum );

            // retrieves the number of textboxes
            int l_txtnumber = Find_Num_Options( b_number );
            g_radio_buttons = l_txtnumber;

            if (g_radio_buttons > 0)
            {
                // If the previous textbox is not empty
                Control l_PanCon = this.TLayPan.Controls["oPan" + b_number.ToString()];
                if (l_PanCon.Controls["txtbox" + (g_radio_buttons - 1).ToString()].Text
                    .ToString() != "")
                {
                    DialogResult result = MessageBox.Show
                    ( "Are you sure you want to delete this option?", "Warning",
                    MessageBoxButtons.OKCancel );
                    if (result == DialogResult.OK)
                    {
                        // Dispose of the txtbox
                        l_PanCon.Controls["txtbox" + (g_radio_buttons - 1).ToString()]
                        .Dispose();
                        // Dispose of the radio option
                        l_PanCon.Controls["option" + (g_radio_buttons - 1).ToString()]
                        .Dispose();
                    }
                }
                else
                {
                    // Dispose of the txtbox
                    l_PanCon.Controls["txtbox" + (g_radio_buttons - 1).ToString()]
                    .Dispose();
                    // Dispose of the radio option
                    l_PanCon.Controls["option" + (g_radio_buttons - 1).ToString()]
                    .Dispose();
                }
            }
        }

        private void Add_Ques_Click( object sender, EventArgs e )
        {
            if (this.TLayPan.Controls["QTxtBox" + (g_quest_count - 1).ToString()].Text
                .ToString() == "")
            {
                MessageBox.Show
                ( "Question " + (g_quest_count - 1) + " can't be empty", "Warning",
                MessageBoxButtons.OK );
            }
            else if (Check_For_Answer() == false)
            {
                MessageBox.Show
                ( "Question " + (g_quest_count - 1) + " must have" +
                  " an answer selected", "Warning", MessageBoxButtons.OK );
            }
            else
            {
                Add_New_Question();
            }
        }

        private void Delete_Ques_Click( object sender, EventArgs e )
        {
            int l_question_count = g_quest_count - 1;
            // Just prints out the number of controls
            Console.WriteLine( this.TLayPan.Controls.Count );
            int l_last = this.TLayPan.Controls.Count;
            Console.WriteLine( this.TLayPan.RowCount );
            int prev_last = this.PrePan.Controls.Count;
            if (l_question_count > 1)
            {
                // Delete the Panel
                this.TLayPan.Controls["oPan" + l_question_count.ToString()].Dispose();
                l_last--;
                // Delete the txtbox
                this.TLayPan.Controls.RemoveAt( l_last - 1 );
                l_last--;
                // Delete the label
                this.TLayPan.Controls.RemoveAt( l_last - 1 );
                Console.WriteLine( this.TLayPan.RowCount );
                // Delete the last two rows
                this.TLayPan.RowStyles.RemoveAt( 0 );
                this.TLayPan.RowCount--;
                this.TLayPan.RowStyles.RemoveAt( 0 );
                this.TLayPan.RowCount--;

                if (this.PrePan.Controls["PrevPanel" + l_question_count.ToString()] !=
                    null)
                {
                    // Delete the panel on the preview side
                    this.PrePan.Controls["PrevPanel" + l_question_count.ToString()]
                    .Dispose();
                    prev_last--;
                    // Delete the textbox on the preview side
                    this.PrePan.Controls.RemoveAt( prev_last - 1 );
                    // Delete the last two rows
                    this.PrePan.RowStyles.RemoveAt( 0 );
                    this.PrePan.RowCount--;
                    this.PrePan.RowStyles.RemoveAt( 0 );
                    this.PrePan.RowCount--;
                }
                // Decrease the global count by 1
                g_quest_count--;
            }
            else
            {
                MessageBox.Show( "Must have at least one question\n" );
            }
        }

        // The purpose of this function is to check to make sure that one 
        // of the radiobuttons has be selected. 
        private bool Check_For_Answer()
        {
            bool l_bool = false;
            foreach ( Control cntl in this.m_panel.Controls )
            {
                if (cntl is RadioButton && ((RadioButton) cntl).Checked)
                {
                    l_bool = true;
                    break;
                }
            }
            return l_bool;
        }

        private void Send_To_Preview( int a_questionId )
        {
            int l_quest_id = a_questionId;
            SuspendLayout();
            // Add a row
            if (l_quest_id > 1)
            {
                this.PrePan.RowStyles.Add( new RowStyle( SizeType.AutoSize ) );
            }
            int l_lastRow = this.PrePan.RowCount++;
            // Initialize a new RichTextBox Object
            RichTextBox PQuesBox = new RichTextBox()
            {
                Name = "PQuesBox" + l_quest_id.ToString(),
                Dock = DockStyle.Fill,
                Font = new Font( "Modern No. 20", 10 ),
                Height = this.TLayPan.Controls["QTxtBox" + l_quest_id].Height,
                Width = this.PrePan.Width - 20,
                AutoSize = true,
                Multiline = true,
                WordWrap = true
            };
            // Set the text of the Preview question
            PQuesBox.Text += this.TLayPan.Controls["QLbl" + l_quest_id].Text + " " +
                             this.TLayPan.Controls["QTxtBox" + l_quest_id].Text;
            // Set the Textbox to read only
            PQuesBox.ReadOnly = true;
            // Add the question control
            this.PrePan.Controls.Add( PQuesBox );
            this.PrePan.SetCellPosition
            ( PQuesBox, new TableLayoutPanelCellPosition( 0, l_lastRow - 1 ) );

            // Add another row
            l_lastRow = this.PrePan.RowCount++;
            this.PrevPanel = new Panel()
            {
                Name = "PrevPanel" + l_quest_id.ToString(),
                Dock = DockStyle.Fill,
                Width = this.PrePan.Width - 20,
                AutoSize = true,
            };
            this.PrePan.Controls.Add( this.PrevPanel );
            this.PrePan.SetCellPosition
            ( this.PrevPanel, new TableLayoutPanelCellPosition( 0, l_lastRow - 1 ) );

            // Add User Options to the preview panel
            Add_Prev_Options( l_quest_id );
            ResumeLayout();
        }

        private void Add_Prev_Options( int a_question_id )
        {
            // Initialize the radio button options
            string[] optionNames = {"A", "B", "C", "D", "E", "F", "G", "H"};

            int l_option_number = 0;
            // Find the number of options that are in the user form
            int l_max_options = Find_Num_Options( a_question_id );

            while (l_option_number < l_max_options)
            {
                // Initialize a new TextBox Object
                TextBox PreviewOption = new TextBox()
                {
                    Name = "PrevOp" + l_option_number.ToString(),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Margin = new Padding( 0 ),
                    Font = new Font( "Modern No. 20", 10 ),
                    Width = this.PrePan.Controls["PQuesBox" + a_question_id].Width,
                };

                // Set the Option Text
                PreviewOption.Text += optionNames[l_option_number].ToString() + ". " +
                                      this.TLayPan.Controls["oPan" + a_question_id]
                                      .Controls["txtbox" + l_option_number].Text;

                // Set option text to readonly
                PreviewOption.ReadOnly = true;

                if (l_option_number == 0)
                {
                    PreviewOption.Location = new Point( 10, 0 );
                }
                else
                {
                    PreviewOption.Location = new Point
                    ( 10,
                    this.PrePan.Controls["PrevPanel" + a_question_id.ToString()]
                    .Controls["PrevOp" + (l_option_number - 1).ToString()].Bottom + 10 );
                }
                // Add option to Preview Panel
                this.PrePan.Controls["PrevPanel" + a_question_id].Controls.Add
                ( PreviewOption );
                // Increment the l_option_number
                l_option_number++;
            }
        }

        // The purpose of this function is to save all of the information that has been entered
        // into the user form up until the time that the user clicked the save button.
        private void Save_Test_Click( object sender, EventArgs e )
        {
            Save_User_Form();
        }

        private void Save_User_Form()
        {
            // This is used to reset the question_id
            g_ques_id = 1;
            DialogResult result = MessageBox.Show
            ( "Saving will overwrite previous data.  Do you wish to proceed?", "Warning",
            MessageBoxButtons.OKCancel );
            if (result == DialogResult.OK)
            {
                // Collects the test information
                Test_Info();
                Check_Questions_Against_DB();
                // While the global question id is less then the global question count
                // Or in other words for every question
                while (g_ques_id < g_quest_count)
                {
                    // Check the number of options in the panel to make sure there are no "excess"
                    // question options.  This fucntion will also delete options if there are too
                    // many in the database
                    Check_Options_Against_DB( g_ques_id );
                    // Insert the question information into the database
                    Insert_Form_Question( g_ques_id );
                    // Insert the option information into the database
                    Insert_Form_Option( g_ques_id );
                    // Increment the count the question id
                    g_ques_id++;
                }
            }
        }

        private void Test_Info()
        {
            // Initialize l_testname
            string l_testname = this.input1.Text.ToString();
            // Initialize l_professor
            string l_professor = this.input2.Text.ToString();
            // Initialize l_course
            string l_course = this.input3.Text.ToString();
            // Initialize l_semester
            string l_semester = this.input4.Text.ToString();
            // Initialize l_year
            int l_year = Convert.ToInt32( this.input5.Value );
            // If the function returns true then we have a
            // duplicate test name
            if (Check_Testname_Dup( l_testname ) == true)
            {
                // If we have a duplicate of a test name I want to check to see if it is a duplicate
                // test.  If it is the same test then I want to retrieve the Testid
                if (Check_Against_All
                    ( l_testname, l_professor, l_course, l_semester, l_year ) == true)
                {
                    // Open the SQL connection
                    this.m_db_conn = DBConnect.OpenConnection();

                    try
                    {
                        // Create the sql query that will look up everything from test_info
                        this.m_sql_command = new MySqlCommand
                        ( "SELECT TestId FROM test_info WHERE ﻿UserName = @input_username AND TestName = @input_testname",
                        this.m_db_conn );
                        // Add the username parameter
                        this.m_sql_command.Parameters.AddWithValue
                        ( "@input_username", this.m_user_name.ToString() );
                        // Add the testname parameter
                        this.m_sql_command.Parameters.AddWithValue
                        ( "@input_testname", l_testname.ToString() );
                        this.m_sql_reader = this.m_sql_command.ExecuteReader();
                        // Checks to see if the reader has data:
                        if (this.m_sql_reader.Read())
                        {
                            // Set the global TestId to the TestId in the database
                            g_test_id = this.m_sql_reader.GetInt32
                            ( this.m_sql_reader.GetOrdinal( "TestId" ) );
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show( "Error: " + error + "\n" );
                    }
                    // Once we are done close the connection
                    this.m_db_conn = DBConnect.CloseConnection();
                    // Reset all of the sql variables
                    Reset_SQL();
                }
            }
            else
            {
                Insert_Into_TestInfo
                ( l_testname, l_professor, l_course, l_semester, l_year );
            }
        }

        /*DONE*/
        /*
        private int Total_Tests()

        NAME
            Total_Tests - process that collects information.

        SYNOPSIS
            int Total_Tests()
        
        DESCRIPTION
            The purpose of this function is to get the count of all of the test that a 
            specific user has ever created.
            
        RETURNS
            This function returns an integer value

        AUTHOR
            Svetlana Marhefka
        
        DATE
            6:27pm 3/29/2017
        */
        /**/
        private int Total_Tests()
        {
            // Set the local count to 0
            int count = 0;
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();

            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand
                ( "SELECT * FROM test_info WHERE ﻿UserName = @input_username",
                this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );

                this.m_sql_adapt = new MySqlDataAdapter( this.m_sql_command );
                this.m_data_table = new DataTable();
                this.m_sql_adapt.Fill( this.m_data_table );
                count = this.m_data_table.Rows.Count;
            }
            catch (Exception error)
            {
                MessageBox.Show( "Error: " + error + "\n" );
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            // We want to return count
            return count;
        }

        private int Max_Questions()
        {
            string l_query =
            @"SELECT max(QuestionId) AS NQuestionId FROM question_table " +
            @"WHERE UserName = @input_username and TestId = @input_testid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            int l_max = 0;
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand( l_query, this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_testid", g_test_id );
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_max = this.m_sql_reader.GetInt32
                        ( this.m_sql_reader.GetOrdinal( "NQuestionId" ) );
                    }
                    catch
                    {
                        l_max = 0;
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show( "Error: " + l_error + "\n" );
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            return l_max;
        }

        private int Max_Options( int a_question_id )
        {
            int l_quest_id = a_question_id;

            const string l_query =
            @"SELECT max(AnswerId) AS NAnswerId FROM answer_table " +
            @"WHERE UserName = @input_username AND TestId = @input_testid " +
            "AND QuestionId = @input_questionid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            int l_option_max = 0;
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand( l_query, this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_testid", g_test_id );
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_questionid", l_quest_id );
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_option_max = this.m_sql_reader.GetInt32
                        ( this.m_sql_reader.GetOrdinal( "NAnswerId" ) );
                    }
                    catch
                    {
                        l_option_max = 0;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show( "Error: " + error + "\n" );
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            return l_option_max;
        }

        private int Find_Num_Options( int a_question_id )
        {
            int txtcount = 0;
            foreach ( Control l_cont in this.TLayPan
            .Controls["oPan" + a_question_id.ToString()].Controls )
                if (l_cont is TextBox)
                {
                    txtcount++;
                }
            return txtcount;
        }

        private string Question_Text( int a_questionid )
        {
            string l_query = @"SELECT QuestionText FROM question_table " +
                             @"WHERE UserName = @input_username AND TestId = @input_testid " +
                             "AND QuestionId = @input_questionid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            string l_result = "";
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand( l_query, this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_testid", g_test_id );
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_questionid", a_questionid );
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_result = this.m_sql_reader.GetString
                        ( this.m_sql_reader.GetOrdinal( "QuestionText" ) );
                    }
                    catch
                    {
                        l_result = "";
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show( "Error: " + l_error + "\n" );
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
            return l_result;
        }

        private string Question_Answer( int a_questionid )
        {
            string l_query = @"SELECT RightAnswer FROM question_table " +
                             @"WHERE UserName = @input_username AND TestId = @input_testid " +
                             "AND QuestionId = @input_questionid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            string l_result = "";
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand( l_query, this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_testid", g_test_id );
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_questionid", a_questionid );
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_result = this.m_sql_reader.GetString
                        ( this.m_sql_reader.GetOrdinal( "RightAnswer" ) );
                    }
                    catch
                    {
                        l_result = "";
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show( "Error: " + l_error + "\n" );
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
            return l_result;
        }

        private string AnswerText_Text( int a_questionid, int a_answerid )
        {
            string l_query = @"SELECT AnswerValue FROM answer_table " +
                             @"WHERE UserName = @input_username AND TestId = @input_testid " +
                             "AND QuestionId = @input_questionid AND AnswerId = @input_answerid";
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            string l_result = "";
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand( l_query, this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_testid", g_test_id );
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_questionid", a_questionid );
                // Add the answerid parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_answerid", a_answerid );
                this.m_sql_reader = this.m_sql_command.ExecuteReader();
                // Checks to see if the reader has data:
                if (this.m_sql_reader.Read())
                {
                    try
                    {
                        // Set the global TestId to the TestId in the database
                        l_result = this.m_sql_reader.GetString
                        ( this.m_sql_reader.GetOrdinal( "AnswerValue" ) );
                    }
                    catch
                    {
                        l_result = "";
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show( "Error: " + l_error + "\n" );
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
            return l_result;
        }

        // The purpose of this function is to check the number of options against the database
        private void Check_Options_Against_DB( int a_question_id )
        {
            int l_quest_id = a_question_id;
            // Determine the number of options in the form
            int l_quest_options = Find_Num_Options( l_quest_id );
            // Determine the number of options in the database
            int l_answer_max = Max_Options( l_quest_id );
            // If the number of answers in the form is less then what we have in the database
            // then we have an "excess number of answer" in the database.  We must get rid of these
            // before we can save the answers
            if (l_quest_options < l_answer_max)
            {
                Delete_Extra_Options( l_quest_id, l_quest_options );
            }
        }

        // The purpose of this function is to compare the number of questions in the form to the 
        // number of questions in the database.  If there are more questions in the database then
        // there are in the form I am going to delete the "excess" questions.
        private void Check_Questions_Against_DB()
        {
            // Initialize the query
            string l_query;
            // Set the number of questions in the form equal to the global question count - 1
            int l_numquest_inform = (g_quest_count - 1);
            int l_quest_max = Max_Questions();
            // If the number of questions in the form is less then what we have in the database
            // then we have an "excess of questions" in the database.  We must get rid of these
            // before we can save the questions
            if (l_numquest_inform < l_quest_max)
            {
                // the query to delete questions from the questions table in the database.  
                // This function is called during the save incase there are more questions
                // in the database then there are in the user form.
                l_query =
                @"DELETE FROM question_table WHERE UserName = @input_username " +
                @" AND TestId = @input_testid AND QuestionId > @input_questioninfo";
                // then we need to get rid of the extra questions in the database before we can
                // resave them 
                Delete_Three_Params( l_numquest_inform, l_query );
                // Now we need to set the query to delete the answers from the answer table that
                // are associated with the questions
                l_query = @"DELETE FROM answer_table WHERE UserName = @input_username " +
                          @" AND TestId = @input_testid AND QuestionId > @input_questioninfo";
                // We also need to get rid of the extra options associated with those questions
                Delete_Three_Params( l_numquest_inform, l_query );
            }
        }

        // The purpose of this 
        private void Delete_Three_Params( int a_numquest_inform, string a_query )
        {
            int l_rows_affected = 0;
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            try
            {
                // Create the sql query that will look up
                this.m_sql_command = new MySqlCommand( a_query, this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_testId", g_test_id );
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_questioninfo", a_numquest_inform );
                // Gives use the number of rows affected
                l_rows_affected = this.m_sql_command.ExecuteNonQuery();
            }
            catch (Exception l_error)
            {
                MessageBox.Show( "Error: " + l_error + "\n" );
            }
            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
            Console.WriteLine( "Rows affected: " + l_rows_affected );
        }

        // The purpose of this function is to delete the user options weather it's because a
        // question is being removed or because there are more user options in the database then
        // there are in the form.  This function is crucial to saving the form correctly.
        private void Delete_Extra_Options( int a_question_id, int a_answer_id )
        {
            int l_rows_affected = 0;
            string l_query =
            @"DELETE FROM answer_table WHERE UserName = @input_username " +
            @" AND TestId = @input_testid AND" +
            @" QuestionId = @input_questid AND AnswerId > @input_answerid";

            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            try
            {
                // Create the sql query that will look up
                this.m_sql_command = new MySqlCommand( l_query, this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_testId", g_test_id );
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_questid", a_question_id );
                // Add the answerid parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_answerid", a_answer_id );
                // Gives use the number of rows affected
                l_rows_affected = this.m_sql_command.ExecuteNonQuery();
            }
            catch (Exception l_error)
            {
                MessageBox.Show( "Error: " + l_error + "\n" );
            }
            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
            Console.WriteLine( "Rows affected: " + l_rows_affected );
        }

        /*DONE*/
        /*
        private void Insert_Into_TestInfo( string a_testname, string a_professor, string a_course,
                                           string a_semester, int a_year )

        NAME
            Total_Tests - process that collects information.

        SYNOPSIS
            void Insert_Into_TestInfo( string a_testname, string a_professor, string a_course,
                                           string a_semester, int a_year )
                a_testname      --> the name of the test that we will be inserting
                a_professor     --> the name of the professor that we will be inserting
                a_course        --> the name of the course that we will be inserting
                a_semester      --> the semester that we will be inserting
                a_year          --> the year that we will be inserting
        
        DESCRIPTION
            The purpose of this function is to save the information given at the begining of our
            create test to the database.  This information includes the testname, the professors
            name, the class, the semester the course was taken and the year it was taken in.
            Additional information that will be placed into the database is the username and
            the created time which was set as soon as the user form is initialized.
            
        RETURNS
            This function returns nothing

        AUTHOR
            Svetlana Marhefka
        
        DATE
            6:27pm 3/29/2017
        */
        /**/
        private void Insert_Into_TestInfo( string a_testname, string a_professor,
                                           string a_course, string a_semester,
                                           int a_year )
        {
            int l_testnum = Total_Tests();
            g_test_id = l_testnum + 1;
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            try
            {
                // Create the sql query that will look up everything from test_info
                this.m_sql_command = new MySqlCommand
                ( "INSERT INTO test_info (﻿UserName, TestId, TestName, Course, Professor, Semester, Year, CreatedTime) VALUES (@input_username, @input_testid, @input_testname, @input_course, @input_professor, @input_semester, @input_year, @input_created)",
                this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_testid", g_test_id );
                // Add the testname parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_testname", a_testname.Trim().ToString() );
                // Add the course parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_course", a_course.Trim().ToString() );
                // Add the professor parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_professor", a_professor.Trim().ToString() );
                // Add the semester parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_semester", a_semester.Trim().ToString() );
                // Add the year parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_year", a_year );
                // Add the created parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_created", this.m_created_time );
                // Execute the command
                this.m_sql_command.ExecuteNonQuery();
            }
            catch (Exception l_error)
            {
                MessageBox.Show( string.Format( "Error: {0}\n", l_error ) );
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
        }

        /*DONE*/
        /*
        private bool Check_Against_All( string a_testname, string a_professor, string a_course,
                                        string a_semester, int a_year )

        NAME
            Check_Against_All - process that checks information against the database.

        SYNOPSIS
            bool Check_Testname_Dup( string a_testname, string a_professor, string a_course,
                                        string a_semester, int a_year )
                a_testname      --> the name of the test that we will comparing against
                a_professor     --> the name of the professor that we are looking for
                a_course        --> the name of the course that we are looking for
                a_semester      --> the semester that we are looking for
                a_year          --> the year that we are looking for
        
        DESCRIPTION
            The purpose of this function is to check the other data that a user has put into the
            beginning of the user form.  This function is only called if Check_Testname_Dup returns
            true.  If there is no duplicate in the test information then the function returns false.
            However, if the test information is the same as another test then the function will 
            return true.
            
        RETURNS
            This function returns true or false

        AUTHOR
            Svetlana Marhefka
        
        DATE
            6:27pm 3/29/2017
        */
        /**/
        private bool Check_Against_All( string a_testname, string a_professor,
                                        string a_course, string a_semester, int a_year )
        {
            // Set the local count to 0
            int count = 0;
            // Set the duplicate bool to false
            bool duplicate;
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();

            try
            {
                // Create the sql query that will look up
                this.m_sql_command = new MySqlCommand
                ( "SELECT * FROM test_info " + "WHERE ﻿UserName = @input_username AND " +
                  "TestName = @input_testname AND " +
                  "Professor = @input_professor AND " + "Course = @input_course AND  " +
                  "Semester = @input_semester AND Year = @input_year", this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testname parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_testname", a_testname.ToString() );
                // Add the professor parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_professor", a_professor.ToString() );
                // Add the course parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_course", a_course.ToString() );
                // Add the semester parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_semester", a_semester.ToString() );
                // Add the year parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_year", a_year );

                this.m_sql_adapt = new MySqlDataAdapter( this.m_sql_command );
                this.m_data_table = new DataTable();
                this.m_sql_adapt.Fill( this.m_data_table );
                count = this.m_data_table.Rows.Count;
            }
            catch (Exception error)
            {
                MessageBox.Show( "Error: " + error + "\n" );
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            // If count is > 0 then we can assume that the
            // the two test names are the same
            if (count > 0)
            {
                // Set duplicate to true
                duplicate = true;
            }
            else
            {
                // Otherwise set duplicate to false
                duplicate = false;
            }

            return duplicate;
        }

        /*DONE*/
        /*
        private bool Check_Testname_Dup( string a_testname )

        NAME
            Check_Testname_Dup - process that checks information against the database.

        SYNOPSIS
            bool Check_Testname_Dup( string a_testname )
                a_testname       --> the name of the test that we will comparing against
         
        DESCRIPTION
            The purpose of this function is to check to see if the test name that the user filled
            in at the beginning of the form is a match to any other test names that they have
            created.  If the testname has been used before then the function will true.  Otherwise
            the function will return false.
            
        RETURNS
            This function returns true or false

        AUTHOR
            Svetlana Marhefka
        
        DATE
            6:27pm 3/29/2017
        */
        /**/
        private bool Check_Testname_Dup( string a_testname )
        {
            // Set the local count to 0
            int count = 0;
            // Set the duplicate bool to false
            bool duplicate;
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();

            try
            {
                // Create the sql query that will look up
                this.m_sql_command = new MySqlCommand
                ( "SELECT * FROM test_info WHERE ﻿" + "UserName = @find_username AND " +
                  "TestName = @find_testname", this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@find_username", this.m_user_name.ToString() );
                // Add the testname parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@find_testname", a_testname.ToString() );

                this.m_sql_adapt = new MySqlDataAdapter( this.m_sql_command );
                this.m_data_table = new DataTable();
                this.m_sql_adapt.Fill( this.m_data_table );
                count = this.m_data_table.Rows.Count;
            }
            catch (Exception error)
            {
                MessageBox.Show( "Error: " + error + "\n" );
            }

            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            // If count is > 0 then we can assume that the
            // the two test names are the same
            if (count > 0)
            {
                // Set duplicate to true
                duplicate = true;
            }
            else
            {
                // Otherwise set duplicate to false
                duplicate = false;
            }

            return duplicate;
        }

        /*DONE*/
        /*
        private void Insert_Question( int a_questionId )

        NAME
            Insert_Question - process that inserts information to the database.

        SYNOPSIS
            bool Insert_Question( int a_questionId )
                a_questionId        --> The question id that will be used to find the correct 
                                        question textbox and option panel
         
        DESCRIPTION
            The purpose of this function is to insert or update the question information into the 
            question table located in the database.  The first thing that this function does is get
            the text from the textbox associated with the correct question count.  
            The next thing that this function does is retrieve the text for the correct answer.
            
        RETURNS
            This function returns true or false

        AUTHOR
            Svetlana Marhefka
        
        DATE
            6:27pm 3/29/2017
        */
        /**/
        private void Insert_Form_Question( int a_questionId )
        {
            int l_rows_affected = 0;
            // Get the question text corresponding to the questionId
            string l_question = this.TLayPan.Controls["QTxtBox" + a_questionId.ToString()]
            .Text.ToString();
            // Get the correct answer in the form of a string
            string l_canswer = Correct_Answer( a_questionId );
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            // Create the update string
            const string l_query = "UPDATE question_table\n" +
                                   "SET UserName = @input_username, " +
                                   "TestId = @input_testId, QuestionId = @input_questid, " +
                                   "QuestionText = @input_questiontxt, " +
                                   "RightAnswer = @input_cor_answer\n" +
                                   "WHERE TestId = @input_testId AND QuestionId = @input_questid";
            try
            {
                // Create the sql query that will look up
                this.m_sql_command = new MySqlCommand( l_query, this.m_db_conn );
                // Add the username parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_username", this.m_user_name.ToString() );
                // Add the testid parameter
                this.m_sql_command.Parameters.AddWithValue( "@input_testId", g_test_id );
                // Add the questionid parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_questid", a_questionId );
                // Add the question text parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_questiontxt", l_question );
                // Add the correct answer parameter
                this.m_sql_command.Parameters.AddWithValue
                ( "@input_cor_answer", l_canswer.ToString() );
                // Gives use the number of rows affected
                l_rows_affected = this.m_sql_command.ExecuteNonQuery();
            }
            catch (Exception l_error)
            {
                MessageBox.Show( "Error: " + l_error + "\n" );
            }
            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();

            // If there were no rows by the previous query then it means that
            // the question or that number of questions has never been saved before
            if (l_rows_affected == 0)
            {
                // Open the SQL connection
                this.m_db_conn = DBConnect.OpenConnection();
                // Create the insert string
                const string l_insert_query =
                "INSERT INTO question_table " +
                "(UserName, TestId, QuestionId, QuestionText, RightAnswer)\n" +
                "VALUES (@input_username, @input_testId, @input_questid, " +
                "@input_questiontxt, @input_cor_answer)";
                try
                {
                    // Create the sql query that will look up
                    this.m_sql_command = new MySqlCommand
                    ( l_insert_query, this.m_db_conn );
                    // Add the username parameter
                    this.m_sql_command.Parameters.AddWithValue
                    ( "@input_username", this.m_user_name.ToString() );
                    // Add the testid parameter
                    this.m_sql_command.Parameters.AddWithValue
                    ( "@input_testId", g_test_id );
                    // Add the questionid parameter
                    this.m_sql_command.Parameters.AddWithValue
                    ( "@input_questid", a_questionId );
                    // Add the question text parameter
                    this.m_sql_command.Parameters.AddWithValue
                    ( "@input_questiontxt", l_question );
                    // Add the correct answer parameter
                    this.m_sql_command.Parameters.AddWithValue
                    ( "@input_cor_answer", l_canswer.ToString() );
                    // Gives use the number of rows affected
                    l_rows_affected = this.m_sql_command.ExecuteNonQuery();
                }
                catch (Exception l_error)
                {
                    MessageBox.Show( "Error: " + l_error + "\n" );
                }
                // Once we are done close the connection
                this.m_db_conn = DBConnect.CloseConnection();
                // Reset all of the sql variables
                Reset_SQL();

                Console.WriteLine( "Rows affected: " + l_rows_affected );
            }
        }

        private void Insert_Form_Option( int a_questionId )
        {
            // this is used to count how many rows have been affected
            // Open the SQL connection
            this.m_db_conn = DBConnect.OpenConnection();
            const string l_query = "REPLACE INTO answer_table " +
                                   "(UserName, TestId, QuestionId, AnswerId, AnswerValue)\n" +
                                   "VALUES (@input_username, @input_testId, @input_questid, " +
                                   "@input_answerid, @input_anstxt)";
            int l_answer_id = 1;

            try
            {
                using ( this.m_sql_command = new MySqlCommand( l_query, this.m_db_conn ) )
                {
                    foreach ( TextBox txtbox in this.TLayPan
                    .Controls["oPan" + a_questionId.ToString()].Controls
                    .OfType<TextBox>() )
                    {
                        if (txtbox.Text != "")
                        {
                            // Add the username parameter
                            this.m_sql_command.Parameters.AddWithValue
                            ( "@input_username", this.m_user_name.ToString() );
                            // Add the testid parameter
                            this.m_sql_command.Parameters.AddWithValue
                            ( "@input_testId", g_test_id );
                            // Add the questionid parameter
                            this.m_sql_command.Parameters.AddWithValue
                            ( "@input_questid", a_questionId );
                            // Add the answerid parameter
                            this.m_sql_command.Parameters.AddWithValue
                            ( "@input_answerid", l_answer_id );
                            // Add the answertxt parameter
                            this.m_sql_command.Parameters.AddWithValue
                            ( "@input_anstxt", txtbox.Text.ToString() );
                            int l_rows_affected = this.m_sql_command.ExecuteNonQuery();
                            // If there were no rows by the previous query then it means that
                            // the question or that number of questions has never been saved before
                            if (l_rows_affected == 0)
                            {
                                this.m_sql_command = null;
                                // Create the insert string
                                const string l_insert_query =
                                "INSERT INTO answer_table " +
                                "(UserName, TestId, QuestionId, AnswerId, AnswerValue)\n" +
                                "VALUES (@input_username, @input_testId, @input_questid, " +
                                "@input_answerid, @input_anstxt)";
                                this.m_sql_command = new MySqlCommand
                                ( l_insert_query, this.m_db_conn );
                                // Add the username parameter
                                this.m_sql_command.Parameters.AddWithValue
                                ( "@input_username", this.m_user_name.ToString() );
                                // Add the testid parameter
                                this.m_sql_command.Parameters.AddWithValue
                                ( "@input_testId", g_test_id );
                                // Add the questionid parameter
                                this.m_sql_command.Parameters.AddWithValue
                                ( "@input_questid", a_questionId );
                                // Add the answerid parameter
                                this.m_sql_command.Parameters.AddWithValue
                                ( "@input_answerid", l_answer_id );
                                // Add the answertxt parameter
                                this.m_sql_command.Parameters.AddWithValue
                                ( "@input_anstxt", txtbox.Text.ToString() );
                                l_rows_affected = this.m_sql_command.ExecuteNonQuery();
                                Console.WriteLine( "Rows affected: " + l_rows_affected );
                            }
                            this.m_sql_command.Parameters.Clear();
                        }
                        // Increment the answer id count by 1
                        l_answer_id++;
                    }
                }
            }
            catch (Exception l_error)
            {
                MessageBox.Show( "Error: " + l_error + "\n" );
            }
            // Once we are done close the connection
            this.m_db_conn = DBConnect.CloseConnection();
            // Reset all of the sql variables
            Reset_SQL();
        }

        /*DONE*/
        /*
        private string Correct_Answer( int a_questionId )

        NAME
            Correct_Answer - process that gathers information.

        SYNOPSIS
            string Correct_Answer( int a_questionId )
                a_questionId        --> the object that is sending the click request.

        DESCRIPTION
            The purpose of this function is to actually retrieve the correct answer. This is
            accomplished by going through each control in "oPan(a_questionId)" and finding the
            radio button that has been selected. The next step is to get to the next control
            which should be the textbox and finding the text value of that.  It is the text
            value that is associated with the selected radio button that is returned in the
            form of a string.
            
        RETURNS
            This function returns a string

        AUTHOR
            Svetlana Marhefka

        DATE
            6:27pm 3/29/2017
        */
        /**/
        private string Correct_Answer( int a_questionId )
        {
            // Initialize a local variable to get the answer
            string l_answer = "";
            foreach ( Control l_control in this.TLayPan
            .Controls["oPan" + a_questionId.ToString()].Controls )
            {
                // Go through the panel that has the same question id
                // for example question id 1 = panel with id of 1
                if (l_control is RadioButton && ((RadioButton) l_control).Checked)
                {
                    Console.WriteLine( l_control.Name );
                    // Set the next control
                    Control l_next_control =
                    this.TLayPan.Controls["oPan" + a_questionId.ToString()].GetNextControl
                    ( l_control, true );
                    // Check to make sure it is the right thing
                    if (l_next_control != null)
                    {
                        Console.WriteLine( l_next_control.Name );
                        l_answer = l_next_control.Text.ToString();
                    }
                }
            }
            return l_answer;
        }

        /*DONE*/
        /*
        private void create_sande_Click(object sender, EventArgs e)

        NAME
            create_sande_Click - process that closes the application based on a click.

        SYNOPSIS
            void create_sande_Click(object sender, EventArgs e)
                sender         --> the object that is sending the click request.
                e              --> the arguement being passed in.

        DESCRIPTION
            The purpose of this function is to save the current user form and go back to the 
            previous menu.  This function calls Save_User_Form and then switches screens.  The
            the application will back-track to user tests.

        RETURNS
            This function does not return anything

        AUTHOR
            Svetlana Marhefka

        DATE
            6:27pm 3/29/2017

        */
        /**/
        private void create_sande_Click( object sender, EventArgs e )
        {
            Save_User_Form();
            // Hide the current form
            Hide();
            // Create a new user_test form
            UserTests user_test = new UserTests();
            // Show the user test form
            user_test.Show();
        }

        private void Prev_Menu_Click( object sender, EventArgs e )
        {
            // Reset the global question count
            g_quest_count = 1;
            DialogResult result = MessageBox.Show
            ( "Data on the current form will not be saved. Would you like to continue?",
            "Warning", MessageBoxButtons.OKCancel );
            if (result == DialogResult.OK)
            {
                // Hide the current form
                Hide();
                // Create a new user_test form
                UserTests user_test = new UserTests();
                // Show the user test form
                user_test.Show();
            }
        }

        /*DONE*/
        /*
        private void CreateTest_FormClosed(object sender, FormClosedEventArgs e)

        NAME
            CreateTest_FormClosed - process that closes the application based on a click.

        SYNOPSIS
            void CreateTest_FormClosed(object sender, EventArgs e)
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
        private void CreateTest_FormClosed( object sender, FormClosedEventArgs e )
        {
            Application.Exit();
        }
    }
}