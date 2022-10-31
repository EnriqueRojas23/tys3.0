using System;
using CargaClic.Common;

namespace CargaClic.Domain.Mantenimiento
{
    public class Sustento : Entity
    {
        public long? id { get; set; }
        public string numhojaruta { get; set; }
        public DateTime fecha { get; set; }
        public decimal montodepositado { get; set; }
        public decimal kilometrajeInicio  { get; set; }
        public decimal kilometrajefinal  { get; set; }
        public decimal km  { get; set; }
        public int idusuarioregistro  { get; set; }
        public int? idestado {get;set;}

    }
}