using System;

namespace CargaClic.Repository.Contracts.Seguimiento
{
    public class ListarGuiasResult
    {
        public long idguiaremisioncliente { get; set; }
        public long idordentrabajo { get; set; }
        public string nroguia { get; set; }
        public string idcobrarpor { get; set; }
        public string bulto { get; set; }
        public string peso { get; set; }
        public string volumen { get; set; }
        public string pesovol { get; set; }
        public string documento { get; set; }
    }
    public class GetDocumentoResult
    {
        public int idarchivo	 {get;set;}
        public string rutaacceso	 {get;set;}
        public string nombrearchivo	 {get;set;}
        public long idordentrabajo {get;set;}
    }
    public class CalendarioResult
    {
        public int idvehiculo	 {get;set;}
        public string title	 {get;set;}
        public DateTime start	 {get;set;}
        public DateTime end {get;set;}
    }
    // public class CalendarioResult
    // {
    //     public int idvehiculo	 {get;set;}
    //     public string title	 {get;set;}
    //     public DateTime start	 {get;set;}
    //     public DateTime end {get;set;}
    // }
    public class PendientesLiquidacionResult {
        public long idordentrabajo {get;set;}
    	public long idordenrecojo	 {get;set;}
        public DateTime fechahoracita	 {get;set;}
        public string razonsocial	 {get;set;}
        public string tipoorden {get;set;}
        public string tipounidad	 {get;set;}
        public string centroacopio	 {get;set;}
        public string observaciones	 {get;set;}
        public string numhojaruta	 {get;set;}
        public string personarecojo	 {get;set;}
        public DateTime fecharegistro	 {get;set;}
        public string puntorecojo	 {get;set;}
        public int bulto	 {get;set;}
        public decimal peso	 {get;set;}
        public decimal pesovol	 {get;set;}
        public decimal volumen	 {get;set;}
        public string responsable	 {get;set;}
        public string estado {get;set;}
        public string nombrechofer  {get;set;}
        public int idmanifiesto {get;set;}
        public string numcp {get;set;}
        public int idvehiculo {get;set;} 
        public string nummanifiesto {get;set;}
        public string destino {get;set;}
        public string TipoOperacion {get;set;}
        public string TipoTransporte {get;set;}
        public string proveedor {get;set;}
        public string placa {get;set;}
        public string tipo {get;set;}
        public DateTime fechaentregaconciliacion {get;set;}


    }
    public class ObtenerUltimaHojaRutaResult {
        public string numhojaruta { get; set; }
    }
    public class ObtenerUltimoManifiestoResult {

        public string nummanifiesto { get; set; }
    }
    public class ManifiestoxEstados 
    {
        public string nummanifiesto {get;set;}
        public string razonsocial {get;set;}
        public string ruc {get;set;}
        public string nombrechofer {get;set;}
        public string dni {get;set;}
        public string estado {get;set;}
        public string fecharegistro {get;set;}

    }
    public class DetalleOT 
    {
        public string numhojaruta {get;set;}
        public string nummanifiesto {get;set;}
        public string numcarga {get;set;}
        public string numcp  {get;set;}
        public string correlativo {get;set;}
        public string Almacenado {get;set;}
        public string Despacho {get;set;}
    }






}