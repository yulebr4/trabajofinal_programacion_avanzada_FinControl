using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Model;
using trabajofinal_programacion_avanzada_logistica.Presenter;
using trabajofinal_programacion_avanzada_logistica.Repository;
using trabajofinal_programacion_avanzada_logistica.Services;


namespace trabajofinal_programacion_avanzada_logistica.View
{

    // Esta clase implementa la vista de la arquitectura MVP
    // Implementa la interfaz IGastosView para cumplir con el principio de Inversión de Dependencias (D en SOLID)
    public partial class Gastos : Form, IGastosView
    {

        // Eventos que serán manejados por el presentador, cumpliendo con el patrón Observer
        // Estos eventos permiten la comunicación desde la Vista hacia el Presentador
        public event EventHandler CargarGastos;
        public event EventHandler GuardarGasto;
        public event EventHandler GenerarPdf;


        // Referencia al presentador que gestionará la lógica de negocio
        private GastosPresenter _presenter;


        // Constructor de la clase
        public Gastos()
        {
            // Constructor de la clase
            InitializeComponent();

            // Configura el presentador
            InitializePresenter();

            // Configura la tabla de datos
            ConfigureDataGridView();

            // Carga las categorías disponibles
            LoadCategories();
        }


        // Método que inicializa el presentador con sus dependencias
        // Aplicación del principio de Inyección de Dependencias
        private void InitializePresenter()
        {

            // Crea la conexión a la base de datos
            var context = new FinControlDBEntities();

            // Crea el repositorio de datos
            var repository = new GastosRepository(context);

            // Crea el servicio para generar PDFs
            var pdfService = new PdfService();


            // Crea el presentador y le pasa la vista (this) y las dependencias necesarias
            _presenter = new GastosPresenter(this, repository, pdfService);
        }


        // Configura las columnas del DataGridView para mostrar los gastos
        private void ConfigureDataGridView()
        {
            dataGridViewGastos.AutoGenerateColumns = false;
            dataGridViewGastos.Columns.Add("Categoria", "Categoría");
            dataGridViewGastos.Columns.Add("Descripcion", "Descripción");
            dataGridViewGastos.Columns.Add("Monto", "Monto");
            dataGridViewGastos.Columns.Add("Fecha", "Fecha");
            dataGridViewGastos.Columns.Add("Empleado", "Empleado");
        }


        // Carga las categorías de gastos en el combobox
        private void LoadCategories()
        {
            comboBoxCategoria.Items.AddRange(new object[] {
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

        #region IGastosView Implementation
        public string Categoria => comboBoxCategoria.SelectedItem?.ToString();
        public string Descripcion => txtDescripcion.Text;
        public decimal Monto => decimal.TryParse(maskedTextBoxMonto.Text, out var m) ? m : 0;
        public DateTime Fecha => dateTimePickerFecha.Value;
        public string Empleado => comboBoxEmpleado.Text;
        public string ComprobantePath { get; set; }

        public void MostrarGastos(List<GastosModel> gastos)
        {
            dataGridViewGastos.Rows.Clear();
            foreach (var gasto in gastos)
            {
                dataGridViewGastos.Rows.Add(
                    gasto.Categoria,
                    gasto.Descripcion,
                    gasto.Monto.ToString("C"),
                    gasto.Fecha.ToShortDateString(),
                    gasto.Empleado
                );
            }
        }

        public void MostrarMensaje(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Event Handlers
        private void Gastos_Load(object sender, EventArgs e)
        {
            CargarGastos?.Invoke(this, EventArgs.Empty);
        }

        private void btnGuardarGastos_Click_1(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                GuardarGasto?.Invoke(this, EventArgs.Empty);
                
            }
        }

        public void LimpiarFormulario()
        {
            comboBoxCategoria.SelectedIndex = -1;
            maskedTextBoxMonto.Clear();
            txtDescripcion.Clear();
            dateTimePickerFecha.Value = DateTime.Today;
            comboBoxEmpleado.SelectedIndex = -1;
            comboBoxCategoria.Focus();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            GenerarPdf?.Invoke(this, EventArgs.Empty);
        }

        private void btnMenuPrincipal_Click(object sender, EventArgs e)
        {
            var menuPrincipal = new MenuPrincipal();
            menuPrincipal.Show();
            this.Close();
        }
        #endregion

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(Categoria))
            {
                MessageBox.Show("Seleccione una categoría", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(Empleado))
            {
                MessageBox.Show("Ingrese el nombre del empleado", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Otros métodos de eventos del formulario (minimizar, cerrar, etc.)
        private void btnCerrar_Click(object sender, EventArgs e) => Application.Exit();
        private void btnExpandir_Click(object sender, EventArgs e) => WindowState = WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
        private void btnMinimizar_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    

    public bool ValidarCamposParaPDF()
        {
            if (string.IsNullOrEmpty(comboBoxCategoria.Text))
            {
                MostrarErrorGeneracionPDF("Seleccione una categoría");
                comboBoxCategoria.Focus();
                return false;
            }

            if (!decimal.TryParse(maskedTextBoxMonto.Text, out decimal monto) || monto <= 0)
            {
                MostrarErrorGeneracionPDF("Ingrese un monto válido");
                maskedTextBoxMonto.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MostrarErrorGeneracionPDF("Ingrese una descripción");
                txtDescripcion.Focus();
                return false;
            }

            return true;
        }

        public void MostrarErrorGeneracionPDF(string mensaje)
        {
            MessageBox.Show(mensaje, "Error al generar PDF",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}