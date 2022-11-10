
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using app.Model;
using Microsoft.EntityFrameworkCore;

namespace app.Servicios.Departamentos
{

    public class ServicioDepartamento : IServicioDepartamento
    {

        private BDContext BD;
        
        public ServicioDepartamento(BDContext context){
            BD = context;
        }

        public async Task<bool> Add(Departamento data)
        {
            using(BD){
                await BD.Departamentos.AddAsync(data);
                if(await BD.SaveChangesAsync() > 0){
                    return true;
                }
                return false;
            }
        }
       
        public async Task<Departamento> Get(int id)
        {
            using(BD){
                var data = await BD.Departamentos.FirstOrDefaultAsync(d => d.id_dep == id);
                return data;
            }
        }

        public async Task<List<Departamento>> GetAll()
        {
            using(BD){
                var data = await BD.Departamentos.ToListAsync();
                return data;
            }
        }

        public async Task<bool> Update(int id, Departamento data)
        {
            using(BD){
                var find = await BD.Departamentos.FirstOrDefaultAsync(d => d.id_dep == id);
                if(find != null){
                    BD.Entry(find).State = EntityState.Modified;
                    find.nombre = data.nombre;
                    if(await BD.SaveChangesAsync() > 0){
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using(BD){
                var data = await BD.Departamentos.FirstOrDefaultAsync(d=>d.id_dep == id);
                if(data != null){
                    var empleados = await BD.Empleados.Where(e => e.id_dep == id).ToListAsync();
                    if(empleados.Count > 0){    
                        foreach(var emp in empleados){
                            BD.Entry(emp).State = EntityState.Modified;
                            emp.id_dep = null;
                            emp.departamento = null;
                        }
                    }
                    BD.Departamentos.Remove(data);
                    if(await BD.SaveChangesAsync() > 0){
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }


    }
}