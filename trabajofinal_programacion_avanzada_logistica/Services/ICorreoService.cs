using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.View;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.Services
{
    public interface ICorreoService
    {
        Task EnviarCorreoAsync(ExperienciaUsuarioModel experiencia);
    }

}

