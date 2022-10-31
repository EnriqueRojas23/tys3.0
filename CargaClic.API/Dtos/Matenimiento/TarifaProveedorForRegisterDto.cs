using System;
using System.ComponentModel.DataAnnotations;

namespace CargaClic.API.Dtos.Matenimiento
{
    public class  TarifaProveedorForRegisterDto
    {
        public int id {get;set;}
        [Required]
        public int idproveedor {get;set;}

        [Required]
        public int iddestinodistrito {get;set;}
        public int iddestinoprovincia {get;set;}
        public int iddestinodepartamento {get;set;}
        [Required]
        public decimal precio { get; set; }
        [Required]
        public int idtipounidad {get;set;}

        public decimal? minimo {get;set;}
        public decimal? desde {get;set;}
        public decimal? hasta {get;set;}
        public decimal? tarifabase {get;set;}
        public decimal? adicional {get;set;}

        public decimal? primerkilo {get;set;}




    }
}