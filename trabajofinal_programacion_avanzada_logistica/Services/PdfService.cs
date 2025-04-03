using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Paragraph = iText.Layout.Element.Paragraph;

namespace trabajofinal_programacion_avanzada_logistica.Services
{
    public class PdfService
    {
        public void GenerarInforme(string filePath, string categoria, decimal monto, DateTime fecha)
        {
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);
                    document.Add(new Paragraph("Informe de Gasto"));
                    document.Add(new Paragraph($"Categoría: {categoria}"));
                    document.Add(new Paragraph($"Monto: {monto:C}"));
                    document.Add(new Paragraph($"Fecha: {fecha:dd/MM/yyyy}"));
                }
            }
        }
    }
}