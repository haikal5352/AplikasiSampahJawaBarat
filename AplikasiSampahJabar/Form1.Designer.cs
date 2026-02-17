namespace AplikasiSampahJabar
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnkeluarmenuinput = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.beratsampah = new System.Windows.Forms.NumericUpDown();
            this.cmblokasitps = new System.Windows.Forms.ComboBox();
            this.cmbjenissampah = new System.Windows.Forms.ComboBox();
            this.dateinputsampah = new System.Windows.Forms.DateTimePicker();
            this.richtxtcatatansampah = new System.Windows.Forms.RichTextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtnamasampah = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beratsampah)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(184, 300);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(687, 287);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(234, 111);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lokasi TPS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(568, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Jenis Sampah";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(571, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Volume (Ton)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnkeluarmenuinput);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnSimpan);
            this.panel1.Controls.Add(this.btnHapus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 694);
            this.panel1.TabIndex = 13;
            // 
            // btnkeluarmenuinput
            // 
            this.btnkeluarmenuinput.FlatAppearance.BorderSize = 0;
            this.btnkeluarmenuinput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkeluarmenuinput.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnkeluarmenuinput.ForeColor = System.Drawing.Color.White;
            this.btnkeluarmenuinput.Image = ((System.Drawing.Image)(resources.GetObject("btnkeluarmenuinput.Image")));
            this.btnkeluarmenuinput.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnkeluarmenuinput.Location = new System.Drawing.Point(3, 540);
            this.btnkeluarmenuinput.Margin = new System.Windows.Forms.Padding(2);
            this.btnkeluarmenuinput.Name = "btnkeluarmenuinput";
            this.btnkeluarmenuinput.Size = new System.Drawing.Size(150, 90);
            this.btnkeluarmenuinput.TabIndex = 23;
            this.btnkeluarmenuinput.Text = "Keluar";
            this.btnkeluarmenuinput.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnkeluarmenuinput.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.ForestGreen;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(165, 120);
            this.panel3.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.ForestGreen;
            this.label4.Font = new System.Drawing.Font("Harlow Solid Italic", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(25, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 101);
            this.label4.TabIndex = 15;
            this.label4.Text = "S";
            // 
            // btnSimpan
            // 
            this.btnSimpan.FlatAppearance.BorderSize = 0;
            this.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimpan.ForeColor = System.Drawing.Color.White;
            this.btnSimpan.Image = global::AplikasiSampahJabar.Properties.Resources.SAVE;
            this.btnSimpan.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSimpan.Location = new System.Drawing.Point(3, 344);
            this.btnSimpan.Margin = new System.Windows.Forms.Padding(2);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(150, 80);
            this.btnSimpan.TabIndex = 7;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHapus.ForeColor = System.Drawing.Color.White;
            this.btnHapus.Image = global::AplikasiSampahJabar.Properties.Resources.DELETED;
            this.btnHapus.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHapus.Location = new System.Drawing.Point(3, 438);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(150, 80);
            this.btnHapus.TabIndex = 8;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Location = new System.Drawing.Point(572, 162);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(263, 10);
            this.panel8.TabIndex = 18;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Location = new System.Drawing.Point(572, 63);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(145, 3);
            this.panel7.TabIndex = 18;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Location = new System.Drawing.Point(238, 167);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(145, 3);
            this.panel6.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.ForestGreen;
            this.panel4.Location = new System.Drawing.Point(156, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 699);
            this.panel4.TabIndex = 15;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(188, 607);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 17;
            this.btnPrevious.Text = "< Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(395, 607);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 18;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.ForeColor = System.Drawing.Color.White;
            this.lblPageInfo.Location = new System.Drawing.Point(280, 610);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(81, 17);
            this.lblPageInfo.TabIndex = 19;
            this.lblPageInfo.Text = "Page 1 of 1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(796, 607);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::AplikasiSampahJabar.Properties.Resources.icons8_weight_50;
            this.pictureBox3.Location = new System.Drawing.Point(518, 111);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(48, 59);
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AplikasiSampahJabar.Properties.Resources.icons8_trash_48;
            this.pictureBox2.Location = new System.Drawing.Point(518, 27);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 59);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AplikasiSampahJabar.Properties.Resources.icons8_location_50;
            this.pictureBox1.Location = new System.Drawing.Point(184, 111);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 59);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // beratsampah
            // 
            this.beratsampah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.beratsampah.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.beratsampah.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.beratsampah.ForeColor = System.Drawing.Color.White;
            this.beratsampah.Location = new System.Drawing.Point(572, 143);
            this.beratsampah.Name = "beratsampah";
            this.beratsampah.Size = new System.Drawing.Size(261, 22);
            this.beratsampah.TabIndex = 21;
            this.beratsampah.ValueChanged += new System.EventHandler(this.beratsampah_ValueChanged);
            // 
            // cmblokasitps
            // 
            this.cmblokasitps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.cmblokasitps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmblokasitps.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.cmblokasitps.ForeColor = System.Drawing.Color.White;
            this.cmblokasitps.FormattingEnabled = true;
            this.cmblokasitps.Location = new System.Drawing.Point(237, 140);
            this.cmblokasitps.Name = "cmblokasitps";
            this.cmblokasitps.Size = new System.Drawing.Size(265, 26);
            this.cmblokasitps.TabIndex = 22;
            this.cmblokasitps.SelectedIndexChanged += new System.EventHandler(this.cmblokasitps_SelectedIndexChanged);
            // 
            // cmbjenissampah
            // 
            this.cmbjenissampah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.cmbjenissampah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbjenissampah.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.cmbjenissampah.ForeColor = System.Drawing.Color.White;
            this.cmbjenissampah.FormattingEnabled = true;
            this.cmbjenissampah.Location = new System.Drawing.Point(571, 49);
            this.cmbjenissampah.Name = "cmbjenissampah";
            this.cmbjenissampah.Size = new System.Drawing.Size(264, 26);
            this.cmbjenissampah.TabIndex = 23;
            this.cmbjenissampah.SelectedIndexChanged += new System.EventHandler(this.cmbjenissampah_SelectedIndexChanged);
            // 
            // dateinputsampah
            // 
            this.dateinputsampah.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.dateinputsampah.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dateinputsampah.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.dateinputsampah.Location = new System.Drawing.Point(317, 267);
            this.dateinputsampah.Name = "dateinputsampah";
            this.dateinputsampah.Size = new System.Drawing.Size(518, 26);
            this.dateinputsampah.TabIndex = 24;
            this.dateinputsampah.ValueChanged += new System.EventHandler(this.dateinputsampah_ValueChanged);
            // 
            // richtxtcatatansampah
            // 
            this.richtxtcatatansampah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.richtxtcatatansampah.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richtxtcatatansampah.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.richtxtcatatansampah.ForeColor = System.Drawing.Color.White;
            this.richtxtcatatansampah.Location = new System.Drawing.Point(184, 212);
            this.richtxtcatatansampah.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richtxtcatatansampah.Name = "richtxtcatatansampah";
            this.richtxtcatatansampah.Size = new System.Drawing.Size(682, 42);
            this.richtxtcatatansampah.TabIndex = 74;
            this.richtxtcatatansampah.Text = "";
            this.richtxtcatatansampah.TextChanged += new System.EventHandler(this.richtxtcatatansampah_TextChanged);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.White;
            this.panel10.Location = new System.Drawing.Point(184, 256);
            this.panel10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(685, 4);
            this.panel10.TabIndex = 73;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(184, 197);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 16);
            this.label5.TabIndex = 72;
            this.label5.Text = "Catatan";
            // 
            // txtnamasampah
            // 
            this.txtnamasampah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.txtnamasampah.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtnamasampah.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.txtnamasampah.ForeColor = System.Drawing.Color.White;
            this.txtnamasampah.Location = new System.Drawing.Point(237, 59);
            this.txtnamasampah.Name = "txtnamasampah";
            this.txtnamasampah.Size = new System.Drawing.Size(265, 19);
            this.txtnamasampah.TabIndex = 75;
            this.txtnamasampah.TextChanged += new System.EventHandler(this.txtnamasampah_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(238, 82);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 4);
            this.panel2.TabIndex = 76;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(240, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 16);
            this.label6.TabIndex = 77;
            this.label6.Text = "Nama Sampah";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::AplikasiSampahJabar.Properties.Resources.icons8_trash_48;
            this.pictureBox4.Location = new System.Drawing.Point(187, 27);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(48, 59);
            this.pictureBox4.TabIndex = 78;
            this.pictureBox4.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(184, 272);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 16);
            this.label7.TabIndex = 79;
            this.label7.Text = "Tanggal Masuk";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(878, 694);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtnamasampah);
            this.Controls.Add(this.richtxtcatatansampah);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateinputsampah);
            this.Controls.Add(this.cmbjenissampah);
            this.Controls.Add(this.cmblokasitps);
            this.Controls.Add(this.beratsampah);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "SampahJabar";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beratsampah)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnkeluarmenuinput;
        private System.Windows.Forms.NumericUpDown beratsampah;
        private System.Windows.Forms.ComboBox cmblokasitps;
        private System.Windows.Forms.ComboBox cmbjenissampah;
        private System.Windows.Forms.DateTimePicker dateinputsampah;
        private System.Windows.Forms.RichTextBox richtxtcatatansampah;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtnamasampah;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label7;
    }
}

