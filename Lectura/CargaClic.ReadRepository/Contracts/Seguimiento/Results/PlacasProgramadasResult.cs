using System;

namespace CargaClic.ReadRepository.Contracts.Seguimiento.Results
{
    public class PlacasProgramadasResult
    {
        public long idmanifiesto {get;set;}
        public string numhojaruta {get;set;}
        public long idvehiculo {get;set;}
        public string estado	 {get;set;}
        public string placa	 {get;set;}
        public string chofer	 {get;set;}
        public string proveedor	 {get;set;}
        public string cliente {get;set;}
        public DateTime fechahoracita {get;set;}
    }
}