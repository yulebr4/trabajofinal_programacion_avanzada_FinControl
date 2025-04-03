using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Models;
using trabajofinal_programacion_avanzada_logistica.View;
using MimeKit;
using MailKit.Net.Smtp;
using trabajofinal_programacion_avanzada_logistica.Services;

namespace trabajofinal_programacion_avanzada_logistica.Presenter
{
    public class ExperienciaUsuarioPresenter
    {
        private readonly IExperienciaUsuario _view;

        // Configura el correo
        private readonly string smtpServidor = "smtp.gmail.com";
        private readonly int smtpPuerto = 587;
        private readonly string correoEmisor = "yulennybonilla6l@gmail.com";
        private readonly string claveCorreo = "ylbr404";

        public ExperienciaUsuarioPresenter(IExperienciaUsuario view)
        {
            _view = view;
            _view.OnEnviarClicked += EnviarCorreo;
        }

        private void EnviarCorreo(object sender, EventArgs e)
        {
            var experiencia = new ExperienciaUsuarioModel
            {
                Satisfaccion = _view.Satisfaccion,
                Comentarios = _view.Comentarios,
                AreasMejora = _view.AreasMejora
            };

            EnviarCorreoElectronico(experiencia);
        }

        private void EnviarCorreoElectronico(ExperienciaUsuarioModel experiencia)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sistema de Experiencia", correoEmisor));
                message.To.Add(new MailboxAddress("Administrador", "destinatario@gmail.com"));
                message.Subject = "Nueva Experiencia de Usuario";

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = $"Satisfacción: {experiencia.Satisfaccion}\n" +
                               $"Comentarios: {experiencia.Comentarios}\n" +
                               $"Áreas a mejorar: {string.Join(", ", experiencia.AreasMejora)}"
                };

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(smtpServidor, smtpPuerto, false);
                    client.Authenticate(correoEmisor, claveCorreo);
                    client.Send(message);
                    client.Disconnect(true);
                }

                _view.MostrarMensaje("Correo enviado exitosamente.");
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al enviar el correo: {ex.Message}");
            }
        }
    }
}