namespace CargaClic.ReadRepository.Contracts.Mantenimiento.Results
{
    public class GetAllDireccionesResult
    {
        public int iddireccion	 { get;set;}
        public string codigo	 { get;set;}
        public string direccion	 { get;set;}
        public int iddistrito	 { get;set;}
        public string distrito	 { get;set;}
        public int idprovincia	 { get;set;}
        public string provincia	 { get;set;}
        public int iddepartamento	 { get;set;}
        public string departamento { get;set;}
    }

    public class GetAllDepartamentos
    {
        public int iddepartamento {get;set;}
        public string departamento {get;set;}
    }

    public class GetAllProvincias
    {
        public int idprovincia {get;set;}
        public string provincia {get;set;}
    }

    public class GetAllDistritos
    {
        public int iddistrito {get;set;}
        public string distrito {get;set;}
    }
    public class GetAllTarifarioProveedorResult
    {

        public int id	{get;set;}
        public int idcliente	{get;set;}
        public int idproveedor {get;set;}
        public string proveedor	{get;set;}
        public int idorigen	{get;set;}
        public int idorigendistrito	{get;set;}
        public int idorigenprovincia	{get;set;}
        
        public string origendistrito	{get;set;}
        public string origenprovincia	{get;set;}
        public string origendepartamento	{get;set;}

        public string departamento	{get;set;}
        public string provincia	{get;set;}
        public string distrito	{get;set;}
        
        public string formula	{get;set;}
        public string TipoUnidad	{get;set;}
        public decimal montobase	{get;set;}
        public decimal minimo	{get;set;}
        public int desde	{get;set;}
        public int hasta	{get;set;}
        public decimal precio {get;set;}


        public decimal adicional {get;set;}
        public decimal primerkilo {get;set;}

        }

    
}