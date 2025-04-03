using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Repository;
using trabajofinal_programacion_avanzada_logistica.Services;
using trabajofinal_programacion_avanzada_logistica.View;
using System.IO;

namespace trabajofinal_programacion_avanzada_logistica.Presenter
{
    public class GastosPresenter
    {
        private readonly IGastosView view;
        private readonly FinControlDBEntities db; // Modelo de Entity Framework

        public GastosPresenter(IGastosView view)
        {
            this.view = view;
            db = new FinControlDBEntities(); // Inicializa el contexto de la BD

            this.view.GuardarGasto += OnGuardarGasto;
            this.view.AdjuntarComprobante += OnAdjuntarComprobante;
        }

        private void OnGuardarGasto(object sender, EventArgs e)
        {
            var gasto = new Data.Gastos
            {
                Categoria = view.Categoria,
                Monto = view.Monto,
                Fecha = view.Fecha,
                Comprobante = view.Comprobante,
                 ProyectoId = view.ProyectoId
            };

            db.Gastos.Add(gasto);
            db.SaveChanges();
            MessageBox.Show("Gasto registrado con éxito.");
        }

        private void OnAdjuntarComprobante(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    view.Comprobante = openFileDialog.FileName;
                }
            }
        }

        private void GenerarInformePdf(Data.Gastos gasto)
        {
            PdfService pdfService = new PdfService();
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "InformeGasto.pdf");

            pdfService.GenerarInforme(filePath, gasto.Categoria, gasto.Monto, gasto.Fecha);
            MessageBox.Show($"Informe generado en: {filePath}");
        }
    }
}