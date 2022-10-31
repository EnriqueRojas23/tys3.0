using System;
using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Seguimiento
{
    public class CargaMasivaDetalle : Entity
    {
            [Key]
            public long id { get; set; }
            public string daterep { get; set; }
            public string timerep { get; set; }
            public string ordernum { get; set; }
            public string orderdate { get; set; }
            public string ordertime { get; set; }
            public string clientnum { get; set; }
            public string lastname { get; set; }
            public string firstname { get; set; }
            public string addr1 { get; set; }
            public string addr2 { get; set; }
            public string addr3 { get; set; }
            public string addr4 { get; set; }
            public string addr5 { get; set; }
            public string homephone { get; set; }  
            public string busphone {get;set;}
            public string postcode {get;set;}
            public string courier {get;set;}
            public string tipo {get;set;}
            public string peso {get;set;}
            public string pesovol {get;set;}
            public string numguia {get;set;}
            public string guianum {get;set;}
            public string fecha_real_entrega {get;set;}
            public string desp_dias {get;set;}
            public string fecha_estimada {get;set;}
            public string waybillnum {get;set;}
            public int cargaid {get;set;}
            public string bultos {get;set;}

    }
}