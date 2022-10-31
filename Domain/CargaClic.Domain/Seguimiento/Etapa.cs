using System;
using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace Webapi.Data.Domain
{
    public class Etapa : Entity
    {
        [Key]
        public long? idetapa { get; set; }
        public int? idmaestroetapa { get; set; }
        public long? idordentrabajo { get; set; }
        public int? idmanifiesto { get; set; }
        public string descripcion { get; set; }
        public string recurso { get; set; }
        public string documento { get; set; }
        public DateTime? fechaetapa { get; set; }
        public DateTime? fecharegistro { get; set; }
        public int? idusuarioregistro { get; set; }
        public bool? visible { get; set; }
        public int? idtipoentrega {get;set;}
    }
}