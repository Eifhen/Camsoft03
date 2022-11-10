

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Context;
using app.Model;
using Microsoft.EntityFrameworkCore;

namespace app.Servicios.Nominas
{

    public class ServicioNominaDetalle : IServicioNominaDetalle{

        private BDContext BD;
        public ServicioNominaDetalle(BDContext context){
            BD = context;
        }

        public async Task<List<DetalleNomina>> GetAll(int id_nomina)
        {
            using(BD){
                var detalle = await BD.DetalleNomina.Where(d => d.id_nomina == id_nomina).ToListAsync();
                return detalle;
            }
        }
   
       
    }
}
