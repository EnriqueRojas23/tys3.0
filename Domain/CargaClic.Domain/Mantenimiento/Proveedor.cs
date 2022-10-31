using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Mantenimiento
{
    public class Proveedor : Entity
    {
        [Key]
        public int idproveedor { get; set; }
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }
    }
}