
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Model
{   
    [Table("empleados")]
    public class Empleado {
        
        public int id {get;set;} 
        public string? nombre {get;set;}
        public DateTime fecha_ingreso {get;set;}
        public double salario {get;set;}

        public int? id_dep {get;set;}
        public Departamento? departamento {get;set;}

        public List<DetalleNomina>? detalle_nomina {get;set;}

    }
}