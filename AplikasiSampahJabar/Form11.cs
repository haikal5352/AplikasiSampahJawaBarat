using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Colors;

namespace AplikasiSampahJabar
{
    public partial class Form11 : Form
    {
        private SampahHelper sampahHelper;
        private MistralAIHelper aiHelper;
        private MongoClient client;
        private IMongoDatabase database;
        private string _initialInstructions;

        public Form11()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.WindowState = FormWindowState.Normal;

            InitializeDatabase();
            InitializeUI();
        }

        private void InitializeDatabase()
        {
            try
            {
                string connectionString = System.IO.File.Exists(Constants.CONFIG_FILE)
                    ? System.IO.File.ReadAllText(Constants.CONFIG_FILE).Trim()
                    : Constants.DEFAULT_CONNECTION_STRING;

                client = new MongoClient(connectionString);
                database = client.GetDatabase(Constants.DATABASE_NAME);
                sampahHelper = new SampahHelper(database);
                aiHelper = new MistralAIHelper(sampahHelper);
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "InitializeDatabase");
                ErrorHandler.ShowError("Gagal terhubung ke database.");
            }
        }

        private void InitializeUI()
        {
            // Store initial instructions from Designer/RESX
            _initialInstructions = Panelchat.Text;
            
            // Append initial greeting
            AppendChat("Trashy", "Halo! Saya Trashy AI, asisten pengelolaan sampah Anda. Ada yang bisa saya bantu hari ini?");
            
            // Wire up events
            if (kirim != null) kirim.Click += async (s, e) => await SendMessage();
            if (Reset != null) Reset.Click += (s, e) => ResetChat();
            if (btnExportPDF != null) btnExportPDF.Click += (s, e) => ExportChatToPDF();
            
            if (panelchatbot != null)
            {
                string placeholder = "Ketik Disini Untuk Bertanya Seputar Sampah Ke Trashy";
                
                panelchatbot.Enter += (s, e) =>
                {
                    if (panelchatbot.Text == placeholder)
                    {
                        panelchatbot.Text = "";
                        panelchatbot.ForeColor = System.Drawing.Color.White;
                    }
                };

                panelchatbot.Leave += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(panelchatbot.Text))
                    {
                        panelchatbot.Text = placeholder;
                        panelchatbot.ForeColor = System.Drawing.Color.Gray;
                    }
                };

                panelchatbot.KeyDown += async (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        await SendMessage();
                    }
                };
            }

            // Wire up Keluar
            Button btnKeluar = this.Controls.Find("button4", true).FirstOrDefault() as Button;
            if (btnKeluar != null) btnKeluar.Click += (s, e) => this.Close();
        }

        private async Task SendMessage()
        {
            string userMessage = panelchatbot.Text.Trim();
            if (string.IsNullOrEmpty(userMessage) || userMessage == "Ketik Disini Untuk Bertanya Seputar Sampah Ke Trashy")
                return;

            AppendChat("Anda", userMessage);
            panelchatbot.Clear();

            AppendChat("Trashy", "[Sedang berpikir...]");
            
            string aiResponse = await aiHelper.GetChatResponseAsync(userMessage);
            
            // Remove "[Sedang berpikir...]" and add real response
            RemoveLastLine();
            AppendChat("Trashy", aiResponse);
        }

        private void AppendChat(string sender, string message)
        {
            if (Panelchat.InvokeRequired)
            {
                Panelchat.Invoke(new Action(() => AppendChat(sender, message)));
                return;
            }

            Panelchat.SelectionStart = Panelchat.TextLength;
            Panelchat.SelectionFont = new System.Drawing.Font(Panelchat.Font, System.Drawing.FontStyle.Bold);
            Panelchat.AppendText($"{sender}: ");
            
            Panelchat.SelectionStart = Panelchat.TextLength;
            Panelchat.SelectionFont = new System.Drawing.Font(Panelchat.Font, System.Drawing.FontStyle.Regular);
            Panelchat.AppendText($"{message}\n\n");
            
            Panelchat.ScrollToCaret();
        }

        private void RemoveLastLine()
        {
            if (Panelchat.InvokeRequired)
            {
                Panelchat.Invoke(new Action(RemoveLastLine));
                return;
            }

            string[] lines = Panelchat.Lines;
            if (lines.Length > 0)
            {
                // Simple way: find the last occurrence of "Trashy: [Sedang berpikir...]"
                int lastIdx = Panelchat.Text.LastIndexOf("Trashy: [Sedang berpikir...]");
                if (lastIdx != -1)
                {
                    Panelchat.Select(lastIdx, Panelchat.Text.Length - lastIdx);
                    Panelchat.SelectedText = "";
                }
            }
        }

        private void ResetChat()
        {
            Panelchat.Text = _initialInstructions;
            AppendChat("Trashy", "Percakapan telah direset. Ada lagi yang bisa saya bantu?");
            panelchatbot.Focus();
        }

        private void ExportChatToPDF()
        {
            try
            {
                if (string.IsNullOrEmpty(Panelchat.Text))
                {
                    ErrorHandler.ShowWarning("Tidak ada chat untuk diexport!", "Peringatan");
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = $"Chat_Trashy_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (PdfWriter writer = new PdfWriter(sfd.FileName))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            Document doc = new Document(pdf, PageSize.A4);
                            
                            doc.Add(new Paragraph("Riwayat Chat Trashy AI")
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(18));
                            
                            doc.Add(new Paragraph($"Tanggal Export: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                .SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
                            
                            doc.Add(new Paragraph("\n"));
                            
                            string[] lines = Panelchat.Text.Split(new[] { "\n\n" }, StringSplitOptions.None);
                            foreach (var line in lines)
                            {
                                if (string.IsNullOrWhiteSpace(line)) continue;
                                
                                var p = new Paragraph(line).SetFontSize(11);
                                if (line.StartsWith("Trashy:"))
                                {
                                    p.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                                }
                                doc.Add(p);
                            }
                        }
                    }
                    ErrorHandler.ShowInfo("Chat successfully exported to PDF!", Constants.TITLE_SUCCESS);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "ExportChatToPDF");
                ErrorHandler.ShowError("Gagal export chat: " + ex.Message);
            }
        }

        private void Form11_Load(object sender, EventArgs e) { }
        private void button4_Click(object sender, EventArgs e) { }
        private void panel10_Paint(object sender, PaintEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}
