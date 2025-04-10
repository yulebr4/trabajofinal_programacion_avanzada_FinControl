using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public interface ISolicitudView
    {
        string Empleado { get; set; }
        string Categoria { get; set; }
        string Descripcion { get; set; }
        decimal Monto { get; set; }

        int SolicitudesPendientes { get; set; }
        decimal MontoTotalAprobado { get; set; }
        int SolicitudesRechazadas { get; set; }

        void HabilitarBotonEnviar(bool habilitar);
        void MostrarMensajeSeguro(string mensaje);
        void ActualizarCursor(Cursor cursor);

        void MostrarMensaje(string mensaje);
        void ActualizarDataGrid(IEnumerable<SolicitudModel> solicitudes);

    }
}
