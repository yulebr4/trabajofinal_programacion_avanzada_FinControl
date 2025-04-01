using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.view.OnGastosClicked += (s, e) => new Gastos().Show();
            this.view.OnSolicitudesClicked += (s, e) => new Solicitudes().Show();
            this.view.OnReportesClicked += (s, e) => new Reportes().Show();
            this.view.OnExperienciaUsuarioClicked += (s, e) => new ExperienciaUsuario().Show();
            this.view.OnAcercaDeClicked += (s, e) => new AcercaDe().Show();
            this.view.OnLogoutClicked += (s, e) =>
            {
                this.view.HideForm();
                new Login().Show();
            };
        }
    }
}
