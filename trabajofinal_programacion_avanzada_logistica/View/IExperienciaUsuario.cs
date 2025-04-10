using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.View
{

    // Interfaz que pertenece a la Capa Vista (View) dentro de la arquitectura MVP.
    // Define las acciones que debe tener la vista ExperienciaUsuario.
    // El Presenter interactúa con esta interfaz sin conocer la implementación concreta de la Vista
    public interface IExperienciaUsuario
    {

        // Método que debe implementar la Vista para obtener los datos ingresados por el usuario.
        // Este método devuelve un objeto del Modelo (ExperienciaUsuarioModel) con los datos capturados.
        ExperienciaUsuarioModel ObtenerExperiencia();

        // Método que debe implementar la Vista para mostrar un mensaje al usuario.
        // El Presenter llama este método para informar sobre procesos completados o errores.
        void MostrarMensaje(string mensaje);



    }
}
