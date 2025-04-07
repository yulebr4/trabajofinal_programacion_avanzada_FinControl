using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Model;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public interface IGastosView
    {
        string Categoria { get; }
        string Descripcion { get; }
        decimal Monto { get; }
        DateTime Fecha { get; }
        string Empleado { get; }
        string ComprobantePath { get; set; }

        event EventHandler CargarGastos;
        event EventHandler GuardarGasto;
        event EventHandler GenerarPdf;

        void MostrarGastos(List<GastosModel> gastos);
        void MostrarMensaje(string mensaje, string titulo);
        void LimpiarFormulario();
        bool ValidarCamposParaPDF();
        void MostrarErrorGeneracionPDF(string mensaje);
    }
}