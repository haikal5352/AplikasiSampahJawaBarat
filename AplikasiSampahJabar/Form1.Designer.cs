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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKabupaten = new System.Windows.Forms.TextBox();
            this.txtJenis = new System.Windows.Forms.TextBox();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.btnKirimChat = new System.Windows.Forms.Button();
            this.rtbChatArea = new System.Windows.Forms.RichTextBox();
            this.txtChatInput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(600, 400);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(618, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kabupaten/Kota";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(618, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Jenis Sampah";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(618, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Volume (Ton)";
            // 
            // txtKabupaten
            // 
            this.txtKabupaten.Location = new System.Drawing.Point(622, 35);
            this.txtKabupaten.Name = "txtKabupaten";
            this.txtKabupaten.Size = new System.Drawing.Size(100, 26);
            this.txtKabupaten.TabIndex = 4;
            // 
            // txtJenis
            // 
            this.txtJenis.Location = new System.Drawing.Point(618, 137);
            this.txtJenis.Name = "txtJenis";
            this.txtJenis.Size = new System.Drawing.Size(100, 26);
            this.txtJenis.TabIndex = 5;
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(618, 264);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(100, 26);
            this.txtVolume.TabIndex = 6;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(548, 609);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(75, 23);
            this.btnSimpan.TabIndex = 7;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(629, 609);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 8;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(710, 609);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(75, 23);
            this.btnExportPDF.TabIndex = 9;
            this.btnExportPDF.Text = "Export ke PDF";
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // btnKirimChat
            // 
            this.btnKirimChat.Location = new System.Drawing.Point(791, 609);
            this.btnKirimChat.Name = "btnKirimChat";
            this.btnKirimChat.Size = new System.Drawing.Size(75, 23);
            this.btnKirimChat.TabIndex = 10;
            this.btnKirimChat.Text = "Kirim ke Chat";
            this.btnKirimChat.UseVisualStyleBackColor = true;
            this.btnKirimChat.Click += new System.EventHandler(this.btnKirimChat_Click);
            // 
            // rtbChatArea
            // 
            this.rtbChatArea.Location = new System.Drawing.Point(12, 418);
            this.rtbChatArea.Name = "rtbChatArea";
            this.rtbChatArea.Size = new System.Drawing.Size(599, 96);
            this.rtbChatArea.TabIndex = 11;
            this.rtbChatArea.Text = "";
            // 
            // txtChatInput
            // 
            this.txtChatInput.Location = new System.Drawing.Point(12, 520);
            this.txtChatInput.Name = "txtChatInput";
            this.txtChatInput.Size = new System.Drawing.Size(100, 26);
            this.txtChatInput.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 644);
            this.Controls.Add(this.txtChatInput);
            this.Controls.Add(this.rtbChatArea);
            this.Controls.Add(this.btnKirimChat);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.txtVolume);
            this.Controls.Add(this.txtJenis);
            this.Controls.Add(this.txtKabupaten);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKabupaten;
        private System.Windows.Forms.TextBox txtJenis;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.Button btnKirimChat;
        private System.Windows.Forms.RichTextBox rtbChatArea;
        private System.Windows.Forms.TextBox txtChatInput;
    }
}

