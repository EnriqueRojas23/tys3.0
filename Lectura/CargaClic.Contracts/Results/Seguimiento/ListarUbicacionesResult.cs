using System;
using System.Collections.Generic;
using Common.QueryContracts;

namespace CargaClic.Contracts.Results.Prerecibo
{
   
    public class ListarValorTablaResult 
    {
        public int idvalortabla { get; set; }
        public string valor { get; set; }
        public int idmaestrotabla { get; set; }
        public bool activo { get; set; }
        public int orden { get; set; }
        
    }
}