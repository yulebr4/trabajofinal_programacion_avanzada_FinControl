using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using trabajofinal_programacion_avanzada_logistica.Models;
using trabajofinal_programacion_avanzada_logistica.View;

namespace trabajofinal_programacion_avanzada_logistica.Services
{
    public class CorreoService : ICorreoService
    {
        private readonly EmailConfig _config;

        public CorreoService(EmailConfig config)
        {
            _config = config;
        }

        public async Task EnviarCorreoAsync(ExperienciaUsuarioModel experiencia)
        {
            var client = new SmtpClient();
            try
            {
                var mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress("Sistema de Feedback", _config.FromEmail));
                mensaje.To.Add(new MailboxAddress("Administrador", _config.ToEmail));
                mensaje.Subject = "Nuevo feedback de experiencia de usuario";

                var builder = new BodyBuilder
                {
                    TextBody = GenerarCuerpo(experiencia),
                    HtmlBody = GenerarCuerpoHtml(experiencia)
                };

                mensaje.Body = builder.ToMessageBody();

                await client.ConnectAsync(_config.SmtpServer, _config.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config.Username, _config.Password);
                await client.SendAsync(mensaje);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar correo: {ex.Message}");
            }
            finally
            {
                if (client.IsConnected)
                {
                    await client.DisconnectAsync(true);
                }
                client.Dispose();
            }
        }

        private string GenerarCuerpo(ExperienciaUsuarioModel experiencia)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Nivel de Satisfacción: {experiencia.Satisfaccion}");
            sb.AppendLine($"Comentarios: {experiencia.Comentarios}");

            if (experiencia.AreasMejora.Count > 0)
            {
                sb.AppendLine("Áreas que desea mejorar:");
                experiencia.AreasMejora.ForEach(a => sb.AppendLine($"- {a}"));
            }
            else
            {
                sb.AppendLine("No se seleccionaron áreas a mejorar");
            }

            return sb.ToString();
        }

        private string GenerarCuerpoHtml(ExperienciaUsuarioModel experiencia)
        {
            return $@"<h1>Nuevo Feedback de Experiencia de Usuario</h1>
                    <p><strong>Nivel de Satisfacción:</strong> {experiencia.Satisfaccion}</p>
                    <p><strong>Comentarios:</strong> {experiencia.Comentarios}</p>
                    <p><strong>Áreas a Mejorar:</strong> {string.Join(", ", experiencia.AreasMejora)}</p>";
        }
    }
}