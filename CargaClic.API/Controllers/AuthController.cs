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
        

        public AuthController(IAuthRepository repo
        , IRepository<RolUser> repo_roluser
        , IConfiguration config
        
        )
        {
            _config = config;
            
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
                new Claim(ClaimTypes.Name, userFromRepo.usr_str_email)
            };

           
              
            //  //Eliminar duplicados [multiples Roles]
            //  foreach (var item in pantallas)
            //  {
            //      if(auxPantallas.Where(x=>x.Id == item.Id).SingleOrDefault() == null)
            //      {
            //             auxPantallas.Add(item);
            //      }
            //  }

            // List<ListarMenusxRolDto> final = new List<ListarMenusxRolDto>();

            // foreach (var item in auxPantallas.Where(x=>x.srp_seleccion == "1").OrderBy(x=>x.Orden))
            // {   
            //     if (item.Nivel=="1")
            //     {
            //         item.submenu = new List<ListarMenusxRolDto>();
            //         item.submenu.AddRange(auxPantallas.Where(x=>x.CodigoPadre == item.Codigo && x.Nivel == "2" && x.srp_seleccion=="1").OrderBy(x=>x.Orden).ToList());
            //         if(final.Where(x=>x.Id == item.Id).SingleOrDefault() == null)
            //         final.Add(item);
            //     }
            // }

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
                 menu = userFromRepo,
                 token = tokenHandler.WriteToken(token)
             });


        }


    }
}