using System;
using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Seguimiento
{
    public class GuiaRemisionCliente : Entity
    {
        [Key]
        public long idguiaremisioncliente { get; set; }
        public long idordentrabajo { get; set; }
        public string nroguia { get; set; }

        public int idcobrarpor {get;set;}
        public int bulto {get;set;}
        public decimal volumen {get;set;} 
        public decimal pesovol {get;set;}
        public string documento {get;set;}

    }
}