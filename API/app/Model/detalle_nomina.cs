
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Model
{   
    [Table("detalle_nomina")]
    public class DetalleNomina{
        
        public double salario_neto {get;set;}
        public double salario_bruto {get;set;}

        
        public int id_nomina {get;set;}
        public Nomina? nomina {get;set;}

        public int id_emp {get;set;}
        public Empleado? empleado {get;set;}

    }

}