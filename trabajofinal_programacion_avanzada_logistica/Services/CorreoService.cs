using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.Services
{
    public class CorreoService
    {
       
            private readonly string smtpServidor = "smtp.gmail.com";
            private readonly int smtpPuerto = 587;
            private readonly string correoEmisor = "yulennybonilla6@gmail.com";
            private readonly string claveCorreo = "yulebr4";

            public void EnviarCorreo(ExperienciaUsuarioModel experiencia)
            {
                try
                {
                    var mensaje = new MimeMessage();
                    mensaje.From.Add(new MailboxAddress("Sistema Experiencia Usuario", correoEmisor));
                    mensaje.To.Add(new MailboxAddress("Administrador", "admin@gmail.com"));
                    mensaje.Subject = "Nueva Experiencia de Usuario";

                    var cuerpo = new BodyBuilder
                    {
                        TextBody = $"Satisfacción: {experiencia.Satisfaccion}\n" +
                                   $"Comentarios: {experiencia.Comentarios}\n" +
                                   $"Áreas a mejorar: {string.Join(", ", experiencia.AreasMejora)}"
                    };

                    mensaje.Body = cuerpo.ToMessageBody();

                    using (var cliente = new SmtpClient())
                    {
                        cliente.Connect(smtpServidor, smtpPuerto, false);
                        cliente.Authenticate(correoEmisor, claveCorreo);
                        cliente.Send(mensaje);
                        cliente.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al enviar correo: {ex.Message}");
                }
            }
        }
    }