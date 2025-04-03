using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public interface IGastosView
    {
        string Categoria { get; set; }
        decimal Monto { get; set; }
        DateTime Fecha { get; set; }
        string Comprobante { get; set; }
        int ProyectoId { get; }

        event EventHandler GuardarGasto;
        event EventHandler AdjuntarComprobante;
       
    }
}
