using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Models;
using trabajofinal_programacion_avanzada_logistica.Services;
using trabajofinal_programacion_avanzada_logistica.View;

namespace trabajofinal_programacion_avanzada_logistica.Presenter
{
    public class ReportePresenter
    {
        private readonly IReporteView _view;
        private readonly IReporteService _service;

        public ReportePresenter(IReporteView view, IReporteService service)
        {
            _view = view;
            _service = service;
            CargarDatosIniciales();
        }

        private void CargarDatosIniciales()
        {
            try
            {
                var meses = _service.ObtenerMesesConRegistros();
                _view.CargarMeses(meses);

                var categorias = _service.ObtenerCategoriasRegistradas();
                _view.CargarCategorias(categorias);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar datos iniciales: {ex.Message}");
            }
        }

        public void BuscarSolicitudes()
        {
            try
            {
                var solicitudes = _service.ObtenerSolicitudesFiltradas(
                    _view.MesSeleccionado,
                    _view.CategoriaSeleccionada);

                _view.MostrarSolicitudes(solicitudes);

                // Elimina estas líneas si no tienes los controles en Reportes
                // CalcularEstadisticas(solicitudes);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al buscar solicitudes: {ex.Message}");
            }

        }
    }
}