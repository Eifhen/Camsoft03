
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using app.Model;
using Microsoft.EntityFrameworkCore;

namespace app.Servicios.Nominas
{

    public class ServicioNomina: IServicioNomina{

        private BDContext BD;
        private double DESCUENTO = 0.9;
        private readonly IServicioNominaDetalle _servicioNominaDetalle;

        public ServicioNomina(BDContext context, IServicioNominaDetalle servicioNominaDetalle){
            BD = context;
            _servicioNominaDetalle = servicioNominaDetalle;
        }

        public async Task<Nomina> Get(int id)
        {
            using(BD){
                var find = await BD.Nomina.FirstOrDefaultAsync(e => e.id_nomina == id);
                return find;
            }
        }

        public async Task<List<Nomina>> GetAll()
        {
            using(BD){
                var data = await BD.Nomina.ToListAsync();
                return data;
            }
        }
        
        public async Task<bool> Add(Nomina nomina)
        {   
            using(BD){
                var empleados = await BD.Empleados.ToListAsync();
                var detalle_nomina = new List<DetalleNomina>();

                empleados.ForEach( empleado => {
                    var detalle = new DetalleNomina();
                    detalle.nomina = nomina;
                    detalle.empleado = empleado;
                    detalle.id_emp = empleado.id;
                    detalle.id_nomina = nomina.id_nomina;
                    detalle.salario_bruto = empleado.salario;
                    detalle.salario_neto = (empleado.salario * DESCUENTO);
                    detalle_nomina.Add(detalle);
                });

                await BD.DetalleNomina.AddRangeAsync(detalle_nomina);
                if(await BD.SaveChangesAsync() > 0){
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using(BD){
                var find = await BD.Nomina.FirstOrDefaultAsync(e => e.id_nomina == id);
                if(find != null){
                    
                    var detalle = await BD.DetalleNomina.Where(d=>d.id_nomina == find.id_nomina).ToListAsync();
                    BD.DetalleNomina.RemoveRange(detalle);
                    BD.Nomina.Remove(find);
                    
                    if(await BD.SaveChangesAsync() > 0){
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }

        public async Task<bool> Update(int id, Nomina data)
        {   
            if(data.fecha <= DateTime.Now){        
                using(BD){
                    var nomina = await BD.Nomina.FirstOrDefaultAsync(n => n.id_nomina == id);
                    if(nomina != null){
                        BD.Entry(nomina).State = EntityState.Modified;
                        nomina.fecha = data.fecha;
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