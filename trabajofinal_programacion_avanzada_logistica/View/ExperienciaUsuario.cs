using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Models;
using trabajofinal_programacion_avanzada_logistica.Presenter;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public partial class ExperienciaUsuario : Form, IExperienciaUsuario
    {

        private readonly ExperienciaUsuarioPresenter _presenter;

        public ExperienciaUsuario()
        {
            InitializeComponent();
            // Configuración del servicio de email
            var emailConfig = new EmailConfig
            {
                Username = "fincontrol521@gmail.com",
                Password = "ixfq rozy dkxx lway",
                FromEmail = "fincontrol521@gmail.com",
                ToEmail = "fincontrol521@gmail.com",
                SmtpServer = "smtp.gmail.com",
                SmtpPort = 587
            };

            _presenter = new ExperienciaUsuarioPresenter(this, emailConfig);
      
        }

        public ExperienciaUsuarioModel ObtenerExperiencia()
        {
            string satisfaccion = groupBoxSatisfaccion.Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked)?.Text ?? "No especificado";

            List<string> areas = groupBoxMejoras.Controls.OfType<RadioButton>()
                .Where(c => c.Checked).Select(c => c.Text).ToList();

            return new ExperienciaUsuarioModel
            {
                Satisfaccion = satisfaccion,
                Comentarios = richTextBoxComentarios.Text,
                AreasMejora = areas
            };
        }
        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }
    



        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExpandir_Click(object sender, EventArgs e)
        {
            this.WindowState = (this.WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        private void btnMenuPrincipal_Click(object sender, EventArgs e)
        {
            var menuPrincipal = new MenuPrincipal();
            menuPrincipal.Show();
            this.Close();
        }

        private async void btnEnviar_Click_1(object sender, EventArgs e)
        {
            _presenter.EnviarExperiencia();


        }




    }
}