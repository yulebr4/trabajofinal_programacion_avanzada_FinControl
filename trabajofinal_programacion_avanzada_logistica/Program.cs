using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.View;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Models;
using trabajofinal_programacion_avanzada_logistica.Presenter;
using trabajofinal_programacion_avanzada_logistica.Repository;
using trabajofinal_programacion_avanzada_logistica.Services;
using trabajofinal_programacion_avanzada_logistica.Data;

namespace trabajofinal_programacion_avanzada_logistica
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuPrincipal());



            var emailConfig = new EmailConfig
            {
                Username = "fincontrol521@gmail.com",
                Password = "ixfq rozy dkxx lway", // Usar contraseña de aplicación
                FromEmail = "fincontrol521@gmail.com",
                ToEmail = "fincontrol521@gmail.com",
                SmtpServer = "smtp.gmail.com",
                SmtpPort = 587
            };
            var solicitudService = new SolicitudService();
            var correoService = new CorreoService(emailConfig);

        }
    }
}
