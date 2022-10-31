using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Mantenimiento
{
    public class TarifaProveedor : Entity
    {
        [Key]
        public int id {get;set;}
        public int idproveedor {get;set;}
        public int? idorigendistrito {get;set;}
        public int? idorigenprovincia {get;set;}
        public int? idorigendepartamento {get;set;}

        public int? iddestinodistrito {get;set;}
        public int? iddestinoprovincia {get;set;}
        public int? iddestinodepartamento {get;set;}
        public int idtipounidad {get;set;}

        public decimal precio { get; set; }
        public decimal? adicional {get;set;}
        public decimal? desde {get;set;}
        public decimal? hasta {get;set;}
        public decimal? minimo {get;set;}
        
        public decimal? primerkilo {get;set;}
    }
}