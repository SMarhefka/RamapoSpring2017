namespace RandomTestGenerator
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"> true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.l_CheckUser = new System.Windows.Forms.Button();
            this.label_username = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.l_username = new System.Windows.Forms.TextBox();
            this.l_password = new System.Windows.Forms.TextBox();
            this.label_des = new System.Windows.Forms.Label();
            this.RText_Description = new System.Windows.Forms.RichTextBox();
            this.l_cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.l_forgot = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // l_CheckUser
            // 
            this.l_CheckUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.l_CheckUser.AutoSize = true;
            this.l_CheckUser.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.l_CheckUser.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_CheckUser.Location = new System.Drawing.Point(31, 3);
            this.l_CheckUser.Name = "l_CheckUser";
            this.l_CheckUser.Size = new System.Drawing.Size(148, 56);
            this.l_CheckUser.TabIndex = 0;
            this.l_CheckUser.Text = "Login";
            this.l_CheckUser.UseVisualStyleBackColor = false;
            this.l_CheckUser.Click += new System.EventHandler(this.l_CheckUser_Click);
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Font = new System.Drawing.Font("Modern No. 20", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_username.Location = new System.Drawing.Point(27, 63);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(99, 21);
            this.label_username.TabIndex = 1;
            this.label_username.Text = "Username:";
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Modern No. 20", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_password.Location = new System.Drawing.Point(35, 119);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(95, 21);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "Password:";
            // 
            // l_username
            // 
            this.l_username.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.l_username.Location = new System.Drawing.Point(145, 64);
            this.l_username.Name = "l_username";
            this.l_username.Size = new System.Drawing.Size(421, 22);
            this.l_username.TabIndex = 2;
            this.l_username.WordWrap = false;
            // 
            // l_password
            // 
            this.l_password.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.l_password.Location = new System.Drawing.Point(145, 118);
            this.l_password.Name = "l_password";
            this.l_password.PasswordChar = '*';
            this.l_password.Size = new System.Drawing.Size(421, 22);
            this.l_password.TabIndex = 4;
            // 
            // label_des
            // 
            this.label_des.AutoSize = true;
            this.label_des.Font = new System.Drawing.Font("Modern No. 20", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_des.Location = new System.Drawing.Point(-4, 157);
            this.label_des.Name = "label_des";
            this.label_des.Size = new System.Drawing.Size(0, 21);
            this.label_des.TabIndex = 5;
            // 
            // RText_Description
            // 
            this.RText_Description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RText_Description.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.RText_Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RText_Description.Font = new System.Drawing.Font("Modern No. 20", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RText_Description.ForeColor = System.Drawing.Color.Maroon;
            this.RText_Description.Location = new System.Drawing.Point(31, 11);
            this.RText_Description.Margin = new System.Windows.Forms.Padding(2);
            this.RText_Description.Multiline = false;
            this.RText_Description.Name = "RText_Description";
            this.RText_Description.ReadOnly = true;
            this.RText_Description.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.RText_Description.Size = new System.Drawing.Size(549, 30);
            this.RText_Description.TabIndex = 0;
            this.RText_Description.Text = "Please login with your given username and password";
            // 
            // l_cancel
            // 
            this.l_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.l_cancel.AutoSize = true;
            this.l_cancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.l_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.l_cancel.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_cancel.Location = new System.Drawing.Point(418, 3);
            this.l_cancel.Name = "l_cancel";
            this.l_cancel.Size = new System.Drawing.Size(148, 56);
            this.l_cancel.TabIndex = 1;
            this.l_cancel.Text = "Cancel";
            this.l_cancel.UseVisualStyleBackColor = false;
            this.l_cancel.Click += new System.EventHandler(this.l_cancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.l_forgot);
            this.panel1.Controls.Add(this.l_CheckUser);
            this.panel1.Controls.Add(this.l_cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 199);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 97);
            this.panel1.TabIndex = 6;
            // 
            // l_forgot
            // 
            this.l_forgot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.l_forgot.AutoSize = true;
            this.l_forgot.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.l_forgot.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_forgot.Location = new System.Drawing.Point(211, 3);
            this.l_forgot.Name = "l_forgot";
            this.l_forgot.Size = new System.Drawing.Size(169, 56);
            this.l_forgot.TabIndex = 2;
            this.l_forgot.Text = "Forgot Password";
            this.l_forgot.UseVisualStyleBackColor = false;
            this.l_forgot.Click += new System.EventHandler(this.l_forgot_Click);
            // 
            // Login
            // 
            this.AcceptButton = this.l_CheckUser;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.l_cancel;
            this.ClientSize = new System.Drawing.Size(633, 296);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RText_Description);
            this.Controls.Add(this.label_des);
            this.Controls.Add(this.l_password);
            this.Controls.Add(this.l_username);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_username);
            this.MaximumSize = new System.Drawing.Size(772, 343);
            this.MinimumSize = new System.Drawing.Size(651, 321);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button l_CheckUser;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox l_username;
        private System.Windows.Forms.TextBox l_password;
        private System.Windows.Forms.Label label_des;
        private System.Windows.Forms.RichTextBox RText_Description;
        private System.Windows.Forms.Button l_cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button l_forgot;
    }
}