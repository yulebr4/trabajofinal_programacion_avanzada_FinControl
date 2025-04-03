using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public interface IExperienciaUsuario
    {
        event EventHandler OnEnviarClicked;
        string Satisfaccion { get; }
        string Comentarios { get; }
        List<string> AreasMejora { get; }
    }
}
