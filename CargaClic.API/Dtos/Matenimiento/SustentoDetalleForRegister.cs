using System;
using System.ComponentModel.DataAnnotations;

namespace CargaClic.API.Dtos.Matenimiento
{
    public class SustentoDetalleForRegisterDto
    {
        public int sustentoid {get;set;}
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public int idtipodocumento { get; set; }
        [Required]
        public int idtiposustento { get; set; }
        [Required]
        public string numeroDocumento { get; set; }
        public decimal montoBase { get; set; }
        public decimal montoImpuesto { get; set; }
        public decimal montoTotal { get; set; }
        public int idestado { get; set; }
      
    }
}