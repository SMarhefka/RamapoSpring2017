namespace RandomTestGenerator
{
    partial class GenTest
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
            this.Options = new System.Windows.Forms.Panel();
            this.Gen_Button = new System.Windows.Forms.Button();
            this.versions = new System.Windows.Forms.NumericUpDown();
            this.questions = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GenPanel = new System.Windows.Forms.Panel();
            this.SaveFile = new System.Windows.Forms.SaveFileDialog();
            this.Gen_Table = new System.Windows.Forms.TableLayoutPanel();
            this.Send_Word = new System.Windows.Forms.Button();
            this.Options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.versions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questions)).BeginInit();
            this.GenPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Options
            // 
            this.Options.Controls.Add(this.Send_Word);
            this.Options.Controls.Add(this.Gen_Button);
            this.Options.Controls.Add(this.versions);
            this.Options.Controls.Add(this.questions);
            this.Options.Controls.Add(this.label2);
            this.Options.Controls.Add(this.label1);
            this.Options.Dock = System.Windows.Forms.DockStyle.Top;
            this.Options.Location = new System.Drawing.Point(25, 0);
            this.Options.Name = "Options";
            this.Options.Padding = new System.Windows.Forms.Padding(5);
            this.Options.Size = new System.Drawing.Size(802, 60);
            this.Options.TabIndex = 0;
            // 
            // Gen_Button
            // 
            this.Gen_Button.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gen_Button.Location = new System.Drawing.Point(512, 13);
            this.Gen_Button.Name = "Gen_Button";
            this.Gen_Button.Size = new System.Drawing.Size(117, 35);
            this.Gen_Button.TabIndex = 4;
            this.Gen_Button.Text = "Generate";
            this.Gen_Button.UseVisualStyleBackColor = true;
            this.Gen_Button.Click += new System.EventHandler(this.Gen_Button_Click);
            // 
            // versions
            // 
            this.versions.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versions.Location = new System.Drawing.Point(387, 18);
            this.versions.Margin = new System.Windows.Forms.Padding(5);
            this.versions.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.versions.Name = "versions";
            this.versions.Size = new System.Drawing.Size(97, 26);
            this.versions.TabIndex = 3;
            // 
            // questions
            // 
            this.questions.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questions.Location = new System.Drawing.Point(154, 18);
            this.questions.Name = "questions";
            this.questions.Size = new System.Drawing.Size(86, 26);
            this.questions.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(263, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "# of Versions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "# of Questions:";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(308, 190);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(8, 8);
            this.panel1.TabIndex = 1;
            // 
            // GenPanel
            // 
            this.GenPanel.AutoScroll = true;
            this.GenPanel.AutoSize = true;
            this.GenPanel.BackColor = System.Drawing.Color.White;
            this.GenPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GenPanel.Controls.Add(this.Gen_Table);
            this.GenPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GenPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GenPanel.Location = new System.Drawing.Point(25, 60);
            this.GenPanel.Margin = new System.Windows.Forms.Padding(0);
            this.GenPanel.Name = "GenPanel";
            this.GenPanel.Padding = new System.Windows.Forms.Padding(10);
            this.GenPanel.Size = new System.Drawing.Size(802, 793);
            this.GenPanel.TabIndex = 2;
            // 
            // Gen_Table
            // 
            this.Gen_Table.AutoScroll = true;
            this.Gen_Table.AutoSize = true;
            this.Gen_Table.ColumnCount = 1;
            this.Gen_Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Gen_Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gen_Table.Location = new System.Drawing.Point(10, 10);
            this.Gen_Table.Margin = new System.Windows.Forms.Padding(10);
            this.Gen_Table.Name = "Gen_Table";
            this.Gen_Table.RowCount = 1;
            this.Gen_Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Gen_Table.Size = new System.Drawing.Size(780, 771);
            this.Gen_Table.TabIndex = 0;
            // 
            // Send_Word
            // 
            this.Send_Word.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Send_Word.Location = new System.Drawing.Point(649, 13);
            this.Send_Word.Name = "Send_Word";
            this.Send_Word.Size = new System.Drawing.Size(142, 35);
            this.Send_Word.TabIndex = 5;
            this.Send_Word.Text = "Send to Word";
            this.Send_Word.UseVisualStyleBackColor = true;
            this.Send_Word.Click += new System.EventHandler(this.Send_Word_Click);
            // 
            // GenTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(852, 853);
            this.Controls.Add(this.GenPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Options);
            this.MaximumSize = new System.Drawing.Size(880, 900);
            this.Name = "GenTest";
            this.Padding = new System.Windows.Forms.Padding(25, 0, 25, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generated Test";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GenTest_FormClosed);
            this.Options.ResumeLayout(false);
            this.Options.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.versions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questions)).EndInit();
            this.GenPanel.ResumeLayout(false);
            this.GenPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Options;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel GenPanel;
        private System.Windows.Forms.SaveFileDialog SaveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown versions;
        private System.Windows.Forms.NumericUpDown questions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Gen_Button;
        private System.Windows.Forms.TableLayoutPanel Gen_Table;
        private System.Windows.Forms.Button Send_Word;
    }
}