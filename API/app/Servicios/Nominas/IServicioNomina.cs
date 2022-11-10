
using System.Collections.Generic;
using System.Threading.Tasks;
using app.Model;

namespace app.Servicios.Nominas
{

    public interface IServicioNomina {
        Task<bool> Add(Nomina data);
        Task<List<Nomina>> GetAll();
        Task<Nomina> Get(int id);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, Nomina data);
    }
    
}