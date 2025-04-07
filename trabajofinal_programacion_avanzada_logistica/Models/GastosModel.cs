using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.Model
{
    public class GastosModel
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string ComprobantePath { get; set; }
        public string Empleado { get; set; }
    }
}

