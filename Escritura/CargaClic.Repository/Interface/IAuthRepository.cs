using System.Threading.Tasks;
using CargaClic.Data;
using System.Collections.Generic;
using System.Linq.Expressions;

using CargaClic.Domain.Seguridad;
using CargaClic.Repository;

namespace CargaClic.Data.Interface
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Update(User user);
        Task<User> UpdateEstadoId(User user);
        Task<GetUsuario> Login(string username, string password);
        Task<User> Get(int usuarioid);
        Task<bool> UserExists(string username);
    }
}