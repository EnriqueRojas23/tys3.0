using System;
using System.ComponentModel.DataAnnotations;

namespace CargaClic.API.Dtos.Recepcion
{
    public class CuadrillaForRegisterDto
    {
        public string nombrecompleto { get; set; }
        public string dni { get; set; }
        public long idordenrecojo { get; set; }
    }
}