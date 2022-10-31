using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CargaClic.Common;

namespace CargaClic.Domain.Mantenimiento
{

    public class ValorTabla : Entity
    {
        public int idvalortabla { get; set; }  
        public string valor { get; set; }
        public int orden { get; set; }
        public int idmaestrotabla {get;set;}
        public bool activo {get;set;}
    }
}