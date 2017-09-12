using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomTestGenerator
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        /**/
        /*
        private void St_Login_Click(object sender, EventArgs e)

        NAME
                St_Login_Click - process that creates a new form based on a click.

        SYNOPSIS
                void St_Login_Click(object sender, EventArgs e)
                    sender         --> the object that is sending the click request.
                    e              --> the arguement being passed in.

        DESCRIPTION
                The purpose of this function is to open a new login form.  If the user
                selects "Login" from the initial screen the start screen will be hidden
                and a new login form will be created.

        RETURNS
                This function does not return anything

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private void St_Login_Click(object sender, EventArgs e)
        {
            Login login_form = new Login();
            this.Hide();
            login_form.Show();
        }

        /**/
        /*
        private void St_Register_Click(object sender, EventArgs e)

        NAME
                St_Register_Click - process that creates a new form based on a click.

        SYNOPSIS
                void St_Register_Click(object sender, EventArgs e)
                    sender         --> the object that is sending the click request.
                    e              --> the arguement being passed in.

        DESCRIPTION
                The purpose of this function is to open a new registration form.  If the user
                selects "Register" from the initial screen the start screen will be hidden
                and a new registration form will be created.

        RETURNS
                This function does not return anything

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private void St_Register_Click(object sender, EventArgs e)
        {
            Registration reg_form = new Registration();
            this.Hide();
            reg_form.Show();
        }

        /**/
        /*
        private void St_Exit_Click(object sender, EventArgs e)

        NAME
                St_Exit_Click - process that closes the application based on a click.

        SYNOPSIS
                void St_Exit_Click(object sender, EventArgs e)
                    sender         --> the object that is sending the click request.
                    e              --> the arguement being passed in.

        DESCRIPTION
                The purpose of this function is to exit the application.  If the user
                selects "Exit" from the initial screen the entire application will be closed.

        RETURNS
                This function does not return anything

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        private void St_Exit_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }
    }
}
