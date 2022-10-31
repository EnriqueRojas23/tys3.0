using System;
using System.ComponentModel.DataAnnotations;

namespace CargaClic.Repository
{
     public class GuiaRemisionBlancoDelete
        {
            public long id { get; set; }
        }
    public class GuiaRemisionBlancoForRegister
    {
        public long id{ get;set; }
        public int idvehiculo{ get;set; }
        [Required]
        public long idmanifiesto { get;set; }
        [Required]
        public string numeroguia{ get;set; }
        [Required]
        public DateTime fecharegistro{ get;set; }
        public int cantidad { get;set; }


    }
}