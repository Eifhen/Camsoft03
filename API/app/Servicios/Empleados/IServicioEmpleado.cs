


using System.Collections.Generic;
using System.Threading.Tasks;
using app.Model;

namespace app.Servicios.Empleados
{

    public interface IServicioEmpleado {
        Task<bool> Add(Empleado data);
        Task<List<Empleado>> GetAll();
        Task<Empleado> Get(int id);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, Empleado data);
    }
    
}