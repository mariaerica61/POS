﻿namespace POS_and_Inventory_System
{
    partial class frmSoldItems
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSoldItems));
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dt1 = new System.Windows.Forms.DateTimePicker();
            this.dt2 = new System.Windows.Forms.DateTimePicker();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cboCashier = new System.Windows.Forms.ComboBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCancel = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(155)))), ((int)(((byte)(53)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Rasputin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1010, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 35);
            this.button2.TabIndex = 11;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView6
            // 
            this.dataGridView6.AllowUserToAddRows = false;
            this.dataGridView6.AllowUserToResizeColumns = false;
            this.dataGridView6.AllowUserToResizeRows = false;
            this.dataGridView6.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView6.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(68)))), ((int)(((byte)(66)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Agrandir Grand Heavy", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(132)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView6.ColumnHeadersHeight = 30;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView6.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column10,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.colCancel});
            this.dataGridView6.Cursor = System.Windows.Forms.Cursors.No;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Palatino Linotype", 14.25F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(68)))), ((int)(((byte)(66)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView6.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView6.EnableHeadersVisualStyles = false;
            this.dataGridView6.Location = new System.Drawing.Point(0, 151);
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.RowHeadersVisible = false;
            this.dataGridView6.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(238)))), ((int)(((byte)(236)))));
            this.dataGridView6.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(68)))), ((int)(((byte)(66)))));
            this.dataGridView6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView6.Size = new System.Drawing.Size(1077, 581);
            this.dataGridView6.TabIndex = 12;
            this.dataGridView6.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView6_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 11F);
            this.label2.Location = new System.Drawing.Point(26, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Filter By Date (From - To)";
            // 
            // dt1
            // 
            this.dt1.CalendarFont = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.dt1.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(68)))), ((int)(((byte)(66)))));
            this.dt1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(132)))), ((int)(((byte)(130)))));
            this.dt1.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(68)))), ((int)(((byte)(66)))));
            this.dt1.Font = new System.Drawing.Font("Now", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt1.Location = new System.Drawing.Point(212, 30);
            this.dt1.Name = "dt1";
            this.dt1.Size = new System.Drawing.Size(120, 29);
            this.dt1.TabIndex = 14;
            this.dt1.ValueChanged += new System.EventHandler(this.dt1_ValueChanged_1);
            // 
            // dt2
            // 
            this.dt2.CalendarFont = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.dt2.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(68)))), ((int)(((byte)(66)))));
            this.dt2.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(132)))), ((int)(((byte)(130)))));
            this.dt2.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(68)))), ((int)(((byte)(66)))));
            this.dt2.Font = new System.Drawing.Font("Now", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt2.Location = new System.Drawing.Point(338, 30);
            this.dt2.Name = "dt2";
            this.dt2.Size = new System.Drawing.Size(120, 29);
            this.dt2.TabIndex = 17;
            this.dt2.ValueChanged += new System.EventHandler(this.dt2_ValueChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(68)))), ((int)(((byte)(66)))));
            this.lblTotal.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(698, 72);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(347, 53);
            this.lblTotal.TabIndex = 18;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(155)))), ((int)(((byte)(53)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Rasputin", 9.749998F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(98, 71);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(78, 30);
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cboCashier
            // 
            this.cboCashier.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.cboCashier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(68)))), ((int)(((byte)(66)))));
            this.cboCashier.FormattingEnabled = true;
            this.cboCashier.Location = new System.Drawing.Point(212, 72);
            this.cboCashier.Name = "cboCashier";
            this.cboCashier.Size = new System.Drawing.Size(247, 30);
            this.cboCashier.TabIndex = 20;
            this.cboCashier.SelectedIndexChanged += new System.EventHandler(this.cboCashier_SelectedIndexChanged);
            this.cboCashier.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboCashier_KeyPress);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "#";
            this.Column1.Name = "Column1";
            this.Column1.Width = 47;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ID";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "INVOICE #";
            this.Column4.Name = "Column4";
            this.Column4.Width = 143;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.HeaderText = "PCODE";
            this.Column5.Name = "Column5";
            this.Column5.Width = 112;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column10.HeaderText = "DESCRIPTION";
            this.Column10.Name = "Column10";
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column6.HeaderText = "PRICE";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column7.HeaderText = "QTY";
            this.Column7.Name = "Column7";
            this.Column7.Width = 79;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column8.HeaderText = "DISC.";
            this.Column8.Name = "Column8";
            this.Column8.Width = 91;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column9.HeaderText = "TOTAL";
            this.Column9.Name = "Column9";
            this.Column9.Width = 103;
            // 
            // colCancel
            // 
            this.colCancel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCancel.HeaderText = "";
            this.colCancel.Image = ((System.Drawing.Image)(resources.GetObject("colCancel.Image")));
            this.colCancel.Name = "colCancel";
            this.colCancel.Width = 5;
            // 
            // frmSoldItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1078, 732);
            this.ControlBox = false;
            this.Controls.Add(this.cboCashier);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dt2);
            this.Controls.Add(this.dt1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView6);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSoldItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSoldItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotal;
        public System.Windows.Forms.Button btnPrint;
        public System.Windows.Forms.DateTimePicker dt1;
        public System.Windows.Forms.DateTimePicker dt2;
        public System.Windows.Forms.DataGridView dataGridView6;
        public System.Windows.Forms.ComboBox cboCashier;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewImageColumn colCancel;
    }
}