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

        event EventHandler GuardarGasto;
        event EventHandler AdjuntarComprobante;
        void MostrarMensaje(string mensaje);
    }
}
