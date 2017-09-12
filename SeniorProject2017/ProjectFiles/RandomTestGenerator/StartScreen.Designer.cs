namespace RandomTestGenerator
{
    partial class StartScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
            this.label1 = new System.Windows.Forms.Label();
            this.m_sspicture = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.St_Login = new System.Windows.Forms.Button();
            this.ss_Register = new System.Windows.Forms.Button();
            this.ss_exit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_sspicture)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 0;
            // 
            // m_sspicture
            // 
            this.m_sspicture.Image = ((System.Drawing.Image)(resources.GetObject("m_sspicture.Image")));
            this.m_sspicture.Location = new System.Drawing.Point(12, 19);
            this.m_sspicture.Name = "m_sspicture";
            this.m_sspicture.Size = new System.Drawing.Size(161, 142);
            this.m_sspicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_sspicture.TabIndex = 1;
            this.m_sspicture.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.richTextBox1.Font = new System.Drawing.Font("Modern No. 20", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.Maroon;
            this.richTextBox1.Location = new System.Drawing.Point(186, 19);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(10);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(554, 288);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // St_Login
            // 
            this.St_Login.AutoSize = true;
            this.St_Login.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.St_Login.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.St_Login.Location = new System.Drawing.Point(36, 3);
            this.St_Login.Name = "St_Login";
            this.St_Login.Size = new System.Drawing.Size(174, 73);
            this.St_Login.TabIndex = 3;
            this.St_Login.Text = "Login";
            this.St_Login.UseVisualStyleBackColor = false;
            this.St_Login.Click += new System.EventHandler(this.St_Login_Click);
            // 
            // ss_Register
            // 
            this.ss_Register.AutoSize = true;
            this.ss_Register.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ss_Register.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ss_Register.Location = new System.Drawing.Point(301, 3);
            this.ss_Register.Name = "ss_Register";
            this.ss_Register.Size = new System.Drawing.Size(174, 73);
            this.ss_Register.TabIndex = 4;
            this.ss_Register.Text = "Register";
            this.ss_Register.UseVisualStyleBackColor = false;
            this.ss_Register.Click += new System.EventHandler(this.St_Register_Click);
            // 
            // ss_exit
            // 
            this.ss_exit.AutoSize = true;
            this.ss_exit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ss_exit.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ss_exit.Location = new System.Drawing.Point(566, 3);
            this.ss_exit.Name = "ss_exit";
            this.ss_exit.Size = new System.Drawing.Size(174, 73);
            this.ss_exit.TabIndex = 5;
            this.ss_exit.Text = "Exit";
            this.ss_exit.UseVisualStyleBackColor = false;
            this.ss_exit.Click += new System.EventHandler(this.St_Exit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.St_Login);
            this.panel1.Controls.Add(this.ss_exit);
            this.panel1.Controls.Add(this.ss_Register);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 347);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 108);
            this.panel1.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 158);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(161, 149);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 455);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.m_sspicture);
            this.Controls.Add(this.label1);
            this.Name = "StartScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Random Test Generator";
            ((System.ComponentModel.ISupportInitialize)(this.m_sspicture)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox m_sspicture;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button St_Login;
        private System.Windows.Forms.Button ss_Register;
        private System.Windows.Forms.Button ss_exit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

