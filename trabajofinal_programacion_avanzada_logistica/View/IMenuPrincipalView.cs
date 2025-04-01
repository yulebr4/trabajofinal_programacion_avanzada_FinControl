using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabajofinal_programacion_avanzada_logistica.View
{
    public interface IMenuPrincipalView
    {
        event EventHandler OnGastosClicked;
        event EventHandler OnSolicitudesClicked;
        event EventHandler OnReportesClicked;
        event EventHandler OnExperienciaUsuarioClicked;
        event EventHandler OnAcercaDeClicked;
        event EventHandler OnLogoutClicked;

        void ShowForm();
        void CloseForm();
        void HideForm();
    }
}
