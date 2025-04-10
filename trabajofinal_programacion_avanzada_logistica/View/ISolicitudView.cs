using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.View
{

    /// Interfaz que define el contrato para la vista de gestión de Solicitudes.
    /// Este contrato es utilizado dentro del patrón MVP (Modelo - Vista - Presentador),
    /// permitiendo la separación de responsabilidades entre la lógica de presentación
    /// (Presentador) y la interfaz de usuario (Vista).
    public interface ISolicitudView
    {
        // Propiedad que representa el nombre o identificador del Empleado.
        string Empleado { get; set; }

        // Propiedad que representa la categoría de la solicitud (ejemplo: Viáticos, Compras, etc).
        string Categoria { get; set; }

        // Propiedad que representa la descripción o detalle de la solicitud.
        string Descripcion { get; set; }

        // Propiedad que representa el monto o valor económico asociado a la solicitud.
        decimal Monto { get; set; }


        // Propiedad que representa la cantidad de solicitudes pendientes.
        int SolicitudesPendientes { get; set; }

        // Propiedad que representa el monto total de las solicitudes aprobadas.
        decimal MontoTotalAprobado { get; set; }

        // Propiedad que representa la cantidad de solicitudes que han sido rechazadas.
        int SolicitudesRechazadas { get; set; }


        // Método que permite habilitar o deshabilitar el botón de enviar solicitud.
        void HabilitarBotonEnviar(bool habilitar);

        // Método que muestra un mensaje de confirmación o seguridad al usuario.
        void MostrarMensajeSeguro(string mensaje);

        // Método que permite actualizar el cursor de la aplicación (ejemplo: mostrar reloj de espera).
        void ActualizarCursor(Cursor cursor);

        // Método que muestra un mensaje de información o alerta al usuario.
        void MostrarMensaje(string mensaje);

        // Método que actualiza el DataGridView o control similar con la lista de solicitudes.
        void ActualizarDataGrid(IEnumerable<SolicitudModel> solicitudes);

    }
}
