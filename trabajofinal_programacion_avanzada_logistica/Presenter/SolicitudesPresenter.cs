using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Models;
using trabajofinal_programacion_avanzada_logistica.Repository;
using trabajofinal_programacion_avanzada_logistica.Services;
using trabajofinal_programacion_avanzada_logistica.View;

namespace trabajofinal_programacion_avanzada_logistica.Presenter
{
    public class SolicitudesPresenter
    {
        
        private readonly ISolicitudView _view;
        private readonly ISolicitudService _solicitudService;
        private readonly ICorreoService _correoService;

        public SolicitudesPresenter(ISolicitudView view, ISolicitudService solicitudService, ICorreoService correoService)
        {
            _view = view;
            _solicitudService = solicitudService;
            _correoService = correoService;

            // Cargar datos iniciales
            CargarDatos();
        }

        public void CargarDatos()
        {
            try
            {
                var solicitudes = _solicitudService.ObtenerTodas();
                _view.ActualizarDataGrid(solicitudes);

                _view.SolicitudesPendientes = solicitudes.Count(s => s.Estado == "Pendiente");
                _view.MontoTotalAprobado = solicitudes.Where(s => s.Estado == "Aprobado").Sum(s => s.Monto);
                _view.SolicitudesRechazadas = solicitudes.Count(s => s.Estado == "Rechazado");
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar datos: {ex.Message}");
            }
        }

        public void GuardarSolicitud()
        {
            try
            {
                // Validación básica
                if (string.IsNullOrEmpty(_view.Empleado) || string.IsNullOrEmpty(_view.Categoria) ||
                    string.IsNullOrEmpty(_view.Descripcion) || _view.Monto <= 0)
                {
                    _view.MostrarMensaje("Todos los campos son obligatorios y el monto debe ser positivo");
                    return;
                }
                if (string.IsNullOrWhiteSpace(_view.Empleado))
                {
                    _view.MostrarMensaje("Debe ingresar el nombre del empleado.");
                    return;
                }


                System.Diagnostics.Debug.WriteLine("EmpleadoNombre = " + _view.Empleado);


                var solicitud = new SolicitudModel
                {
                    Empleado = _view.Empleado,
                    Categoria = _view.Categoria,
                    Descripcion = _view.Descripcion,
                    Monto = _view.Monto,
                    Estado = _solicitudService.DeterminarEstado(_view.Monto),
                    Fecha = DateTime.Now,
                    UsuarioId = 1,
                    GastoId = 1,
                    EmpleadoId = 1,
                    CategoriaId = 1,
                    FechaSolicitud = DateTime.Now,
                    EmpleadoNombre = _view.Empleado,


                };

                _solicitudService.GuardarSolicitud(solicitud);
                _view.MostrarMensaje("Solicitud guardada correctamente");
                CargarDatos(); // Actualizar la vista con los nuevos datos
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendLine($"Propiedad: {error.PropertyName}, Error: {error.ErrorMessage}");
                    }
                }

                throw new Exception("Error de validación en la entidad:\n" + sb.ToString(), ex);
            }
        }

        public async Task EnviarSolicitud()
        {
            try
            {
                _view.ActualizarCursor(Cursors.WaitCursor);
                _view.HabilitarBotonEnviar(false);

                // Validación básica (puedes mover esto a un método separado si se repite)
                if (string.IsNullOrEmpty(_view.Empleado) || string.IsNullOrEmpty(_view.Categoria) ||
                    string.IsNullOrEmpty(_view.Descripcion) || _view.Monto <= 0)
                {
                    _view.MostrarMensajeSeguro("Todos los campos son obligatorios y el monto debe ser positivo");
                    return;
                }
                var solicitud = new SolicitudModel
                {
                    Empleado = _view.Empleado,
                    Categoria = _view.Categoria,
                    Descripcion = _view.Descripcion,
                    Monto = _view.Monto,
                    Estado = _solicitudService.DeterminarEstado(_view.Monto),
                    Fecha = DateTime.Now,
                    UsuarioId = 1,
                    GastoId = 1,
                    EmpleadoId = 1,
                    CategoriaId = 1,
                    FechaSolicitud = DateTime.Now,
                    EmpleadoNombre = _view.Empleado,

                };

                await Task.Run(() => _solicitudService.GuardarSolicitud(solicitud));
                    await _correoService.EnviarReporteReembolso(solicitud);

                    _view.MostrarMensajeSeguro("Solicitud enviada correctamente");

                    // Actualizar datos (asegúrate de que ActualizarDataGrid sea thread-safe)
                    var solicitudes = await Task.Run(() => _solicitudService.ObtenerTodas());
                    _view.ActualizarDataGrid(solicitudes);
                }
        catch (Exception ex)
            {
                _view.MostrarMensajeSeguro($"Error al enviar: {ex.Message}");
            }
            finally
            {
                _view.ActualizarCursor(Cursors.Default);
                _view.HabilitarBotonEnviar(true);
            }
        }
    }
}