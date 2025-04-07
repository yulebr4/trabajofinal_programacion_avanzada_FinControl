using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.Models
{
    public class EmailConfig
    {
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 587;
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
    }
}
