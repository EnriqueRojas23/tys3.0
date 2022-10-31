using System;

namespace CargaClic.Repository
{
    public class GetUsuario
    {
        public int usr_int_pwdvalido	{get;set;}
        public int usr_int_rolinvalido	{get;set;}
        public string usr_str_recordarpwd	{get;set;}
        public int usr_int_aprobado	{get;set;}
        public int usr_int_bloqueado	{get;set;}
        public DateTime usr_dat_fecvctopwd	{get;set;}
        public int usr_int_numintentos	{get;set;}
        public DateTime usr_dat_ultfeclogin	{get;set;}
        public string usr_str_red	{get;set;}
        public int usr_int_id	{get;set;}
        public string usr_str_email	{get;set;}
        public int? idprovincia{get;set;}
        public string idclientes {get;set;}

    }
    public class RolUsuario
    {
        public int usr_int_id {get;set;}
        public int rol_int_id {get;set;}
    }
}