using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabajofinal_programacion_avanzada_logistica.Data;
using trabajofinal_programacion_avanzada_logistica.Model;
using trabajofinal_programacion_avanzada_logistica.Models;

namespace trabajofinal_programacion_avanzada_logistica.Repository
{

    /// Interfaz que define las operaciones para el acceso y manipulación de datos 
    /// relacionados con gastos en el sistema de logística.
    /// 
    /// SOLID:
    /// - Principio de Segregación de Interfaces (ISP): Define una interfaz específica 
    ///   para operaciones de gastos sin métodos innecesarios.
    /// - Principio de Inversión de Dependencias (DIP): Permite depender de abstracciones 
    ///   (esta interfaz) en lugar de implementaciones concretas.
    /// 
    /// MVP:
    /// - Forma parte del componente Modelo en la arquitectura MVP, específicamente 
    ///   en la capa de acceso a datos (Repository Pattern).
    /// - Proporciona una abstracción para que el Presentador pueda interactuar con 
    ///   el almacenamiento de datos sin conocer los detalles de implementación.
    public interface IGastosRepository
    {


        /// Agrega un nuevo registro de gasto al sistema.
        /// 
        /// SOLID:
        /// - Principio de Responsabilidad Única (SRP): El método tiene la única
        ///   responsabilidad de agregar un gasto al repositorio.
        void AgregarGasto(GastosModel model);

        /// Recupera la lista completa de gastos almacenados en el sistema.
        /// 
        /// SOLID:
        /// - Principio de Responsabilidad Única (SRP): El método tiene la única
        ///   responsabilidad de recuperar los gastos del repositorio.
        List<GastosModel> ObtenerGastos();
    }
}

