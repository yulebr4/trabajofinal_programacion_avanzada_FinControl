using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public partial class ExperienciaUsuario : Form, IExperienciaUsuario
    {
        public event EventHandler OnEnviarClicked;
        public ExperienciaUsuario()
        {
            InitializeComponent();
            btnEnviar.Click += (s, e) => OnEnviarClicked?.Invoke(this, EventArgs.Empty);
        }

        // Obtiene el valor del RadioButton seleccionado
        public string Satisfaccion => groupBoxSatisfaccion.Controls
            .OfType<RadioButton>().FirstOrDefault(r => r.Checked)?.Text;

        // Obtiene el comentario escrito
        public string Comentarios => richTextBoxComentarios.Text;

        // Obtiene la lista de CheckBox seleccionados
        public List<string> AreasMejora => groupBoxMejoras.Controls
            .OfType<CheckBox>().Where(c => c.Checked).Select(c => c.Text).ToList();
    



private void btnMenuPrincipal_Click(object sender, EventArgs e)
        {
            
            // Cerrar el formulario actual (Registro de Gastos)
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void btnExpandir_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;

            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

        }
    }
}
