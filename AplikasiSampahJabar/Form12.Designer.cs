namespace AplikasiSampahJabar
{
    partial class Form12
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form12));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.chartsampah = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartsampah)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.ForestGreen;
            this.panel4.Location = new System.Drawing.Point(159, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 699);
            this.panel4.TabIndex = 76;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnExportPDF);
            this.panel5.Controls.Add(this.button4);
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(153, 694);
            this.panel5.TabIndex = 75;
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button4.Location = new System.Drawing.Point(0, 555);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 90);
            this.button4.TabIndex = 23;
            this.button4.Text = "Keluar";
            this.button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.ForestGreen;
            this.panel9.Controls.Add(this.label4);
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(165, 120);
            this.panel9.TabIndex = 15;
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
            // btnExportPDF
            // 
            this.btnExportPDF.FlatAppearance.BorderSize = 0;
            this.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPDF.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPDF.ForeColor = System.Drawing.Color.White;
            this.btnExportPDF.Image = global::AplikasiSampahJabar.Properties.Resources.PDF;
            this.btnExportPDF.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExportPDF.Location = new System.Drawing.Point(4, 448);
            this.btnExportPDF.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(150, 90);
            this.btnExportPDF.TabIndex = 77;
            this.btnExportPDF.Text = "Export ke PDF";
            this.btnExportPDF.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExportPDF.UseVisualStyleBackColor = true;
            // 
            // chartsampah
            // 
            this.chartsampah.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled;
            chartArea1.Name = "ChartArea1";
            this.chartsampah.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartsampah.Legends.Add(legend1);
            this.chartsampah.Location = new System.Drawing.Point(175, 12);
            this.chartsampah.Name = "chartsampah";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartsampah.Series.Add(series1);
            this.chartsampah.Size = new System.Drawing.Size(681, 670);
            this.chartsampah.TabIndex = 77;
            this.chartsampah.Text = "chart1";
            // 
            // Form12
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(878, 694);
            this.Controls.Add(this.chartsampah);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Name = "Form12";
            this.Text = "Form12";
            this.panel5.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartsampah)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartsampah;
    }
}