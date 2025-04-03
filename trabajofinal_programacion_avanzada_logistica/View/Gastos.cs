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
    public partial class Gastos : Form, IGastosView
    {
        private GastosPresenter presenter;
        public Gastos()
        {
            InitializeComponent();
            presenter = new GastosPresenter(this);
        }

        public string Categoria
        {
            get => comboBoxCategoria.SelectedItem?.ToString() ?? "";
            set => comboBoxCategoria.SelectedItem = value;
        }

        public decimal Monto
        {
            get => decimal.TryParse(maskedTextBoxMonto.Text, out var monto) ? monto : 0;
            set => maskedTextBoxMonto.Text = value.ToString();
        }

        public DateTime Fecha
        {
            get => dateTimePickerFecha.Value;
            set => dateTimePickerFecha.Value = value;
        }

        public string Comprobante
        {
            get => txtComprobante.Text;
            set => txtComprobante.Text = value;
        }

        public int ProyectoId => throw new NotImplementedException();

        public event EventHandler GuardarGasto;
        public event EventHandler AdjuntarComprobante;


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
       
            this.Close();
        }

    

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardarGastos_Click(object sender, EventArgs e)
        {
            GuardarGasto?.Invoke(this, EventArgs.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdjuntarComprobante?.Invoke(this, EventArgs.Empty);

        }
    }
}
