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
    public interface IGastosRepository
    {
        
        void AgregarGasto(GastosModel model);
        List<GastosModel> ObtenerGastos();
    }
}

