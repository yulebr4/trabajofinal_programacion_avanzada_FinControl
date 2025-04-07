using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Models;
using trabajofinal_programacion_avanzada_logistica.View;

namespace trabajofinal_programacion_avanzada_logistica.Repository
{
    public class GastosRepository : IGastosRepository
    {
        private readonly FinControlDBEntities _context;

        public GastosRepository(FinControlDBEntities context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AgregarGasto(GastosModel model)
        {
            var entity = new Data.Gastos
            {
                Categoria = model.Categoria,
                Descripcion = model.Descripcion,
                Monto = model.Monto,
                FechaRegistro = model.Fecha,
                Comprobante = model.ComprobantePath,
                Empleado = model.Empleado,
                ProyectoId = 1,
                UsuarioId = 1,
            };

            _context.Gastos.Add(entity);
            _context.SaveChanges();
        }

        public List<GastosModel> ObtenerGastos()
        {
            return _context.Gastos
                .Select(x => new GastosModel
                {
                    Id = x.Id,
                    Categoria = x.Categoria,
                    Descripcion = x.Descripcion,
                    Monto = x.Monto,
                    Fecha = (DateTime)x.FechaRegistro,
                    ComprobantePath = x.Comprobante,
                    Empleado = x.Empleado
                })
                .ToList();
        }
    }
}