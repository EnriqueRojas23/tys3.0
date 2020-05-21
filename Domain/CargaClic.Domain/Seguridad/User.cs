using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CargaClic.Common;
using CargaClic.Domain.Mantenimiento;

namespace CargaClic.Domain.Seguridad
{
    public class User : Entity
    {
         public int Id {get;set;}
        public int usr_int_id	{get;set;}
        public string usr_str_nombre	{get;set;}
        public string usr_str_apellidos	{get;set;}
        public string usr_str_red	{get;set;}
        public string usr_str_email	{get;set;}
        public string usr_int_bloqueado	{get;set;}
        public int idcliente	{get;set;}
        public int idestacionorigen	{get;set;}
        public int iddistrito	{get;set;}
        public int idprovincia	{get;set;}
        public string idclientes	{get;set;}
        public bool usr_bit_aprobado {get;set;}

        // public int Id {get;set;}
        // public string Username { get; set; }
        // public string NombreCompleto { get; set; }
        // public string Email {get;set;}
        // public bool EnLinea { get; set; }
        // public DateTime Created { get; set; }
        // public String Dni {get;set;}
        // public DateTime? LastActive { get; set; }
        // [Required]
        // public byte[] PasswordHash { get; set; }
        // [Required]
        // public byte[] PasswordSalt { get; set; }
        // public int EstadoId { get; set; }
        // public ICollection<RolUser> RolUser {get;set;}
        
    }
}