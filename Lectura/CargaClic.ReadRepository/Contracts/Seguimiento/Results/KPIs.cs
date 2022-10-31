using System;

namespace CargaClic.ReadRepository.Contracts.Seguimiento.Results
{
    public class PorEstadoResult
    {
        public string estado	 {get;set;}
        public int cantidad	 {get;set;}
    }
    public class GetDespachosATiempo
    {
        public string numcp {get;set;}
        public DateTime fechadespacho{get;set;}
        public DateTime fechaestimada {get;set;}
        public DateTime fechaentrega {get;set;}
        public int atiempo {get;set;}
        public string cliente {get;set;}
        public int tiempo {get;set;}
        public string distrito {get;set;}
        public string provincia {get;set;}
        public string departamento {get;set;}
    }
    
    public class GetRetornoDocumentario
    {
        public string numcp {get;set;}
        public DateTime fechaentrega {get;set;}
        public DateTime fechaestimada {get;set;}
        public DateTime fechaconciliacion {get;set;}
        public int atiempo {get;set;}
        public string cliente {get;set;}
        public int ots {get;set;}
        public string rango {get;set;}
    }
    
    public class GetEntregaVsConciliacionResult
    {
        public string numcp {get;set;}
        public DateTime fechadespacho{get;set;}
        public DateTime fechaentregaconciliacion{get;set;}
        public DateTime fechaentrega {get;set;}
        public int diferencia {get;set;}
        public int coincide {get;set;}
        public string cliente {get;set;}
        public string distrito {get;set;}
        public string provincia {get;set;}
        public string departamento {get;set;}

    }
}