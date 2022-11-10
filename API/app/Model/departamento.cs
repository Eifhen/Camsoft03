
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Model
{
    [Table("departamentos")]
    public class Departamento {

        public int id_dep {get;set;}
        public string? nombre {get;set;}

        public List<Empleado>? empleados {get;set;}

    }
}