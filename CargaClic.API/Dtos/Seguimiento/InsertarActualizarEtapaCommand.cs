using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Webapi.Data.Commands.QueryContracts
{
    public class ConfirmarEntregaCommand 
    {
        public string numcp {get;set;}
        public string horaetapa { get; set; } 
        [DateTimeModelBinder(DateFormat = "dd/MM/yyyy")]
        public DateTime fechaetapa { get; set; }
        public string descripcion { get; set; }
        public int? idmaestroetapa { get; set; }
        public string etapa {get;set;}
        public string recurso { get; set; }
        public string documento { get; set; }
        public int? id_usuario {get;set;}
        public int? idtipoentrega {get;set;}

    }
   
    public class InsertarGuiasRechazadas 
    {
        public long idguia { get; set; }
        public long idordentrabajo { get; set; }
        public string guia { get; set; }
        public int cantidad { get; set; }
        
    }
   


}