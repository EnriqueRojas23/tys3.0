using System;
using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Seguimiento
{
    public class OrdenRecojo : Entity
    {
        [Key]
        public long idordenrecojo { get; set; }
        public int responsablecomercialid { get; set; }
        public DateTime fechahoracita {get;set;}
        public int? idtipounidad {get;set;}
        public string centroacopio {get;set;} 
        public string observaciones {get;set;}
        public long idordentrabajo {get; set;}

    }
}