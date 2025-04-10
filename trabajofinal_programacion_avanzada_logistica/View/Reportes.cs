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
    public partial class Reportes : Form, IReporteView
    {
        private ReportePresenter _presenter;
        public Reportes()
        {
            InitializeComponent();
            _presenter = new ReportePresenter(this, new ReporteService());
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            cmbPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria2.DropDownStyle = ComboBoxStyle.DropDownList;

            ConfigurarDataGridView();

            // Configurar columnas
        }

        private void ConfigurarDataGridView()
        {
            dgvReportes.AutoGenerateColumns = false;
            dgvReportes.Columns.Clear();

            var columnas = new[]
            {
            new { DataProperty = "Fecha", Header = "Fecha", Format = "dd/MM/yyyy", Width = 100 },
            new { DataProperty = "Empleado", Header = "Empleado", Format = "", Width = 150 },
            new { DataProperty = "Categoria", Header = "Categoría", Format = "", Width = 120 },
            new { DataProperty = "Monto", Header = "Monto", Format = "C2", Width = 80 },
            new { DataProperty = "Estado", Header = "Estado", Format = "", Width = 80 }
        };

            foreach (var col in columnas)
            {
                var dataCol = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = col.DataProperty,
                    HeaderText = col.Header,
                    Name = "col" + col.DataProperty,
                    Width = col.Width
                };

                if (!string.IsNullOrEmpty(col.Format))
                {
                    dataCol.DefaultCellStyle.Format = col.Format;
                }

                dgvReportes.Columns.Add(dataCol);
            }
        }

        // Implementación de IReporteView
        public string MesSeleccionado => cmbPeriodo.SelectedItem?.ToString();
        public string CategoriaSeleccionada => cmbCategoria2.SelectedItem?.ToString();


        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "FINCONTROL", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CargarMeses(List<string> meses)
        {
            cmbPeriodo.Items.Clear();
            cmbPeriodo.Items.Add("Todos los meses");
            cmbPeriodo.Items.AddRange(meses.ToArray());
            cmbPeriodo.SelectedIndex = 0;
        }

        public void CargarCategorias(List<string> categorias)
        {
            cmbCategoria2.Items.Clear();
            cmbCategoria2.Items.Add("Todas las categorías");
            cmbCategoria2.Items.AddRange(categorias.ToArray());
            cmbCategoria2.SelectedIndex = 0;
        }

        public void MostrarSolicitudes(IEnumerable<SolicitudReporte> solicitudes)
        {
            dgvReportes.DataSource = solicitudes.ToList();
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

        private void btnMenuPrincipal_Click(object sender, EventArgs e)
        {


            // Cerrar el formulario actual (Registro de Gastos)
            var menuPrincipal = new MenuPrincipal();
            menuPrincipal.Show();
            this.Close();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbPeriodo.SelectedIndex = 0;
            cmbCategoria2.SelectedIndex = 0;
            _presenter.BuscarSolicitudes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _presenter.BuscarSolicitudes();

        }
    }
}
