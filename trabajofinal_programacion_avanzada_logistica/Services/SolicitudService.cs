using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.Services
{
    public class SolicitudService : ISolicitudService
    {
        private readonly FinControlDBEntities _context;

        public SolicitudService()
        {
            _context = new FinControlDBEntities();
        }

        public IEnumerable<SolicitudModel> ObtenerTodas()
        {
            return _context.Solicitudes
                .Select(s => new SolicitudModel
                {
                    Id = s.Id,
                    Empleado = s.Empleado,
                    Categoria = s.Categoria,
                    Descripcion = s.Descripcion,
                    Monto = s.Monto,
                    Estado = s.Estado
                }).ToList();
        }

        public void GuardarSolicitud(SolicitudModel solicitud)
        {
            var entity = new Solicitudes
            {
                Empleado = solicitud.Empleado,
                EmpleadoNombre = solicitud.Empleado, // Asegúrate de mapear este campo
                Categoria = solicitud.Categoria,
                Descripcion = solicitud.Descripcion,
                Monto = solicitud.Monto,
                Estado = solicitud.Estado,
                Fecha = DateTime.Now,
                EmpleadoId = solicitud.EmpleadoId,
                CategoriaId = solicitud.CategoriaId,
                UsuarioId = solicitud.UsuarioId,
                GastoId = solicitud.GastoId,
                FechaSolicitud = solicitud.FechaSolicitud
            };

            _context.Solicitudes.Add(entity);
            _context.SaveChanges();
        }

        public string DeterminarEstado(decimal monto)
        {
            if (monto >= 1000 && monto <= 40000) return "Aprobado";
            if (monto > 40000 && monto <= 100000) return "Pendiente";
            if (monto > 100000) return "Rechazado";
            return "Rechazado";
        }
    }
}

    

