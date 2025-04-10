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
        public string Username { get; set; } = "fincontrol521@gmail.com";
        public string Password { get; set; } = "ixfq rozy dkxx lway";
        public string FromEmail { get; set; } = "fincontrol521@gmail.com";
        public string ToEmail { get; set; } = "fincontrol521@gmail.com";
    }
}
