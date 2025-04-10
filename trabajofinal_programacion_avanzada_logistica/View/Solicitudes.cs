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
using trabajofinal_programacion_avanzada_logistica.Services;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public partial class Solicitudes : Form, ISolicitudView
    {
        private SolicitudesPresenter _presenter;


        public Solicitudes()
        {
            InitializeComponent();
            dgvResumen.AutoGenerateColumns = true;
            LoadCategories();
            _presenter = new SolicitudesPresenter(this, new SolicitudService(), new CorreoService(new EmailConfig()));
            


        }

        public string Empleado
        {
            get => cmbEmpleado.Text;
            set => cmbEmpleado.Text = value;
        }

        public string Categoria
        {
            get => cmbCategoria.Text;
            set => cmbCategoria.Text = value;
        }

        public string Descripcion
        {
            get => txtDescripcion.Text;
            set => txtDescripcion.Text = value;
        }

        public decimal Monto
        {
            get => decimal.Parse(txtMonto2.Text);
            set => txtMonto2.Text = value.ToString();
        }

        public int SolicitudesPendientes
        {
            get => int.Parse(lblPendientes.Text);
            set => lblPendientes.Text = value.ToString();
        }

        public decimal MontoTotalAprobado
        {
            get => decimal.Parse(lblAprobado.Text);
            set => lblAprobado.Text = value.ToString();
        }

        public int SolicitudesRechazadas
        {
            get => int.Parse(lblRechazadas.Text);
            set => lblRechazadas.Text = value.ToString();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void HabilitarBotonEnviar(bool habilitar)
        {
            if (btnEnviar.InvokeRequired)
            {
                btnEnviar.Invoke(new Action<bool>(HabilitarBotonEnviar), habilitar);
                return;
            }
            btnEnviar.Enabled = habilitar;
        }

        public void MostrarMensajeSeguro(string mensaje)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(MostrarMensajeSeguro), mensaje);
                return;
            }
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ActualizarCursor(Cursor cursor)
        {
            // Verifica si necesitas invocar (si se llama desde otro hilo)
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<Cursor>(ActualizarCursor), cursor);
                return;
            }

            // Actualiza el cursor de forma segura
            this.Cursor = cursor;
        }


        private void LoadCategories()
        {
            cmbCategoria.Items.AddRange(new object[] {
                "Gastos operativos",
                "Gastos de infraestructura",
                "Gastos de tecnología",
                "Gastos de marketing",
                "Gastos administrativos",
                "Gastos de alimentación",
                "Gastos de entretenimiento",
                "Gastos de transporte",
                "Otros"
            });
        }
        private void btnMenuPrincipal_Click(object sender, EventArgs e)
        {

            // Cerrar el formulario actual (Registro de Gastos)
            var menuPrincipal = new MenuPrincipal();
            menuPrincipal.Show();
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

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            await _presenter.EnviarSolicitud();
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrEmpty(Empleado) || string.IsNullOrEmpty(Categoria) ||
                    string.IsNullOrEmpty(Descripcion) || string.IsNullOrEmpty(txtMonto2.Text))
                {
                    MostrarMensaje("Todos los campos son obligatorios");
                    return;
                }

                // Validar que el monto sea un número válido
                if (!decimal.TryParse(txtMonto2.Text, out decimal monto))
                {
                    MostrarMensaje("El monto debe ser un valor numérico válido");
                    return;
                }

                _presenter.GuardarSolicitud();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar: {ex.Message}");
            }
        }

        private void LimpiarCampos()
        {
            Empleado = string.Empty;
            Categoria = string.Empty;
            Descripcion = string.Empty;
            Monto = 0;
        }

        public void ActualizarDataGrid(IEnumerable<SolicitudModel> solicitudes)
        {
            dgvResumen.DataSource = solicitudes.ToList();

            // Opcional: Configurar columnas si es necesario
            if (dgvResumen.Columns.Count == 0)
            {
                dgvResumen.AutoGenerateColumns = true;
            }
        }
    }


    
}
