

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using app.Model;
using Microsoft.EntityFrameworkCore;

namespace app.Servicios.Empleados
{

    public class ServicioEmpleado: IServicioEmpleado{

        private BDContext BD;
        public ServicioEmpleado(BDContext context){
            BD = context;
        }

        public async Task<bool> Add(Empleado data)
        {   
            if(data.fecha_ingreso <= DateTime.Now){
                using(BD){

                    var empleado = await BD.Empleados.AddAsync(data);
                    var departamento = await BD.Departamentos.FirstOrDefaultAsync(d => d.id_dep == data.id_dep);
                    if(departamento != null)
                    {
                        if(BD.SaveChanges() > 0){
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public async Task<bool> Delete(int id_empleado)
        {
            using(BD){
                var find = await BD.Empleados.FirstOrDefaultAsync(e => e.id == id_empleado);
                if(find != null){
                    var detallesNominaEmpleado = await BD.DetalleNomina.Where(d => d.id_emp == id_empleado).ToListAsync();
                    BD.DetalleNomina.RemoveRange(detallesNominaEmpleado);
                    BD.Empleados.Remove(find);
                    if(await BD.SaveChangesAsync() > 0){
                        return true;
                    }
                }
                return false;
            }
        }

        public async Task<Empleado> Get(int id_empleado)
        {
            using(BD){
                var find = await BD.Empleados.FirstOrDefaultAsync(e => e.id == id_empleado);
                return find;
            }
        }

        public async Task<List<Empleado>> GetAll()
        {
            using(BD){
                var data = await BD.Empleados.ToListAsync();
                return data;
            }
        }

        public async Task<bool> Update(int id_empleado, Empleado data)
        {   
            if(data.fecha_ingreso <= DateTime.Now){
                using(BD){
                    var empleado = await BD.Empleados.FirstOrDefaultAsync(e=>e.id == id_empleado);
                    var departamento = await BD.Departamentos.FirstOrDefaultAsync(d=>d.id_dep == data.id_dep);
            
                    if(empleado != null && departamento != null){
                        BD.Entry(empleado).State = EntityState.Modified;
                        empleado.nombre = data.nombre;
                        empleado.fecha_ingreso = data.fecha_ingreso;
                        empleado.salario = data.salario;
                        empleado.id_dep = data.id_dep;
                        empleado.departamento = departamento;

                        if(await BD.SaveChangesAsync() > 0){
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    } 
}