using System;
using CargaClic.Common;

namespace CargaClic.Domain.Mantenimiento
{
    public class SustentoDetalle : Entity
    {
        public long id { get; set; }
        public long sustentoid { get; set; }
        public DateTime fecha {get;set;}
        public int idtiposustento {get;set;}
        public int idtipodocumento {get;set;}
        public string numeroDocumento {get;set;}
        public decimal montoBase {get;set;}
        public decimal montoImpuesto {get;set;}

        public decimal montoTotal {get;set;}

        public int usuarioAprobador {get;set;}

        public bool aprobado {get;set;}
        

        public int usuarioAprobacion  {get;set;}
        public int idestado {get;set;}
        public DateTime fechaRegistro {get;set;}
        
        public decimal valorBase {get;set;}
    
     
      

    }
}