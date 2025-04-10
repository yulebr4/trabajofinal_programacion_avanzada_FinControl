using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.Models
{

    /// Modelo que representa la experiencia del usuario.
    /// Forma parte de la capa Models en la arquitectura MVP.
    /// Contiene las propiedades necesarias para almacenar la experiencia que el usuario proporciona.
    public class ExperienciaUsuarioModel
    {

        /// Nivel de satisfacción seleccionado por el usuario.
        /// Puede ser por ejemplo: "Excelente", "Bueno", "Regular", "Malo".
        public string Satisfaccion { get; set; }

        /// Comentarios adicionales proporcionados por el usuario sobre su experiencia.
        public string Comentarios { get; set; }

        /// Lista de las áreas de mejora seleccionadas por el usuario.
        /// Permite seleccionar varios aspectos a mejorar
        public List<string> AreasMejora { get; set; }

        /// Propiedad de solo lectura que devuelve la calificación basada en la propiedad Satisfaccion.
        /// Se utiliza como un alias de Satisfaccion.
        public string Calificacion => Satisfaccion;


    }
}
