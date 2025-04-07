using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Presenter;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public partial class ExperienciaUsuario : Form, IExperienciaUsuario
    {
        
        public event EventHandler OnEnviarClicked;

        public ExperienciaUsuario()
        {
            InitializeComponent();
           

        }

        public string Satisfaccion => groupBoxSatisfaccion.Controls
            .OfType<RadioButton>().FirstOrDefault(r => r.Checked)?.Text ?? "No especificado";

        public string Comentarios => richTextBoxComentarios.Text;

        public List<string> AreasMejora => groupBoxMejoras.Controls
            .OfType<CheckBox>().Where(c => c.Checked).Select(c => c.Text).ToList();

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnEnviar_Click_1(object sender, EventArgs e)
        {
            OnEnviarClicked?.Invoke(this, EventArgs.Empty);
            MostrarMensaje("Gracias por su opinión");
        }


    }
}