using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Model;
using trabajofinal_programacion_avanzada_logistica.Models;
using trabajofinal_programacion_avanzada_logistica.View;

namespace trabajofinal_programacion_avanzada_logistica.Repository
{

    /// Clase responsable de manejar las operaciones de acceso a datos relacionadas con los gastos.
    /// Implementa la interfaz IGastosRepository (Principio de Inversión de Dependencias - SOLID).
    public class GastosRepository : IGastosRepository
    {

        // Contexto de la base de datos (Entity Framework
        private readonly FinControlDBEntities _context;

        /// Constructor que inyecta la dependencia del contexto (Principio de Inversión de Dependencias - SOLID).
        public GastosRepository(FinControlDBEntities context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        /// Método para agregar un nuevo gasto a la base de datos.
        /// Este método transforma un objeto GastosModel (Modelo de dominio) a la entidad Gastos (Modelo de datos)
        /// Modelo de gasto recibido desde la capa de presentación.
        public void AgregarGasto(GastosModel model)
        {
            var entity = new Data.Gastos
            {

                // Mapear el modelo recibido a la entidad de base de datos.
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


        /// Método para obtener todos los gastos de la base de datos.
        /// Realiza un mapeo de entidad a modelo de dominio (Principio de Responsabilidad Única - SOLID)
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