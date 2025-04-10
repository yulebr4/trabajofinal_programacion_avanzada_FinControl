using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public interface IReporteView
    {
        string MesSeleccionado { get; }
        string CategoriaSeleccionada { get; }

 
        void MostrarMensaje(string mensaje);
        void CargarMeses(List<string> meses);
        void CargarCategorias(List<string> categorias);
        void MostrarSolicitudes(IEnumerable<SolicitudReporte> solicitudes);

    }
}
