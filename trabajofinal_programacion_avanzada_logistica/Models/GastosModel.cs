using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.Model
{
    public class GastosModel
    {

        // Este modelo representa la estructura de un Gasto dentro de la aplicación.
        // Forma parte de la capa Model en la arquitectura MVP.
        // Cumple con el principio de Responsabilidad Única (SRP) ya que esta clase
        // solo se encarga de almacenar datos de un gasto.

        // Identificador único del gasto (clave primaria).
        public int Id { get; set; }


        // Categoría del gasto
        public string Categoria { get; set; }

        // Descripción o detalle del gasto.
        public string Descripcion { get; set; }

        // Monto o valor del gasto registrado.
        public decimal Monto { get; set; }

        // Fecha en la que se realizó el gasto.
        public DateTime Fecha { get; set; }

        // Ruta del archivo del comprobante asociado al gasto (PDF).
        public string ComprobantePath { get; set; }

        // Nombre del empleado que realizó el gasto.
        public string Empleado { get; set; }
    }
}

