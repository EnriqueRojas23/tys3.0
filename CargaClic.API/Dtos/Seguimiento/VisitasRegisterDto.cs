
using System;

namespace CargaClic.API.Dtos.Seguimiento
{
        public class VisitasRegisterDto {
            public long idordentrabajo {get;set;} 
            public DateTime? fecvisita1 {get;set;} 
            public  string motivo1 {get;set;}
            public DateTime? fecvisita2 {get;set;} 
            public  string motivo2 {get;set;}
            public DateTime? fecvisita3 {get;set;} 
            public  string motivo3 {get;set;}

            public string observacionvisita {get;set;}
        }
        public class CargaMasivaDto {
            public long cargaid {get;set;}
            public int idcliente {get;set;}
        }
}