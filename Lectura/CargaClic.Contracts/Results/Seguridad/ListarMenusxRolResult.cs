using System.Collections.Generic;
using Common.QueryContracts;

namespace CargaClic.Data.Contracts.Results.Seguridad
{
    public class ListarMenusxRolResult : QueryResult 
    {
        public IEnumerable<ListarMenusxRolDto> Hits { get; set; }
    }
    public class ListarMenusxRolDto
    {
        public int pag_int_id {get;set;}
        public string pag_str_codmenu	{ get;set; }
        public string pag_str_codmenu_padre	{ get;set; }
        public string pag_str_nombre	{ get;set; }
        public string pag_str_url	{ get;set; }
        public int pag_int_nivel	{ get;set; }
        public int pag_int_secuencia	{ get;set; }
        public string Icono	{ get;set; }        
        public string srp_seleccion { get;set; }
        public bool visible {get;set;}
        public int version {get;set;}
        
        public List<ListarMenusxRolDto> submenu {get;set;}

    }

   



 
}