namespace Api.ReadRepository.Mantenimiento.Parameters
{
    public class ListarTarifaOrdenParameters 
    {
        public int idorigendistrito { get; set; }
        public int idorigenprovincia { get; set; }
        public int idorigendepartamento { get; set; }

        public int iddepartamento { get; set; }
        public int idprovincia { get; set; }
        public int iddistrito { get; set; }

        public int idcliente { get; set; }
        public int idformula { get; set; }
        public int idtipotransporte { get; set; }
        public int idtipomaterial {get;set;}
        public int? idconceptocobro { get; set; }
    }
}