using System;

namespace CargaClic.ReadRepository.Contracts.Seguimiento.Results
{
    public class OrdenesRecojoResult
    {
        public long idordentrabajo {get;set;}
        public long idordenrecojo {get;set;}
        public long id	 {get;set;}
        public DateTime fechahoracita	 {get;set;}
        public string razonsocial	 {get;set;}
        public string tipounidad	 {get;set;}
        public string centroacopio	 {get;set;}
        public string observaciones	 {get;set;}
        public string numcp	 {get;set;}
        public string personarecojo	 {get;set;}
        public DateTime fecharegistro	 {get;set;}
        public int bulto	 {get;set;}
        public decimal peso	 {get;set;}
        public decimal pesovol {get;set;}
        public string responsable {get;set;}
        public string estado {get;set;}
        public string puntorecojo {get;set;}
    }
    public class GetCuadrillaResult
    {
        public long id {get;set;}
        public string nombrecompleto	 {get;set;}
        public string dni	 {get;set;}
    }
}