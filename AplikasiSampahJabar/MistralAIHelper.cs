using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AplikasiSampahJabar
{
    public class MistralAIHelper
    {
        private readonly HttpClient _httpClient;
        private readonly SampahHelper _sampahHelper;

        public MistralAIHelper(SampahHelper sampahHelper)
        {
            _httpClient = new HttpClient();
            _sampahHelper = sampahHelper;
            
            if (Constants.MISTRAL_API_KEY != "YOUR_MISTRAL_API_KEY_HERE")
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Constants.MISTRAL_API_KEY}");
            }
        }

        public async Task<string> GetChatResponseAsync(string userMessage)
        {
            try
            {
                if (Constants.MISTRAL_API_KEY == "YOUR_MISTRAL_API_KEY_HERE")
                {
                    return "Maaf, API Key Mistral belum diset. Silakan masukkan API Key Anda di Constants.cs.";
                }

                var context = BuildDatabaseContext();
                var systemPromptWithContext = $"{Constants.TRASHY_SYSTEM_PROMPT}\n\n[Konteks Database Terbaru]\n{context}";

                var requestBody = new
                {
                    model = Constants.MISTRAL_MODEL,
                    messages = new[]
                    {
                        new { role = "system", content = systemPromptWithContext },
                        new { role = "user", content = userMessage }
                    },
                    temperature = 0.7
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(Constants.MISTRAL_API_ENDPOINT, content);
                
                if (!response.IsSuccessStatusCode)
                {
                    string errorDetail = await response.Content.ReadAsStringAsync();
                    return $"Gagal menghubungi Mistral AI: {response.ReasonPhrase}. Detail: {errorDetail}";
                }

                var responseString = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);
                
                string aiResponse = jsonResponse.choices[0].message.content;
                return aiResponse;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GetChatResponseAsync");
                return "Maaf, terjadi kesalahan saat menghubungi Trashy AI: " + ex.Message;
            }
        }

        private string BuildDatabaseContext()
        {
            try
            {
                var allSampah = _sampahHelper.GetAllSampah();
                int total = allSampah.Count;
                int pending = allSampah.Count(s => s.Status == Constants.STATUS_PENDING || s.Status == "Menunggu");
                int dijemput = allSampah.Count(s => s.Status == Constants.STATUS_DIJEMPUT);
                int selesai = allSampah.Count(s => s.Status == Constants.STATUS_SELESAI);

                var tpsCounts = allSampah.GroupBy(s => s.LokasiTPS)
                    .Select(g => $"- {g.Key}: {g.Count()} item")
                    .ToList();

                var sb = new StringBuilder();
                sb.AppendLine($"Waktu Database: {DateTime.Now:dd/MM/yyyy HH:mm}");
                sb.AppendLine($"- Total Laporan Sampah: {total}");
                sb.AppendLine($"- Menunggu Dijemput: {pending}");
                sb.AppendLine($"- Dalam Proses Jemput: {dijemput}");
                sb.AppendLine($"- Sudah Selesai: {selesai}");
                sb.AppendLine("- Sebaran per TPS:");
                foreach (var tps in tpsCounts)
                {
                    sb.AppendLine(tps);
                }

                return sb.ToString();
            }
            catch
            {
                return "Data database saat ini tidak dapat diakses.";
            }
        }
    }
}
