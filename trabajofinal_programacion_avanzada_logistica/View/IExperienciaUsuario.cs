using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public interface IExperienciaUsuario
    {
        ExperienciaUsuarioModel ObtenerExperiencia();
        void MostrarMensaje(string mensaje);

    }
}
