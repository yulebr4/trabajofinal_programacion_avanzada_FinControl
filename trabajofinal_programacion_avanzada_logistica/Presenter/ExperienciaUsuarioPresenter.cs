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

namespace trabajofinal_programacion_avanzada_logistica.Presenter
{
    public class ExperienciaUsuarioPresenter
    {
        private readonly IExperienciaUsuario _view;

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
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sistema Experiencia Usuario", "yulennybonilla6@gmail.com"));
            message.To.Add(new MailboxAddress("Administrador", "yumagaymer@gmail.com"));
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
                client.Connect("smtp.example.com", 587, false);
                client.Authenticate("tuemail@example.com", "tucontraseña");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}

