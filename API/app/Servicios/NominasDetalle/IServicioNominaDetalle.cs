using System.Collections.Generic;
using System.Threading.Tasks;
using app.Context;
using app.Model;

namespace app.Servicios.Nominas
{

    public interface IServicioNominaDetalle {
        //Task<bool> Generate(int id_nomina, BDContext BD);
        Task<List<DetalleNomina>> GetAll(int id_nomina);

    }
    
}