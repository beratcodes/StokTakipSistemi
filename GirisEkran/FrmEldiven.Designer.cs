namespace GirisEkran
{
    partial class FrmEldiven
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEldiven));
            this.cmbTür = new System.Windows.Forms.ComboBox();
            this.txtAdet = new System.Windows.Forms.TextBox();
            this.txtFiyat = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.GüncelleBtn = new System.Windows.Forms.PictureBox();
            this.SilBtn = new System.Windows.Forms.PictureBox();
            this.ListeleBtn = new System.Windows.Forms.PictureBox();
            this.EkleBtn = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GeriBtn = new System.Windows.Forms.PictureBox();
            this.CarpiBtn = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbMarka = new System.Windows.Forms.ComboBox();
            this.IscıBeybi = new System.Windows.Forms.PictureBox();
            this.KaynakcıUniversal = new System.Windows.Forms.PictureBox();
            this.AscıEldiveni = new System.Windows.Forms.PictureBox();
            this.ElektirikciBeybi = new System.Windows.Forms.PictureBox();
            this.LblTür = new System.Windows.Forms.Label();
            this.LblMarka = new System.Windows.Forms.Label();
            this.LblFiyat = new System.Windows.Forms.Label();
            this.LblAdet = new System.Windows.Forms.Label();
            this.btnSat = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAlisFiyat = new System.Windows.Forms.TextBox();
            this.SatisTarihi = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.GüncelleBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SilBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListeleBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EkleBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GeriBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarpiBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IscıBeybi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KaynakcıUniversal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AscıEldiveni)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElektirikciBeybi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSat)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbTür
            // 
            this.cmbTür.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTür.FormattingEnabled = true;
            this.cmbTür.Location = new System.Drawing.Point(216, 704);
            this.cmbTür.Name = "cmbTür";
            this.cmbTür.Size = new System.Drawing.Size(191, 32);
            this.cmbTür.TabIndex = 82;
            this.cmbTür.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTür_KeyPress);
            // 
            // txtAdet
            // 
            this.txtAdet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAdet.Location = new System.Drawing.Point(216, 670);
            this.txtAdet.Name = "txtAdet";
            this.txtAdet.Size = new System.Drawing.Size(191, 22);
            this.txtAdet.TabIndex = 76;
            // 
            // txtFiyat
            // 
            this.txtFiyat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFiyat.Location = new System.Drawing.Point(216, 589);
            this.txtFiyat.Name = "txtFiyat";
            this.txtFiyat.Size = new System.Drawing.Size(191, 22);
            this.txtFiyat.TabIndex = 75;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.SystemColors.Info;
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtID.Location = new System.Drawing.Point(216, 545);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(191, 22);
            this.txtID.TabIndex = 74;
            // 
            // GüncelleBtn
            // 
            this.GüncelleBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GüncelleBtn.Image = ((System.Drawing.Image)(resources.GetObject("GüncelleBtn.Image")));
            this.GüncelleBtn.Location = new System.Drawing.Point(856, 633);
            this.GüncelleBtn.Name = "GüncelleBtn";
            this.GüncelleBtn.Size = new System.Drawing.Size(195, 50);
            this.GüncelleBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GüncelleBtn.TabIndex = 73;
            this.GüncelleBtn.TabStop = false;
            this.GüncelleBtn.Click += new System.EventHandler(this.GüncelleBtn_Click);
            // 
            // SilBtn
            // 
            this.SilBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SilBtn.Image = ((System.Drawing.Image)(resources.GetObject("SilBtn.Image")));
            this.SilBtn.Location = new System.Drawing.Point(655, 633);
            this.SilBtn.Name = "SilBtn";
            this.SilBtn.Size = new System.Drawing.Size(195, 50);
            this.SilBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SilBtn.TabIndex = 72;
            this.SilBtn.TabStop = false;
            this.SilBtn.Click += new System.EventHandler(this.SilBtn_Click);
            // 
            // ListeleBtn
            // 
            this.ListeleBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ListeleBtn.Image = ((System.Drawing.Image)(resources.GetObject("ListeleBtn.Image")));
            this.ListeleBtn.Location = new System.Drawing.Point(856, 577);
            this.ListeleBtn.Name = "ListeleBtn";
            this.ListeleBtn.Size = new System.Drawing.Size(195, 50);
            this.ListeleBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ListeleBtn.TabIndex = 71;
            this.ListeleBtn.TabStop = false;
            this.ListeleBtn.Click += new System.EventHandler(this.ListeleBtn_Click);
            // 
            // EkleBtn
            // 
            this.EkleBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EkleBtn.Image = ((System.Drawing.Image)(resources.GetObject("EkleBtn.Image")));
            this.EkleBtn.Location = new System.Drawing.Point(655, 577);
            this.EkleBtn.Name = "EkleBtn";
            this.EkleBtn.Size = new System.Drawing.Size(195, 50);
            this.EkleBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EkleBtn.TabIndex = 70;
            this.EkleBtn.TabStop = false;
            this.EkleBtn.Click += new System.EventHandler(this.EkleBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(160)))), ((int)(((byte)(0)))));
            this.dataGridView1.Location = new System.Drawing.Point(-1, 134);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1052, 319);
            this.dataGridView1.TabIndex = 69;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // GeriBtn
            // 
            this.GeriBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GeriBtn.Image = ((System.Drawing.Image)(resources.GetObject("GeriBtn.Image")));
            this.GeriBtn.Location = new System.Drawing.Point(1552, 0);
            this.GeriBtn.Name = "GeriBtn";
            this.GeriBtn.Size = new System.Drawing.Size(40, 39);
            this.GeriBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GeriBtn.TabIndex = 68;
            this.GeriBtn.TabStop = false;
            this.GeriBtn.Click += new System.EventHandler(this.GeriBtn_Click);
            // 
            // CarpiBtn
            // 
            this.CarpiBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CarpiBtn.Image = ((System.Drawing.Image)(resources.GetObject("CarpiBtn.Image")));
            this.CarpiBtn.Location = new System.Drawing.Point(1589, 0);
            this.CarpiBtn.Name = "CarpiBtn";
            this.CarpiBtn.Size = new System.Drawing.Size(40, 39);
            this.CarpiBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CarpiBtn.TabIndex = 67;
            this.CarpiBtn.TabStop = false;
            this.CarpiBtn.Click += new System.EventHandler(this.CarpiBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(381, 136);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 66;
            this.pictureBox1.TabStop = false;
            // 
            // cmbMarka
            // 
            this.cmbMarka.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarka.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbMarka.FormattingEnabled = true;
            this.cmbMarka.Location = new System.Drawing.Point(216, 748);
            this.cmbMarka.Name = "cmbMarka";
            this.cmbMarka.Size = new System.Drawing.Size(191, 32);
            this.cmbMarka.TabIndex = 84;
            this.cmbMarka.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMarka_KeyPress);
            // 
            // IscıBeybi
            // 
            this.IscıBeybi.Image = ((System.Drawing.Image)(resources.GetObject("IscıBeybi.Image")));
            this.IscıBeybi.Location = new System.Drawing.Point(1261, 115);
            this.IscıBeybi.Name = "IscıBeybi";
            this.IscıBeybi.Size = new System.Drawing.Size(170, 193);
            this.IscıBeybi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IscıBeybi.TabIndex = 87;
            this.IscıBeybi.TabStop = false;
            // 
            // KaynakcıUniversal
            // 
            this.KaynakcıUniversal.Image = ((System.Drawing.Image)(resources.GetObject("KaynakcıUniversal.Image")));
            this.KaynakcıUniversal.Location = new System.Drawing.Point(1248, 115);
            this.KaynakcıUniversal.Name = "KaynakcıUniversal";
            this.KaynakcıUniversal.Size = new System.Drawing.Size(199, 193);
            this.KaynakcıUniversal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.KaynakcıUniversal.TabIndex = 88;
            this.KaynakcıUniversal.TabStop = false;
            // 
            // AscıEldiveni
            // 
            this.AscıEldiveni.Image = ((System.Drawing.Image)(resources.GetObject("AscıEldiveni.Image")));
            this.AscıEldiveni.Location = new System.Drawing.Point(1261, 115);
            this.AscıEldiveni.Name = "AscıEldiveni";
            this.AscıEldiveni.Size = new System.Drawing.Size(195, 193);
            this.AscıEldiveni.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AscıEldiveni.TabIndex = 89;
            this.AscıEldiveni.TabStop = false;
            // 
            // ElektirikciBeybi
            // 
            this.ElektirikciBeybi.Image = ((System.Drawing.Image)(resources.GetObject("ElektirikciBeybi.Image")));
            this.ElektirikciBeybi.Location = new System.Drawing.Point(1252, 115);
            this.ElektirikciBeybi.Name = "ElektirikciBeybi";
            this.ElektirikciBeybi.Size = new System.Drawing.Size(195, 193);
            this.ElektirikciBeybi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ElektirikciBeybi.TabIndex = 90;
            this.ElektirikciBeybi.TabStop = false;
            // 
            // LblTür
            // 
            this.LblTür.AutoSize = true;
            this.LblTür.Location = new System.Drawing.Point(1248, 346);
            this.LblTür.Name = "LblTür";
            this.LblTür.Size = new System.Drawing.Size(0, 24);
            this.LblTür.TabIndex = 91;
            // 
            // LblMarka
            // 
            this.LblMarka.AutoSize = true;
            this.LblMarka.Location = new System.Drawing.Point(1248, 391);
            this.LblMarka.Name = "LblMarka";
            this.LblMarka.Size = new System.Drawing.Size(0, 24);
            this.LblMarka.TabIndex = 92;
            // 
            // LblFiyat
            // 
            this.LblFiyat.AutoSize = true;
            this.LblFiyat.Location = new System.Drawing.Point(1248, 440);
            this.LblFiyat.Name = "LblFiyat";
            this.LblFiyat.Size = new System.Drawing.Size(0, 24);
            this.LblFiyat.TabIndex = 93;
            // 
            // LblAdet
            // 
            this.LblAdet.AutoSize = true;
            this.LblAdet.Location = new System.Drawing.Point(1248, 484);
            this.LblAdet.Name = "LblAdet";
            this.LblAdet.Size = new System.Drawing.Size(0, 24);
            this.LblAdet.TabIndex = 94;
            // 
            // btnSat
            // 
            this.btnSat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSat.Image = ((System.Drawing.Image)(resources.GetObject("btnSat.Image")));
            this.btnSat.Location = new System.Drawing.Point(763, 686);
            this.btnSat.Name = "btnSat";
            this.btnSat.Size = new System.Drawing.Size(195, 50);
            this.btnSat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSat.TabIndex = 96;
            this.btnSat.TabStop = false;
            this.btnSat.Click += new System.EventHandler(this.btnSat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 24);
            this.label1.TabIndex = 97;
            this.label1.Text = "Eldiven ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 589);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 24);
            this.label2.TabIndex = 98;
            this.label2.Text = "Satış Fiyat:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 671);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 24);
            this.label3.TabIndex = 99;
            this.label3.Text = "Adet:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 712);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 24);
            this.label4.TabIndex = 100;
            this.label4.Text = "Türü:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(133, 751);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 24);
            this.label5.TabIndex = 101;
            this.label5.Text = "Marka:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(110, 633);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 24);
            this.label6.TabIndex = 103;
            this.label6.Text = "Alış Fiyat:";
            // 
            // txtAlisFiyat
            // 
            this.txtAlisFiyat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAlisFiyat.Location = new System.Drawing.Point(216, 633);
            this.txtAlisFiyat.Name = "txtAlisFiyat";
            this.txtAlisFiyat.Size = new System.Drawing.Size(191, 22);
            this.txtAlisFiyat.TabIndex = 102;
            // 
            // SatisTarihi
            // 
            this.SatisTarihi.Location = new System.Drawing.Point(122, 492);
            this.SatisTarihi.Name = "SatisTarihi";
            this.SatisTarihi.Size = new System.Drawing.Size(285, 29);
            this.SatisTarihi.TabIndex = 104;
            // 
            // FrmEldiven
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1630, 854);
            this.Controls.Add(this.SatisTarihi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAlisFiyat);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSat);
            this.Controls.Add(this.LblAdet);
            this.Controls.Add(this.LblFiyat);
            this.Controls.Add(this.LblMarka);
            this.Controls.Add(this.LblTür);
            this.Controls.Add(this.ElektirikciBeybi);
            this.Controls.Add(this.AscıEldiveni);
            this.Controls.Add(this.KaynakcıUniversal);
            this.Controls.Add(this.IscıBeybi);
            this.Controls.Add(this.cmbMarka);
            this.Controls.Add(this.cmbTür);
            this.Controls.Add(this.txtAdet);
            this.Controls.Add(this.txtFiyat);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.GüncelleBtn);
            this.Controls.Add(this.SilBtn);
            this.Controls.Add(this.ListeleBtn);
            this.Controls.Add(this.EkleBtn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.GeriBtn);
            this.Controls.Add(this.CarpiBtn);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEldiven";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmEldiven";
            this.Load += new System.EventHandler(this.FrmEldiven_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GüncelleBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SilBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListeleBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EkleBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GeriBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarpiBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IscıBeybi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KaynakcıUniversal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AscıEldiveni)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElektirikciBeybi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTür;
        private System.Windows.Forms.TextBox txtAdet;
        private System.Windows.Forms.TextBox txtFiyat;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.PictureBox GüncelleBtn;
        private System.Windows.Forms.PictureBox SilBtn;
        private System.Windows.Forms.PictureBox ListeleBtn;
        private System.Windows.Forms.PictureBox EkleBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox GeriBtn;
        private System.Windows.Forms.PictureBox CarpiBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbMarka;
        private System.Windows.Forms.PictureBox IscıBeybi;
        private System.Windows.Forms.PictureBox KaynakcıUniversal;
        private System.Windows.Forms.PictureBox AscıEldiveni;
        private System.Windows.Forms.PictureBox ElektirikciBeybi;
        private System.Windows.Forms.Label LblTür;
        private System.Windows.Forms.Label LblMarka;
        private System.Windows.Forms.Label LblFiyat;
        private System.Windows.Forms.Label LblAdet;
        private System.Windows.Forms.PictureBox btnSat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAlisFiyat;
        private System.Windows.Forms.DateTimePicker SatisTarihi;
    }
}