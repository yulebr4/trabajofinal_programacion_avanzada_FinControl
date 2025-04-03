using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.View;

namespace trabajofinal_programacion_avanzada_logistica.Presenter
{
    public class LoginPresenter
    {
        private readonly ILoginView view;

        public LoginPresenter(ILoginView view)
        {
            this.view = view;
        }

        public async Task ValidarCredencialesAsync()
        {
            await Task.Delay(2000);

            using (var db = new FinControlDBEntities())
            {
                var usuario = await Task.Run(() => db.Usuarios
                    .FirstOrDefault(u => u.Nombre == view.Usuario && u.Contraseña == view.Contraseña));
                if (usuario != null)
                {
                    view.MostrarMensaje("Login exitoso");
                    view.CerrarFormulario();

                    // Create the MenuPrincipal form
                    MenuPrincipal menu = new MenuPrincipal();
                    // Ensure menu starts contracted
                    menu.ContraerMenu();
                    // Show the menu
                    menu.Show();
                }
                else
                {
                    view.MostrarMensaje("Usuario o contraseña incorrectos");


                }
            }
        }
    }
}