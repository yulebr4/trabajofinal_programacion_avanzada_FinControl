using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.Services
{
    public interface ISolicitudService
    {
        IEnumerable<SolicitudModel> ObtenerTodas();
        void GuardarSolicitud(SolicitudModel solicitud);
        string DeterminarEstado(decimal monto);
    }
}
