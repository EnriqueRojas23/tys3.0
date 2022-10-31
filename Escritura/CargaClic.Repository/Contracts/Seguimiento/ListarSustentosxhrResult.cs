using System;

namespace CargaClic.Repository.Contracts.Seguimiento
{
    public class ListarSustentoResult
    {
        public int id { get; set; }
        public string numhojaruta {get;set;}
        public string aprobado {get;set;}
        public string  numeroDocumento {get;set;}
        public DateTime fecha {get;set;}
        public decimal MontoBase {get;set;}
     
        public string TipoDocumento {get;set;}
        public string TipoSustento { get; set; }
        public int idtipodocumento {get;set;}

    }
}