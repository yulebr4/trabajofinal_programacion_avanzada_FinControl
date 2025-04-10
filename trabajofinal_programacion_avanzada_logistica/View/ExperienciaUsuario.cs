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

    // Vista (View) de la arquitectura MVP, encargada de la interfaz gráfica
    public partial class ExperienciaUsuario : Form, IExperienciaUsuario
    {

        // Referencia al Presenter que gestiona la lógica de negocio
        private readonly ExperienciaUsuarioPresenter _presenter;


        // Constructor de la Vista
        public ExperienciaUsuario()
        {
            InitializeComponent();
            // Configuración del servicio de email


            // Configuración para el envío de correos
            var emailConfig = new EmailConfig
            {
                Username = "fincontrol521@gmail.com",
                Password = "ixfq rozy dkxx lway",
                FromEmail = "fincontrol521@gmail.com",
                ToEmail = "fincontrol521@gmail.com",
                SmtpServer = "smtp.gmail.com",
                SmtpPort = 587
            };

            // Instanciamos el presenter pasándole la vista (this) y la configuración de email
            _presenter = new ExperienciaUsuarioPresenter(this, emailConfig);
      
        }


        // Método para obtener los datos de experiencia ingresados por el usuario
        public ExperienciaUsuarioModel ObtenerExperiencia()
        {

            // Obtener nivel de satisfacción seleccionado en los RadioButton
            string satisfaccion = groupBoxSatisfaccion.Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked)?.Text ?? "No especificado";


            // Obtener las áreas de mejora seleccionadas
            List<string> areas = groupBoxMejoras.Controls.OfType<RadioButton>()
                .Where(c => c.Checked).Select(c => c.Text).ToList();


            // Retornamos un objeto del modelo con los datos capturados
            return new ExperienciaUsuarioModel
            {
                Satisfaccion = satisfaccion,
                Comentarios = richTextBoxComentarios.Text,
                AreasMejora = areas
            };
        }

        // Método para mostrar mensajes en pantalla
        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }



        // Evento para cerrar la aplicación
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        // Evento para minimizar la ventana
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        // Evento para maximizar o restaurar la ventana
        private void btnExpandir_Click(object sender, EventArgs e)
        {
            this.WindowState = (this.WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal;
        }


        // Evento para volver al menú principal
        private void btnMenuPrincipal_Click(object sender, EventArgs e)
        {
            var menuPrincipal = new MenuPrincipal();
            menuPrincipal.Show();
            this.Close();
        }


        // Evento para enviar la experiencia capturada al presenter (lógica de negocio)
        private async void btnEnviar_Click_1(object sender, EventArgs e)
        {
            _presenter.EnviarExperiencia();


        }




    }
}