using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Seguimiento
{
   public class CargaMasiva
    {
        [Key]
        public int id { get; set; }
        public DateTime? fecharegistro { get; set; }
        public int? usuarioid { get; set; }
        public int? estadoid { get; set; }

    }
}