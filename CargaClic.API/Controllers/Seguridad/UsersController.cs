using System.Threading.Tasks;
using AutoMapper;
using CargaClic.API.Dtos;
using CargaClic.Data.Contracts.Parameters.Seguridad;
using CargaClic.Data.Contracts.Results.Seguridad;
using CargaClic.Data.Interface;
using CargaClic.Domain.Seguridad;
using Common.QueryHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargaClic.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _repo;
        
        private readonly IAuthRepository _auth;
        private readonly IMapper _mapper;
        private readonly IQueryHandler<ListarUsuariosParameters> _user;

        public UsersController(IRepository<User> repo
        
        ,IAuthRepository auth
        , IMapper mapper
        , IQueryHandler<ListarUsuariosParameters> user)
        {
            _user = user;
            _mapper = mapper;
            _repo = repo;
            
            _auth = auth;
        }
       
       
       
        [HttpGet]
        public IActionResult GetUsers(string nombres)
        {
            ListarUsuariosParameters Param = new ListarUsuariosParameters();
            
            Param.Nombre = nombres;
            var users = (ListarUsuariosResult) _user.Execute(Param);
            return Ok(users.Hits);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.Get(x => x.usr_int_id == id);
            var userToResult = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userToResult);
        }


    }
}