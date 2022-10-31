using System;

namespace CargaClic.Repository.Contracts.Seguimiento
{
    public class CargaMasivaForRegister
    {
        public int id { get; set; }
        public DateTime? fecharegistro { get; set; }
        public int? usuarioid { get; set; }
        public int? estadoid { get; set; }
       
    }
}