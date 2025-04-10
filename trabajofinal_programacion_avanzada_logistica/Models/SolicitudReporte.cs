using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.Models
{
    public class SolicitudReporte
    {
        public DateTime Fecha { get; set; }
        public string Empleado { get; set; }
        public string Categoria { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
    }
}
