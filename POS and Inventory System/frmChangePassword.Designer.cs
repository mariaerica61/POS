namespace POS_and_Inventory_System
{
    partial class frmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOld = new MetroFramework.Controls.MetroTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNew = new MetroFramework.Controls.MetroTextBox();
            this.txtConfirm = new MetroFramework.Controls.MetroTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(48)))), ((int)(((byte)(160)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Rasputin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(565, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 35);
            this.button2.TabIndex = 9;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(142)))), ((int)(((byte)(219)))));
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 58);
            this.panel1.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rasputin", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "CHANGE PASSWORD";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Now", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(983, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(201, 28);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "System Administrator";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(931, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 33);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Rasputin", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(48)))), ((int)(((byte)(160)))));
            this.label8.Location = new System.Drawing.Point(31, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(196, 24);
            this.label8.TabIndex = 33;
            this.label8.Text = "NEW PASSWORD";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Rasputin", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(48)))), ((int)(((byte)(160)))));
            this.label7.Location = new System.Drawing.Point(31, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(185, 24);
            this.label7.TabIndex = 32;
            this.label7.Text = "OLD PASSWORD";
            // 
            // txtOld
            // 
            // 
            // 
            // 
            this.txtOld.CustomButton.Font = new System.Drawing.Font("Now", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOld.CustomButton.Image = null;
            this.txtOld.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.txtOld.CustomButton.Name = "";
            this.txtOld.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtOld.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtOld.CustomButton.TabIndex = 1;
            this.txtOld.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOld.CustomButton.UseSelectable = true;
            this.txtOld.CustomButton.Visible = false;
            this.txtOld.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(48)))), ((int)(((byte)(160)))));
            this.txtOld.Lines = new string[0];
            this.txtOld.Location = new System.Drawing.Point(280, 92);
            this.txtOld.MaxLength = 32767;
            this.txtOld.Name = "txtOld";
            this.txtOld.PasswordChar = '\0';
            this.txtOld.PromptText = "Enter something";
            this.txtOld.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOld.SelectedText = "";
            this.txtOld.SelectionLength = 0;
            this.txtOld.SelectionStart = 0;
            this.txtOld.ShortcutsEnabled = true;
            this.txtOld.Size = new System.Drawing.Size(300, 29);
            this.txtOld.TabIndex = 31;
            this.txtOld.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOld.UseSelectable = true;
            this.txtOld.WaterMark = "Enter something";
            this.txtOld.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtOld.WaterMarkFont = new System.Drawing.Font("Now", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Rasputin", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(48)))), ((int)(((byte)(160)))));
            this.label2.Location = new System.Drawing.Point(31, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 24);
            this.label2.TabIndex = 35;
            this.label2.Text = "RE-TYPE PASSWORD";
            // 
            // txtNew
            // 
            // 
            // 
            // 
            this.txtNew.CustomButton.Font = new System.Drawing.Font("Now", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew.CustomButton.Image = null;
            this.txtNew.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.txtNew.CustomButton.Name = "";
            this.txtNew.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtNew.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNew.CustomButton.TabIndex = 1;
            this.txtNew.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNew.CustomButton.UseSelectable = true;
            this.txtNew.CustomButton.Visible = false;
            this.txtNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(48)))), ((int)(((byte)(160)))));
            this.txtNew.Lines = new string[0];
            this.txtNew.Location = new System.Drawing.Point(280, 144);
            this.txtNew.MaxLength = 32767;
            this.txtNew.Name = "txtNew";
            this.txtNew.PasswordChar = '•';
            this.txtNew.PromptText = "Enter something";
            this.txtNew.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNew.SelectedText = "";
            this.txtNew.SelectionLength = 0;
            this.txtNew.SelectionStart = 0;
            this.txtNew.ShortcutsEnabled = true;
            this.txtNew.Size = new System.Drawing.Size(300, 29);
            this.txtNew.TabIndex = 43;
            this.txtNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNew.UseSelectable = true;
            this.txtNew.WaterMark = "Enter something";
            this.txtNew.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNew.WaterMarkFont = new System.Drawing.Font("Now", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtConfirm
            // 
            // 
            // 
            // 
            this.txtConfirm.CustomButton.Font = new System.Drawing.Font("Now", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirm.CustomButton.Image = null;
            this.txtConfirm.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.txtConfirm.CustomButton.Name = "";
            this.txtConfirm.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtConfirm.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtConfirm.CustomButton.TabIndex = 1;
            this.txtConfirm.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtConfirm.CustomButton.UseSelectable = true;
            this.txtConfirm.CustomButton.Visible = false;
            this.txtConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(48)))), ((int)(((byte)(160)))));
            this.txtConfirm.Lines = new string[0];
            this.txtConfirm.Location = new System.Drawing.Point(280, 197);
            this.txtConfirm.MaxLength = 32767;
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '•';
            this.txtConfirm.PromptText = "Enter something";
            this.txtConfirm.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtConfirm.SelectedText = "";
            this.txtConfirm.SelectionLength = 0;
            this.txtConfirm.SelectionStart = 0;
            this.txtConfirm.ShortcutsEnabled = true;
            this.txtConfirm.Size = new System.Drawing.Size(300, 29);
            this.txtConfirm.TabIndex = 44;
            this.txtConfirm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtConfirm.UseSelectable = true;
            this.txtConfirm.WaterMark = "Enter something";
            this.txtConfirm.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtConfirm.WaterMarkFont = new System.Drawing.Font("Now", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(48)))), ((int)(((byte)(160)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Rasputin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(199, 254);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(222, 44);
            this.btnSave.TabIndex = 48;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 313);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtOld);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private MetroFramework.Controls.MetroTextBox txtOld;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroTextBox txtNew;
        private MetroFramework.Controls.MetroTextBox txtConfirm;
        public System.Windows.Forms.Button btnSave;
    }
}