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
    public class ExperienciaUsuarioPresenter
    {
        private readonly IExperienciaUsuario _view;
        private readonly ICorreoService _emailService;
        private List<ExperienciaUsuarioModel> _historicoFeedback = new List<ExperienciaUsuarioModel>();


        public ExperienciaUsuarioPresenter(IExperienciaUsuario view, EmailConfig emailConfig)
        {
            _view = view;
            _emailService = new CorreoService(emailConfig);
        }

        public async void EnviarExperiencia()
        {
            var experiencia = _view.ObtenerExperiencia();

            if (string.IsNullOrEmpty(experiencia.Satisfaccion))
            {
                _view.MostrarMensaje("Por favor seleccione un nivel de satisfacción");
                return;
            }

            try
            {
                await _emailService.EnviarCorreoAsync(experiencia);
                _view.MostrarMensaje("¡Gracias por tu opinión!");
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al enviar: {ex.Message}");
            }
        }



    }
    
}