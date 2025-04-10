using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.Models
{

    /// Modelo de configuración de correo electrónico.
    /// Pertenece a la capa Models de la arquitectura MVP.
    /// Esta clase contiene las propiedades necesarias para configurar el envío de correos electrónicos.
    public class EmailConfig
    {

        /// Servidor SMTP que se utiliza para enviar correos electrónicos.
        /// En este caso, se está utilizando el servidor SMTP de Gmail
        public string SmtpServer { get; set; } = "smtp.gmail.com";

        /// Puerto que utiliza el servidor SMTP.
        /// El puerto 587 es el estándar para envío de correos con TLS (Gmail).
        public int SmtpPort { get; set; } = 587;

        /// Nombre de usuario de la cuenta de correo (normalmente el correo electrónico).
        /// Se utiliza para autenticarse en el servidor SMTP.
        public string Username { get; set; } = "fincontrol521@gmail.com";

        /// Contraseña generada por Gmail para aplicaciones (App Password).
        /// No se recomienda dejarla visible en el código fuente por seguridad.
        public string Password { get; set; } = "ixfq rozy dkxx lway";

        /// Dirección de correo electrónico del remitente (quien envía el correo).
        public string FromEmail { get; set; } = "fincontrol521@gmail.com";

        /// Dirección de correo electrónico del destinatario (quien recibe el correo).
        public string ToEmail { get; set; } = "fincontrol521@gmail.com";
    }
}
