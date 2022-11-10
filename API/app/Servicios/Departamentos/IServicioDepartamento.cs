

using System.Collections.Generic;
using System.Threading.Tasks;
using app.Model;

namespace app.Servicios.Departamentos
{
    public interface IServicioDepartamento {

        Task<bool> Add(Departamento data);
        Task<List<Departamento>> GetAll();
        Task<Departamento> Get(int id);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, Departamento data);

    }
}