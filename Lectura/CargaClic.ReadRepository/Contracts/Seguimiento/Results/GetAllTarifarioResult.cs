namespace Api.ReadRepository.Contracts.Mantenimiento.Results
{
    public class GetAllTarifarioResult 
    {

        public int id	{get;set;}
        public int idcliente	{get;set;}
        public string razonsocial	{get;set;}
        public int idorigen	{get;set;}
        public int idorigendistrito	{get;set;}
        public int idorigenprovincia	{get;set;}
        
        public string origendistrito	{get;set;}
        public string origenprovincia	{get;set;}
        public string origendepartamento	{get;set;}

        public string destinodepartamento	{get;set;}
        public string destinoprovincia	{get;set;}
        public string destinodistrito	{get;set;}
        
        public string formula	{get;set;}
        public int idtipounidadmedida	{get;set;}
        public int tipounidad	{get;set;}
        public int idtipotransporte	{get;set;}
        public string tipotransporte	{get;set;}
        public decimal montobase	{get;set;}
        public decimal minimo	{get;set;}
        public int desde	{get;set;}
        public int hasta	{get;set;}
        public decimal precio {get;set;}







    }
}