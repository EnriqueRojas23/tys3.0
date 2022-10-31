using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Mantenimiento
{
   public class Vehiculo : Entity
  {
        [Key]
        public int idvehiculo { get; set; }
        public string placa { get; set; }
        public int? idtipo { get; set; }
        public int? idmarca { get; set; }
        public int? idmodelo {get;set;}
         public string confveh { get; set; }
         public decimal? pesobruto { get; set; }
         public decimal? cargautil {get;set;}
         public bool activo {get;set;}
         public int idproveedor  {get;set;}

    }
}