using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Model;

namespace trabajofinal_programacion_avanzada_logistica.View
{

    /// Interfaz que define el contrato para la vista de Gastos en la arquitectura MVP.
    /// Esta interfaz actúa como el punto de comunicación entre el Presentador y la Vista.
    /// 
    /// Implementa el principio de Segregación de Interfaces (I en SOLID) al definir solo
    /// los métodos y propiedades necesarios para la vista de Gastos.
    /// 
    /// Implementa el principio de Inversión de Dependencias (D en SOLID) al permitir que
    /// el Presentador dependa de esta abstracción y no de una implementación concreta.
    public interface IGastosView
    {

        // Propiedades de solo lectura que permiten al Presentador obtener los datos ingresados por el usuario
        // Cumple con el principio de Responsabilidad Única (S en SOLID) al definir claramente
        // que estas propiedades son para acceder a los datos de la vista

        /// Obtiene la categoría seleccionada del gasto
        string Categoria { get; }

        /// Obtiene la descripción del gasto
        string Descripcion { get; }

        /// Obtiene el monto del gasto
        decimal Monto { get; }

        /// Obtiene la fecha del gasto
        DateTime Fecha { get; }

        /// Obtiene el nombre del empleado asociado al gasto
        string Empleado { get; }

        /// Obtiene o establece la ruta del archivo de comprobante del gasto
        /// Permite lectura y escritura ya que el Presentador debe poder asignar la ruta
        string ComprobantePath { get; set; }


        /// Evento que se dispara cuando se solicita cargar los gastos
        event EventHandler CargarGastos;

        /// Evento que se dispara cuando se solicita guardar un nuevo gasto
        event EventHandler GuardarGasto;

        /// Evento que se dispara cuando se solicita generar un PDF del gasto
        event EventHandler GenerarPdf;


        // Métodos que permiten al Presentador actualizar la Vista o solicitar acciones
        // Cumple con el principio de Responsabilidad Única (S) al definir operaciones específicas

        /// Muestra la lista de gastos en la interfaz de usuario
        void MostrarGastos(List<GastosModel> gastos);

        /// Muestra un mensaje al usuario
        void MostrarMensaje(string mensaje, string titulo);

        /// Limpia los campos del formulario después de guardar un gasto
        void LimpiarFormulario();

        /// Valida que los campos necesarios para generar un PDF estén completos y sean válidos
        bool ValidarCamposParaPDF();

        /// Muestra un mensaje de error específico para la generación de PDF
        void MostrarErrorGeneracionPDF(string mensaje);
    }
}