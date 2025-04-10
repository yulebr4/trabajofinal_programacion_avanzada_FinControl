using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Repository;
using trabajofinal_programacion_avanzada_logistica.Services;
using trabajofinal_programacion_avanzada_logistica.View;
using trabajofinal_programacion_avanzada_logistica.Model;
using trabajofinal_programacion_avanzada_logistica.Presenter;

namespace trabajofinal_programacion_avanzada_logistica.Presenter
{

    /// Clase Presenter que actúa como intermediario entre la Vista (IGastosView) y el Repositorio (IGastosRepository).
    /// Gestiona la lógica de presentación y coordina las operaciones del módulo de gastos.
    /// Implementa correctamente el patrón MVP (Modelo-Vista-Presentador).
    public class GastosPresenter
    {

        // Referencia a la vista de gastos (Interfaz)
        private readonly IGastosView _view;

        // Referencia al repositorio de gastos (Interfaz)
        private readonly IGastosRepository _repository;

        // Referencia al servicio encargado de generar el PDF (Interfaz)
        private readonly IPdfService _pdfService;


        /// Constructor que recibe las dependencias necesarias inyectadas (Principio de Inversión de Dependencias - SOLID).
        public GastosPresenter(IGastosView view, IGastosRepository repository, IPdfService pdfService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));


            // Suscripción de eventos de la vista a métodos manejadores del presentador
            _view.CargarGastos += OnCargarGastos;
            _view.GuardarGasto += OnGuardarGasto;
            _view.GenerarPdf += OnGenerarPdf;
        }


        /// Evento que carga y muestra la lista de gastos desde el repositorio.
        private void OnCargarGastos(object sender, EventArgs e)
        {
            try
            {
                var gastos = _repository.ObtenerGastos();
                _view.MostrarGastos(gastos);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar gastos: {ex.Message}", "Error");
            }
        }


        /// Evento encargado de guardar un nuevo gasto.
        /// Recoge datos de la vista y los almacena usando el repositorio.
        private void OnGuardarGasto(object sender, EventArgs e)
        {
            try
            {
                var gasto = new GastosModel
                {
                    Categoria = _view.Categoria,
                    Descripcion = _view.Descripcion,
                    Monto = _view.Monto,
                    Fecha = _view.Fecha,
                    Empleado = _view.Empleado,
                    ComprobantePath = _view.ComprobantePath
                };

                _repository.AgregarGasto(gasto); // Guardar en BD
                _view.LimpiarFormulario();
                _view.MostrarMensaje("Gasto guardado correctamente", "Éxito");
                OnCargarGastos(sender, e);  // Recargar lista de gastos
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al guardar gasto: {ex.Message}", "Error");
            }
        }


        /// Evento que genera un archivo PDF con los datos del gasto.
        /// Valida previamente los datos.
        private void OnGenerarPdf(object sender, EventArgs e)
        {
            try
            {
                // Validación centralizada
                var errorValidacion = ValidarDatosParaPDF();
                if (!string.IsNullOrEmpty(errorValidacion))
                {
                    _view.MostrarMensaje(errorValidacion, "Error");
                    return;
                }
                var gasto = new GastosModel
                {
                    Categoria = _view.Categoria,
                    Descripcion = _view.Descripcion,
                    Monto = _view.Monto,
                    Fecha = _view.Fecha,
                    Empleado = _view.Empleado
                };

                _view.ComprobantePath = _pdfService.GenerarComprobantePdf(gasto); // Llamada al servicio PDF
                _view.MostrarMensaje($"PDF generado en: {_view.ComprobantePath}", "Éxito");
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al generar PDF: {ex.Message}", "Error");
            }
        }

        private string ValidarDatosParaPDF()
        {
            if (string.IsNullOrEmpty(_view.Categoria))
                return "Seleccione una categoría";

            if (_view.Monto <= 0)
                return "Ingrese un monto válido";

            if (string.IsNullOrEmpty(_view.Descripcion))
                return "Ingrese una descripción";

            return string.Empty;
        }
    }
}