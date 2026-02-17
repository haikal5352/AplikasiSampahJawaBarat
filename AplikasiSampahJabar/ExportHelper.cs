using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using Image = iText.Layout.Element.Image;

namespace AplikasiSampahJabar
{
    public static class ExportHelper
    {
        public static void ExportToPDF(List<SampahModel> data)
     {
            try
       {
                if (data == null || data.Count == 0)
      {
           ErrorHandler.ShowWarning("Tidak ada data untuk diexport!", "Peringatan");
       return;
                }

  SaveFileDialog sfd = new SaveFileDialog
              {
     Filter = "PDF Files (*.pdf)|*.pdf",
        FileName = $"Laporan_Sampah_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
     };

          if (sfd.ShowDialog() == DialogResult.OK)
        {
      GeneratePDF(sfd.FileName, data);
        ErrorHandler.ShowInfo("PDF berhasil dibuat!", Constants.TITLE_SUCCESS);
      }
            }
  catch (Exception ex)
 {
                ErrorHandler.LogError(ex, "ExportToPDF");
    ErrorHandler.ShowError("Gagal export PDF: " + ex.Message);
            }
    }

     private static void GeneratePDF(string fileName, List<SampahModel> data)
        {
   using (PdfWriter writer = new PdfWriter(fileName))
     {
    using (PdfDocument pdf = new PdfDocument(writer))
           {
        Document doc = new Document(pdf, PageSize.A4.Rotate());
   
        // Title
     Paragraph title = new Paragraph("Laporan Data Sampah Jawa Barat")
 .SetTextAlignment(TextAlignment.CENTER)
        .SetFontSize(20);
       doc.Add(title);
         
         doc.Add(new Paragraph($"Tanggal Cetak: {DateTime.Now:dd/MM/yyyy HH:mm}")
   .SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));

    doc.Add(new Paragraph("\n"));

          // Table with all columns matching dashboard detail
         Table table = new Table(UnitValue.CreatePercentArray(new float[] { 15, 10, 15, 10, 10, 15, 10, 10 }));
           table.SetWidth(UnitValue.CreatePercentValue(100));

         // Headers
      string[] headers = { "Nama", "Jenis", "Lokasi", "Status", "Waktu", "Catatan", "Petugas", "Jemput" };
       foreach (var header in headers)
         {
        table.AddHeaderCell(new Cell().Add(new Paragraph(header)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
               }

        // Data
      foreach (var s in data)
       {
     table.AddCell(s.NamaSampah ?? "-");
          table.AddCell(s.JenisSampah ?? "-");
      table.AddCell(s.LokasiTPS ?? "-");
          table.AddCell(s.Status ?? "-");
      table.AddCell(s.WaktuInput.ToString("dd/MM/yyyy"));
  table.AddCell(s.Catatan ?? "-");
   table.AddCell(s.NamaPetugasInput ?? "-");
         table.AddCell(s.NamaPetugasJemput ?? "-");
  }

      doc.Add(table);
        
    // Footer
        doc.Add(new Paragraph("\n"));
      doc.Add(new Paragraph($"Total Data: {data.Count} items."));
          }
      }
        }

 public static void ExportChartToPDF(string fileName, string chartImagePath)
{
            try
        {
using (PdfWriter writer = new PdfWriter(fileName))
{
   using (PdfDocument pdf = new PdfDocument(writer))
      {
     Document doc = new Document(pdf, PageSize.A4);
     
    // Title
        Paragraph title = new Paragraph("Laporan Grafik Sampah Per Jenis")
    .SetTextAlignment(TextAlignment.CENTER)
        .SetFontSize(18)
      .SetMarginBottom(20);
     doc.Add(title);
        
    // Tanggal Cetak
 doc.Add(new Paragraph($"Tanggal Cetak: {DateTime.Now:dd/MM/yyyy HH:mm}")
      .SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10));
        
 doc.Add(new Paragraph("\n"));

          // Add chart image
       if (System.IO.File.Exists(chartImagePath))
        {
   Image img = new Image(
 global::iText.IO.Image.ImageDataFactory.Create(chartImagePath))
      .SetWidth(500)
  .SetAutoScale(true);
 doc.Add(img);
   }
       }
       }
 }
        catch (Exception ex)
    {
 ErrorHandler.LogError(ex, "ExportChartToPDF");
    throw;
   }
   }
    }
}
