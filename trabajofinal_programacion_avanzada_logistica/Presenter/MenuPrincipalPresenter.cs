using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.View;

namespace trabajofinal_programacion_avanzada_logistica.Presenter
{
    public class MenuPrincipalPresenter
    {
        private readonly IMenuPrincipalView view;

        public MenuPrincipalPresenter(IMenuPrincipalView view)
        {
            this.view = view;

            // Suscribirse a los eventos de la vista
            this.view.OnGastosClicked += (s, e) => AbrirFormulario(new Gastos());
            this.view.OnSolicitudesClicked += (s, e) => AbrirFormulario(new Solicitudes());
            this.view.OnReportesClicked += (s, e) => AbrirFormulario(new Reportes());
            this.view.OnExperienciaUsuarioClicked += (s, e) => AbrirFormulario(new ExperienciaUsuario());
            this.view.OnAcercaDeClicked += (s, e) => AbrirFormulario(new AcercaDe());
            this.view.OnLogoutClicked += (s, e) =>
            {
                this.view.HideForm();
                new Login().Show();
            };
        }

        private void AbrirFormulario(Form formulario)
        {
            // Ocultar el menú principal antes de abrir el nuevo formulario
            view.HideForm();

            // Mostrar el nuevo formulario
            formulario.ShowDialog();

            // Una vez que el usuario cierre el formulario, volvemos a mostrar el menú principal
            view.ShowForm();
        }
    }
}
