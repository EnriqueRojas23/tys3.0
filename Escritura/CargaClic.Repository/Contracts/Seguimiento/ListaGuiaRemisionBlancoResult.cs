using System;
using System.ComponentModel.DataAnnotations;

public class ListaGuiaRemisionBlancoResult
{
   
    public long id{ get;set; }
    public int idvehiculo{ get;set; }
    public string numeroguia{ get;set; }
    public DateTime fecharegistro{ get;set; }
    public string placa{ get;set; }
    public int idestado {get;set;}
    public long idordentrabajo {get;set;}
    public string estado {get;set;}
  

}