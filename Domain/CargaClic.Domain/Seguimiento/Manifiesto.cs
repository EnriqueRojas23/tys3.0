using System;
using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Seguimiento
{
     public class Manifiesto : Entity
    {
        [Key]
        public long idmanifiesto { get; set; }
        public string nummanifiesto { get; set; }
        public DateTime fecharegistro { get; set; }
        public int? idusuarioregistro { get; set; }
        public long iddespacho { get; set; }
        public bool? activo { get; set; }
        public string numhojaruta { get; set; }
        public int idvehiculo { get; set; }
        public int? idtipooperacion { get; set; }
        public int? iddestino { get; set; }
        public int? idestado {get;set;}
        public int? iddocumentoliq {get;set;}
        public string observacionliq {get;set;}
        public decimal? costoproveedor {get;set;}

        public string nrofactura {get;set;}
        public bool? facturado {get;set;}
    }
}