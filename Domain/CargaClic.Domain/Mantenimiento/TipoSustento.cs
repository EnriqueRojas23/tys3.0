using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Mantenimiento
{
   public class TipoSustento : Entity
  {
        [Key]
        public int id { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
       

    }
}