using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.Services
{
    public class ReporteService : IReporteService
    {
        private readonly FinControlDBEntities _context;

        public ReporteService()
        {
            _context = new FinControlDBEntities();
        }

        public List<string> ObtenerMesesConRegistros()
        {
            try
            {
                return _context.Solicitudes
                    .Where(s => s.Fecha.HasValue)  // Filtramos solo fechas que tienen valor
                    .Select(s => s.Fecha.Value)    // Accedemos al valor DateTime
                    .ToList()                      // Cambiamos a LINQ to Objects
                    .Select(f => new DateTime(f.Year, f.Month, 1))  // Ahora f es DateTime, no DateTime?
                    .Distinct()
                    .OrderByDescending(f => f)
                    .Select(f => f.ToString("MMMM yyyy"))
                    .ToList();
            }
            catch (Exception ex)
            {
                // Manejar error o registrar
                return new List<string> { DateTime.Now.ToString("MMMM yyyy") };
            }
        }

        public List<string> ObtenerCategoriasRegistradas()
        {
            try
            {
                // Obtener categorías únicas de las solicitudes
                return _context.Solicitudes
                    .Where(s => s.Categoria != null)
                    .Select(s => s.Categoria)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();
            }
            catch (Exception ex)
            {
                // Manejar error o registrar
                return new List<string> { "General" };
            }
        }

        public IEnumerable<SolicitudReporte> ObtenerSolicitudesFiltradas(string mes, string categoria)
        {
            var query = _context.Solicitudes.AsQueryable();

            // Filtrar por mes si está seleccionado
            if (!string.IsNullOrEmpty(mes) && mes != "Todos los meses")
            {
                var fecha = DateTime.ParseExact(mes, "MMMM yyyy", System.Globalization.CultureInfo.CurrentCulture);
                query = query.Where(s =>
                    s.Fecha.HasValue &&
                    s.Fecha.Value.Year == fecha.Year &&
                    s.Fecha.Value.Month == fecha.Month);

            }

            // Filtrar por categoría si está seleccionada
            if (!string.IsNullOrEmpty(categoria) && categoria != "Todas las categorías")
            {
                query = query.Where(s => s.Categoria == categoria);
            }


            return query
                .OrderByDescending(s => s.Fecha)
                .ToList()
                .Select(s => new SolicitudReporte
                {
                    Fecha = (DateTime)s.Fecha,
                    Empleado = s.Empleado ?? "Sin empleado",
                    Categoria = s.Categoria ?? "Sin categoría",
                    Monto = s.Monto,
                    Estado = s.Estado ?? "Pendiente"
                });
        }
    }
}