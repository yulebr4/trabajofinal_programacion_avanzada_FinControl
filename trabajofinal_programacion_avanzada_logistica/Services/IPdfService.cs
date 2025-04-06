using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using trabajofinal_programacion_avanzada_logistica.Models;
using System.Windows.Forms;


namespace trabajofinal_programacion_avanzada_logistica.Services
{
    public interface IPdfService
    {
        
        string GenerarComprobantePdf(GastosModel gasto);
    }

    public class PdfService : IPdfService
    {
        public string GenerarComprobantePdf(GastosModel gasto)
        {
            string folderPath = Path.Combine(Application.StartupPath, "Comprobantes");
            Directory.CreateDirectory(folderPath);

            string pdfPath = Path.Combine(folderPath, $"Comprobante_{DateTime.Now:yyyyMMddHHmmss}.pdf");

            using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();

                // Título
                doc.Add(new Paragraph("COMPROBANTE DE GASTO", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18)));
                doc.Add(Chunk.NEWLINE);

                // Información del gasto
                doc.Add(new Paragraph($"Categoría: {gasto.Categoria}"));
                doc.Add(new Paragraph($"Descripción: {gasto.Descripcion}"));
                doc.Add(new Paragraph($"Monto: {gasto.Monto:C}"));
                doc.Add(new Paragraph($"Fecha: {gasto.Fecha:dd/MM/yyyy}"));
                doc.Add(new Paragraph($"Empleado: {gasto.Empleado}"));
                doc.Add(Chunk.NEWLINE);

                // Pie de página
                doc.Add(new Paragraph($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss}"));

                doc.Close();
            }

            return pdfPath;
        }
    }
}
    

