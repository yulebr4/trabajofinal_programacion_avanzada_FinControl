using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public interface ILoginView
    {

        string Usuario { get; }
        string Contraseña { get; }
        void MostrarMensaje(string mensaje);
        void CerrarFormulario();
    }
}
