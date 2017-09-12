namespace RandomTestGenerator
{
    partial class PasswordReset
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
            this.r_updatePassword = new System.Windows.Forms.Button();
            this.label_username = new System.Windows.Forms.Label();
            this.label_pass = new System.Windows.Forms.Label();
            this.reset_username = new System.Windows.Forms.TextBox();
            this.reset_pass = new System.Windows.Forms.TextBox();
            this.reset_desc = new System.Windows.Forms.RichTextBox();
            this.l_cancel = new System.Windows.Forms.Button();
            this.reset_conn = new System.Windows.Forms.TextBox();
            this.label_conn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // r_updatePassword
            // 
            this.r_updatePassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.r_updatePassword.AutoSize = true;
            this.r_updatePassword.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.r_updatePassword.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.r_updatePassword.Location = new System.Drawing.Point(27, 242);
            this.r_updatePassword.Margin = new System.Windows.Forms.Padding(5, 15, 15, 15);
            this.r_updatePassword.Name = "r_updatePassword";
            this.r_updatePassword.Size = new System.Drawing.Size(148, 52);
            this.r_updatePassword.TabIndex = 7;
            this.r_updatePassword.Text = "Login";
            this.r_updatePassword.UseVisualStyleBackColor = false;
            this.r_updatePassword.Click += new System.EventHandler(this.r_updatePassword_Click);
            // 
            // label_username
            // 
            this.label_username.AutoEllipsis = true;
            this.label_username.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_username.Location = new System.Drawing.Point(25, 86);
            this.label_username.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.label_username.Name = "label_username";
            this.label_username.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_username.Size = new System.Drawing.Size(150, 20);
            this.label_username.TabIndex = 1;
            this.label_username.Text = "Username:";
            this.label_username.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_pass
            // 
            this.label_pass.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pass.Location = new System.Drawing.Point(25, 138);
            this.label_pass.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.label_pass.Name = "label_pass";
            this.label_pass.Size = new System.Drawing.Size(150, 20);
            this.label_pass.TabIndex = 3;
            this.label_pass.Text = "New Password:";
            this.label_pass.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // reset_username
            // 
            this.reset_username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reset_username.Location = new System.Drawing.Point(190, 86);
            this.reset_username.Margin = new System.Windows.Forms.Padding(10, 15, 5, 15);
            this.reset_username.Name = "reset_username";
            this.reset_username.ReadOnly = true;
            this.reset_username.Size = new System.Drawing.Size(441, 22);
            this.reset_username.TabIndex = 2;
            // 
            // reset_pass
            // 
            this.reset_pass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reset_pass.Location = new System.Drawing.Point(190, 138);
            this.reset_pass.Margin = new System.Windows.Forms.Padding(10, 15, 5, 15);
            this.reset_pass.Name = "reset_pass";
            this.reset_pass.PasswordChar = '*';
            this.reset_pass.Size = new System.Drawing.Size(441, 22);
            this.reset_pass.TabIndex = 4;
            // 
            // reset_desc
            // 
            this.reset_desc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reset_desc.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.reset_desc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reset_desc.Font = new System.Drawing.Font("Modern No. 20", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reset_desc.ForeColor = System.Drawing.Color.Maroon;
            this.reset_desc.Location = new System.Drawing.Point(15, 15);
            this.reset_desc.Margin = new System.Windows.Forms.Padding(5, 5, 5, 15);
            this.reset_desc.Multiline = false;
            this.reset_desc.Name = "reset_desc";
            this.reset_desc.ReadOnly = true;
            this.reset_desc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.reset_desc.Size = new System.Drawing.Size(616, 41);
            this.reset_desc.TabIndex = 1;
            this.reset_desc.Text = "Please create a password that you will remember";
            // 
            // l_cancel
            // 
            this.l_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.l_cancel.AutoSize = true;
            this.l_cancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.l_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.l_cancel.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_cancel.Location = new System.Drawing.Point(483, 242);
            this.l_cancel.Margin = new System.Windows.Forms.Padding(15, 15, 5, 5);
            this.l_cancel.Name = "l_cancel";
            this.l_cancel.Size = new System.Drawing.Size(148, 52);
            this.l_cancel.TabIndex = 8;
            this.l_cancel.Text = "Cancel";
            this.l_cancel.UseVisualStyleBackColor = false;
            this.l_cancel.Click += new System.EventHandler(this.l_cancel_Click);
            // 
            // reset_conn
            // 
            this.reset_conn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reset_conn.Location = new System.Drawing.Point(190, 190);
            this.reset_conn.Margin = new System.Windows.Forms.Padding(10, 15, 5, 15);
            this.reset_conn.Name = "reset_conn";
            this.reset_conn.PasswordChar = '*';
            this.reset_conn.Size = new System.Drawing.Size(441, 22);
            this.reset_conn.TabIndex = 6;
            // 
            // label_conn
            // 
            this.label_conn.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_conn.Location = new System.Drawing.Point(15, 190);
            this.label_conn.Margin = new System.Windows.Forms.Padding(5, 10, 5, 15);
            this.label_conn.Name = "label_conn";
            this.label_conn.Size = new System.Drawing.Size(160, 20);
            this.label_conn.TabIndex = 5;
            this.label_conn.Text = "Confirm Password:";
            this.label_conn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PasswordReset
            // 
            this.AcceptButton = this.r_updatePassword;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.l_cancel;
            this.ClientSize = new System.Drawing.Size(646, 353);
            this.Controls.Add(this.reset_conn);
            this.Controls.Add(this.label_conn);
            this.Controls.Add(this.l_cancel);
            this.Controls.Add(this.reset_desc);
            this.Controls.Add(this.reset_pass);
            this.Controls.Add(this.reset_username);
            this.Controls.Add(this.label_pass);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.r_updatePassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(664, 411);
            this.MinimumSize = new System.Drawing.Size(605, 400);
            this.Name = "PasswordReset";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reset Password";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PasswordReset_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button r_updatePassword;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.Label label_pass;
        private System.Windows.Forms.TextBox reset_username;
        private System.Windows.Forms.TextBox reset_pass;
        private System.Windows.Forms.RichTextBox reset_desc;
        private System.Windows.Forms.Button l_cancel;
        private System.Windows.Forms.TextBox reset_conn;
        private System.Windows.Forms.Label label_conn;
    }
}