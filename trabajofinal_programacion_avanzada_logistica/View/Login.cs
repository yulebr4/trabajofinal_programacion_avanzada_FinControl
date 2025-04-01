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
using trabajofinal_programacion_avanzada_logistica.Presenter;


namespace trabajofinal_programacion_avanzada_logistica.View
{
    public partial class Login : Form, ILoginView
    {
        private LoginPresenter presenter;

        public Login()
        {
            InitializeComponent();
            presenter = new LoginPresenter(this);


          

        }
        // Propiedades requeridas por ILoginView
        public string Usuario => txtUsuario.Text;
        public string Contraseña => txtContraseña.Text;

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CerrarFormulario()
        {
            this.Hide(); 
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private const int WM_NCLBUTTONDOWM = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern bool ReleaseCapture();

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1 && e.Y <= this.Height && e.Y >= 0)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_NCLBUTTONDOWM, HT_CAPTION, 0);
                }
            }
        }

        

        private  async void guna2GradientButton1_Click(object sender, EventArgs e)
        {

            guna2GradientButton1.Text = "Cargando...";
            guna2GradientButton1.Enabled = false;

            await presenter.ValidarCredencialesAsync();

            guna2GradientButton1.Text = "Iniciar sesión";
            guna2GradientButton1.Enabled = true;
        }

    }

    
}
        
    

