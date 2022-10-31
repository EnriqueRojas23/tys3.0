using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CargaClic.Common;
using CargaClic.Domain.Seguridad;

namespace CargaClic.Domain.Mantenimiento
{
    public class Distrito : Entity
    {
        [Key]
        public int iddistrito { get; set; }
        public string distrito { get; set; }
        public int idprovincia { get; set; }
    }
}