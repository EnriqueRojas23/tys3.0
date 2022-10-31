using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Seguimiento
{
    public class Cuadrilla : Entity
    {
        [Key]
        public long id { get; set; }
        public string nombrecompleto { get; set; }
        public string dni { get; set; }
        public long idordenrecojo { get; set; }

    }
}