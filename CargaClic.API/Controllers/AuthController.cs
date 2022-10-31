using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CargaClic.API.Dtos;
using CargaClic.Data.Contracts.Parameters.Seguridad;
using CargaClic.Data.Contracts.Results.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Common.QueryHandlers;
using System.Linq;
using CargaClic.Data.Interface;
using CargaClic.Domain.Seguridad;

namespace CargaClic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IRepository<RolUser> _repo_Roluser;
        private readonly IConfiguration _config;
          private readonly IQueryHandler<ListarMenusxRolParameter> _repo_Menu;

        public AuthController(IAuthRepository repo
        , IRepository<RolUser> repo_roluser
        , IConfiguration config
        , IQueryHandler<ListarMenusxRolParameter>  repo_menu
        
        )
        {
            _config = config;
            _repo_Menu = repo_menu;
            _repo = repo;
            _repo_Roluser = repo_roluser;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var pantallas = new List<ListarMenusxRolDto>();
            var auxPantallas = new List<ListarMenusxRolDto>();

            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            
            if (userFromRepo.usr_int_pwdvalido == 0)
                return Unauthorized();
           

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.usr_int_id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.usr_str_red)
            };

            var roles =  await _repo.GetRolUsuario(userFromRepo.usr_int_id);


            foreach (var rol in roles)
            {
                ListarMenusxRolParameter Param = new  ListarMenusxRolParameter
                {
                 idRol = rol.rol_int_id
                };
                pantallas.AddRange(  ((ListarMenusxRolResult)  _repo_Menu.Execute(Param)).Hits  );
            }

            foreach (var item in pantallas)
             {
                 if(auxPantallas.Where(x=>x.pag_int_id == item.pag_int_id).SingleOrDefault() == null)
                 {
                        auxPantallas.Add(item);
                 }
             }

            List<ListarMenusxRolDto> final = new List<ListarMenusxRolDto>();
            var todos = auxPantallas.Where(x=>x.srp_seleccion == "1").OrderBy(x=>x.pag_int_secuencia);
            foreach (var item in todos)
            {   
                if (item.pag_int_nivel == 1 && item.version == 2)
                {
                    item.submenu = new List<ListarMenusxRolDto>();
                    item.submenu.AddRange(auxPantallas.Where(x=>x.pag_str_codmenu_padre == item.pag_str_codmenu && x.pag_int_nivel == 2 && x.srp_seleccion=="1").OrderBy(x=>x.pag_int_secuencia).ToList());
                    if(final.Where(x=>x.pag_int_id == item.pag_int_id).SingleOrDefault() == null)
                    final.Add(item);
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
             .GetBytes(_config.GetSection("AppSettings:Token").Value));

             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

             var tokenDescriptor = new SecurityTokenDescriptor
             {
                 Subject = new ClaimsIdentity(claims),
                 Expires = DateTime.Now.AddDays(365),
                 SigningCredentials = creds
             };

             var tokenHandler = new JwtSecurityTokenHandler();

             var token = tokenHandler.CreateToken(tokenDescriptor);

             return Ok(new {
                 menu = final,
                 id_usr  = userFromRepo.usr_int_id,
                 user = userFromRepo,
                 token = tokenHandler.WriteToken(token)
             });


        }


    }
}