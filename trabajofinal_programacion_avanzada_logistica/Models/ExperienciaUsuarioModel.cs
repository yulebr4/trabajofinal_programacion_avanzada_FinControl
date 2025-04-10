using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.Models
{
    public class ExperienciaUsuarioModel
    {
        public string Satisfaccion { get; set; }
        public string Comentarios { get; set; }
        public List<string> AreasMejora { get; set; }

        public string Calificacion => Satisfaccion;


    }
}
