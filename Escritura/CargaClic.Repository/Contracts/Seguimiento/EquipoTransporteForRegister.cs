using System.ComponentModel.DataAnnotations;

public class EquipoTransporteForRegister
{
    [Required]
    public int idvehiculo{ get;set; }
    [Required]
    public int? idproveedor{ get;set; }
    [Required]
    public int? idchofer{ get;set; }
    
    public string ids {get;set;}

    public int idusuarioregistro {get;set;}
}