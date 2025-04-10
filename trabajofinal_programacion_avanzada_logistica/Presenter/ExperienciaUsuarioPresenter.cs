using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.View;
using MimeKit;
using MailKit.Net.Smtp;
using trabajofinal_programacion_avanzada_logistica.Services;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.Presenter
{

    // Clase Presenter perteneciente a la capa de Presentación dentro del patrón MVP.
    // Esta clase actúa como intermediario entre la Vista (View) y los Servicios/Modelo (Model).
    public class ExperienciaUsuarioPresenter
    {

        // Atributo privado que almacena una referencia a la vista (Vista que implementa IExperienciaUsuario).
        private readonly IExperienciaUsuario _view;

        // Servicio responsable de enviar correos electrónicos (abstracción por medio de interfaz ICorreoService).
        private readonly ICorreoService _emailService;

        // Lista privada que almacena el histórico de experiencias enviadas por los usuarios.
        private List<ExperienciaUsuarioModel> _historicoFeedback = new List<ExperienciaUsuarioModel>();



        // Constructor del Presenter.
        // Recibe la Vista (por inyección de dependencia) y una configuración de correo.
        // Se instancia el servicio de envío de correo (CorreoService) utilizando la configuración.
        public ExperienciaUsuarioPresenter(IExperienciaUsuario view, EmailConfig emailConfig)
        {
            _view = view;
            _emailService = new CorreoService(emailConfig);
        }



        // Método encargado de manejar el flujo de enviar la experiencia del usuario.
        // Este método se comunica con la Vista y con el Servicio de envío de correo.
        public async void EnviarExperiencia()
        {

            // Se obtienen los datos de experiencia desde la Vista.
            var experiencia = _view.ObtenerExperiencia();


            // Validación: si el campo de satisfacción está vacío, se muestra un mensaje de advertencia.
            if (string.IsNullOrEmpty(experiencia.Satisfaccion))
            {
                _view.MostrarMensaje("Por favor seleccione un nivel de satisfacción");
                return; // Se detiene la ejecución.
            }

            try
            {

                // Si todo está correcto, se envía el correo de forma asíncrona usando el servicio.
                await _emailService.EnviarCorreoAsync(experiencia);

                // Se muestra mensaje de éxito al usuario.
                _view.MostrarMensaje("¡Gracias por tu opinión!");
            }
            catch (Exception ex)
            {

                // En caso de error, se muestra un mensaje con el detalle de la excepción.
                _view.MostrarMensaje($"Error al enviar: {ex.Message}");
            }
        }



    }
    
}