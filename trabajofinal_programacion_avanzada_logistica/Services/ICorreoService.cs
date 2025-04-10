using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.View;
using trabajofinal_programacion_avanzada_logistica.Models;


namespace trabajofinal_programacion_avanzada_logistica.Services
{

    /// Interfaz que define el contrato para el servicio de correo electrónico.
    /// Esta interfaz forma parte de la capa de servicios en la arquitectura MVP,
    /// actuando como un servicio auxiliar que puede ser utilizado por el Presentador
    /// para enviar notificaciones por correo electrónico.
    /// Implementa el principio de Segregación de Interfaces (I en SOLID) al definir
    /// sólo los métodos necesarios para el envío de correos específicos.
    /// También facilita el principio de Inversión de Dependencias (D en SOLID) ya que
    /// los presentadores pueden depender de esta abstracción en lugar de implementaciones concretas.
    public interface ICorreoService
    {

        /// Envía un correo electrónico con información de experiencia de usuario de forma asíncrona.
        /// Este método demuestra el principio de Responsabilidad Única (S en SOLID)
        /// al enfocarse específicamente en el envío de correos de feedback.
        /// Tarea asíncrona que representa la operación de envío
        Task EnviarCorreoAsync(ExperienciaUsuarioModel experiencia);


        /// Envía un correo electrónico con el reporte de una solicitud de reembolso de forma asíncrona.
        /// Este método también demuestra el principio de Responsabilidad Única (S en SOLID)
        /// al enfocarse específicamente en el envío de correos de reembolsos.
        Task EnviarReporteReembolso(SolicitudModel solicitud);



    }

}

