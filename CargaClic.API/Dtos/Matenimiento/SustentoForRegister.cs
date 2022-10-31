using System;
using System.ComponentModel.DataAnnotations;

namespace CargaClic.API.Dtos.Matenimiento
{
    public class SustentoForRegisterDto
    {
        
        [Required]
        [MaxLength(20)]
        [MinLength(4)]
        public string numhojaruta {get;set;}
        public DateTime fecha { get; set; }
        public decimal montodepositado { get; set; }
        public decimal kilometrajeInicio { get; set; }
        public decimal kilometrajefinal { get; set; }
        public int idusuarioregistro {get;set;}

    }
}