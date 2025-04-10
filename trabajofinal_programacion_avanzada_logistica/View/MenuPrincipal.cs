using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Presenter;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public partial class MenuPrincipal : Form, IMenuPrincipalView
    {


        private MenuPrincipalPresenter presenter;

        public event EventHandler OnGastosClicked;
        public event EventHandler OnSolicitudesClicked;
        public event EventHandler OnReportesClicked;
        public event EventHandler OnExperienciaUsuarioClicked;
        public event EventHandler OnAcercaDeClicked;
        public event EventHandler OnLogoutClicked;

        //1.2
        //Campos
        private int borderSize = 2;

        //Constructor
        public MenuPrincipal()
        {
            InitializeComponent();
            ContraerMenu();


            //Tamaño de los bordes
            this.Padding = new Padding(borderSize);

            //Color de los bordes
            this.BackColor = Color.FromArgb(255, 255, 255);

            // Inicializar Presentador
            presenter = new MenuPrincipalPresenter(this);
        }

        public void ShowForm() => this.HideForm();
        public void CloseForm() => this.Close();
        public void HideForm() => this.Hide();

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OnGastosClicked?.Invoke(this, EventArgs.Empty);
            
      
        }



        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            OnSolicitudesClicked?.Invoke(this, EventArgs.Empty);
       
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            OnReportesClicked?.Invoke(this, EventArgs.Empty);
            
        }
        private void btnExpeUsuario_Click(object sender, EventArgs e)
        {
            OnExperienciaUsuarioClicked?.Invoke(this, EventArgs.Empty);
          
        }

        
        private void btnAcercaDe_Click(object sender, EventArgs e)
        {
            OnAcercaDeClicked?.Invoke(this, EventArgs.Empty);
            
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            OnLogoutClicked?.Invoke(this, EventArgs.Empty);
            this.Hide();

            // Create and show a new login form
            Login loginForm = new Login();
            loginForm.Show();
        }

        private Size formSize;
        private void Form1_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
        }

        //Permitir arrastrar el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWND, int Msg, int wParam, int lParam);

        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        protected override void WndProc(ref Message m)
        {
            //Barra de título estándar - Ajuste de ventana
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_SYSCOMMAND = 0x0112;

            //Minimiza el formulario (como estaba antes)
            const int SC_MINIMIZE = 0xF020;

            //Restaura el formulario (como estaba antes)
            const int SC_RESTORE = 0xF120;

            //Win32,  Notifica la entrada del mouse: permite redimensionar la ventana
            const int WM_NCHITTEST = 0x0084;
            const int resizeAreaSize = 10;


            #region Form Resize

            //Valores de redimensionamiento WM_NCHITTEST

            //Representa el área cliente de la ventana
            const int HTCLIENT = 1;

            //Borde izquierdo de la ventana, permite redimensionar horizontalmente hacia la izquierda
            const int HTLEFT = 10;

            //Borde derecho de la ventana, permite redimensionar horizontalmente hacia la derecha
            const int HTRIGHT = 11;

            //Borde superior, permite redimensionar verticalmente hacia arriba
            const int HTTOP = 12;

            //Esquina superior izquierda, permite redimensionar en diagonal hacia la izquierda
            const int HTTOPLEFT = 13;

            //Esquina superior derecha, permite redimensionar en diagonal hacia la derecha
            const int HTTOPRIGHT = 14;

            //Borde inferior, permite redimensionar verticalmente hacia abajo
            const int HTBOTTOM = 15;

            //Esquina inferior izquierda, permite redimensionar en diagonal hacia la izquierda
            const int HTBOTTOMLEFT = 16;

            //Esquina inferior derecha, permite redimensionar en diagonal hacia la derecha
            const int HTBOTTOMRIGHT = 17;
        
            if (m.Msg == WM_NCHITTEST)
            { //Aqui estamos haciendo una condicional, esta nos dice que si m es igual a WM_NCHITTEST
                base.WndProc(ref m);

                //Redimensiona el formulario si está en estado normal
                if (this.WindowState == FormWindowState.Normal)
                {
                    //Si el resultado de m (puntero del mouse) está en el área cliente de la ventana
                    if ((int)m.Result == HTCLIENT)
                    {

                        // Obtiene las coordenadas del cursor en pantalla
                        Point screenPoint = new Point(m.LParam.ToInt32());

                        // Convierte la ubicación a coordenadas del cliente
                        Point clientPoint = this.PointToClient(screenPoint);


                        // Si el puntero está en la parte superior del formulario(dentro del área de cambio de tamaño - coordenada X)
                        if (clientPoint.Y <= resizeAreaSize)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion
            //Remove border and keep snap window
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
            if (m.Msg == WM_SYSCOMMAND)
            {
             
                /// Quote:
                /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                /// are used internally by the system.To obtain the correct result when testing 
                /// the value of wParam, an application must combine the value 0xFFF0 with the 
                /// wParam value by using the bitwise AND operator.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)  //Before
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }



        //Evento del metodo
        private void MenuPrincipal_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        //Metodo privado
        private void AdjustForm()
        {

            //Vamos a crear una seleccion de casos para el estado de la ventana
            switch (this.WindowState)
            {

                //Si el estado de ventana del formulario esta maximizado simplemente añadimos un relleno
                //de 8x8 en la parte izquierda arriba y derecha
                case FormWindowState.Minimized:
                    this.Padding = new Padding(0, 8, 8, 0);
                    break;

                //Y si el estado de ventana del formulario es normal volvemos a establecer el tamaño del
                // borde como relleno
                case FormWindowState.Normal:

                    //Agregamos una condicion limitador para que el relleno se aplique solo una vez ya que al
                    //caso contrario se ejecutara cada vez que el tamaño o ubicacion del formulario cambio
                    //!= estos signos significan desigualdad o diferente de
                    if (this.Padding.Top != borderSize)
                    this.Padding = new Padding(borderSize);
                    break;
                   
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void btnMazimizar_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;

            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }



        //Evento ContraerMenu
        private void btnMenuM_Click(object sender, EventArgs e)
        {
        
            // Toggle menu state - if contracted, expand it; if expanded, contract it
            if (panelMenu.Width == 100)
            {
                // Expand menu
                panelMenu.Width = 230;
                pictureBox1.Visible = true;
                btnMenuM.Dock = DockStyle.None;

                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "   " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
            else
            {
                // Contract menu
                panelMenu.Width = 100;
                pictureBox1.Visible = false;
                btnMenuM.Dock = DockStyle.Top;

                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
        }
        

        public void ContraerMenu()
        {

                // Force menu to contracted state
                panelMenu.Width = 100;
                pictureBox1.Visible = false;
                btnMenuM.Dock = DockStyle.Top;

                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
     
                }
            }
        public void ExpandirMenu()
        {
            panelMenu.Width = 230;
            pictureBox1.Visible = true;
            btnMenuM.Dock = DockStyle.None;

            foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
            {
                menuButton.Text = "   " + menuButton.Tag.ToString();
                menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                menuButton.Padding = new Padding(10, 0, 0, 0);
            }
        }

    }
}






