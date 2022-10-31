using System;

namespace CargaClic.ReadRepository.Contracts.Seguimiento.Results
{
    public class EquipoTransporteResult
    {
        public int idvehiculo {get;set;}
        public int idchofer	 {get;set;}
        public int idproveedor	 {get;set;}

        public string placa	 {get;set;}
        public string datosunidad	 {get;set;}
        public string nombrechofer {get;set;}
        public string dni {get;set;}
        public string proveedor { get;set;}
        public string rucproveedor {get;set;}
        public string brevete {get;set;}
     
    }
}