
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Model
{
    [Table("nomina")]
    public class Nomina {

        public int id_nomina {get;set;}
        public DateTime fecha{get;set;}


        public List<DetalleNomina>? detalle_nomina {get;set;}

    }
    
}