using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.Services
{
    public interface IReporteService
    {
        List<string> ObtenerMesesConRegistros();
        List<string> ObtenerCategoriasRegistradas();
        IEnumerable<SolicitudReporte> ObtenerSolicitudesFiltradas(string mes, string categoria);
    }
}
