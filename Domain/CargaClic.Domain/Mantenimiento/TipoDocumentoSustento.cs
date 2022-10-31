using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Mantenimiento
{
   public class TipoDocumentoSustento : Entity
  {
        [Key]
        public int id { get; set; }
        public string descripcion { get; set; }
        public string codigo { get; set; }
        public bool activo { get; set; }
        public bool requiereAprobacion {get;set;}
       

    }
}