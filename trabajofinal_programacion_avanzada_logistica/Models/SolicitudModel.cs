using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.Models
{
    public class SolicitudModel
    {
        public int Id { get; set; }
        public string Empleado { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; } // "Pendiente", "Aprobada", "Rechazada"

        public DateTime Fecha { get; set; }
        public int UsuarioId { get; internal set; }
        public int GastoId { get; internal set; }
        public int EmpleadoId { get; internal set; }
        public DateTime FechaSolicitud { get; internal set; }
        public int CategoriaId { get; internal set; }
        public object EmpleadoNombre { get; internal set; }
    }



}
