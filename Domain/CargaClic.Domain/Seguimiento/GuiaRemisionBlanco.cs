using System;
using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Seguimiento
{
    public class GuiaRemisionBlanco : Entity
    {
        [Key]
        public long id { get; set; }
        public long idmanifiesto { get; set; }
        public int idvehiculo { get; set; }
        public DateTime fecharegistro {get;set;}
        public string numeroguia {get;set;} 
        public int idestado {get;set;}
        public long? idordentrabajo {get;set;}

    }
}