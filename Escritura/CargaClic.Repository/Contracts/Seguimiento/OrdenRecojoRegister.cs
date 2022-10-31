using System;
using System.ComponentModel.DataAnnotations;

namespace CargaClic.Repository
{
    public class OrdenRecojoDelete
    {
        public long? idordentrabajo { get; set; }
    }

    public class OrdenRecojoRegister
    {
        public long? idordenrecojo { get; set; }
        [Required]
        public int? idcliente {get;set;}
        public DateTime fechahoraregistro {get;set;}
        public string  dnipersonarecojo {get;set;}
        public string personarecojo {get;set;}
        public string puntopartida {get;set;}
        public decimal? peso {get;set;}
        public int? bulto {get;set;}
        public decimal? volumen {get;set;}

        [Required]
        public int? responsablecomercialid { get; set; }
        public int? idtipounidad {get;set;}
        public string centroacopio {get;set;} 

        [Required]
        public DateTime fechacita {get;set;}
        [Required]
        public string horacita {get;set;}
        public string observaciones {get;set;}
        public int idestadorecojo {get;set;}
        public int idordentrabajo {get; set;}
        public string numcp {get;set;}
    }
}

 public class InsertarActualizarEtapaCommand 
    {
        public long? idetapa { get; set; }
        public int? idmaestroetapa { get; set; }
        public long? idordentrabajo { get; set; }
        public string descripcion { get; set; }
        public string horaetapa { get; set; }
        public string recurso { get; set; }
        public string documento { get; set; }
        public int? idtipoentrega {get;set;}
        
        public decimal? lat {get;set;}
        public decimal? lng {get;set;}
        
        public DateTime? fechaetapa { get; set; }
        public DateTime? fechaentrega {get;set;}
        public string horaentrega {get;set;}    
        public DateTime? fecharegistro { get; set; }
        public int? idusuarioregistro { get; set; }
        public bool visible { get; set; }
        public int idestado { get; set; }
        public int idusuarioentrega { get; set; }
        public int? incidenciaentregaid {get;set;}
        public int? canal_confirmacion_id {get;set;}
        public string personaentrega {get;set;}
        public string  dnientrega {get;set;}
        public bool? enzona {get;set;}
        
    }