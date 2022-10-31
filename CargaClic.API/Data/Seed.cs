using System.Collections.Generic;
using CargaClic.Data;
using CargaClic.Domain.Seguridad;
using Newtonsoft.Json;

namespace CargaClic.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            this._context = context;
        }
        public void SeedPaginas()
        {
             var tablaData = System.IO.File.ReadAllText("Data/PaginaSeedData.json");
            var paginas = JsonConvert.DeserializeObject<List<Pagina>>(tablaData);
            foreach (var pagina in paginas)
            {
                _context.Paginas.Add(pagina);
            }
            _context.SaveChanges();
        }
        public void SeedRoles()
        {
            var tablaData = System.IO.File.ReadAllText("Data/RolSeedData.json");
            var roles = JsonConvert.DeserializeObject<List<Rol>>(tablaData);
            foreach (var rol in roles)
            {
                _context.Roles.Add(rol);
            }
            _context.SaveChanges();
        }
        public void SeedRolPaginas()
        {
            var tablaData = System.IO.File.ReadAllText("Data/RolPaginaSeedData.json");
            var rolPaginas = JsonConvert.DeserializeObject<List<RolPagina>>(tablaData);
            foreach (var rol in rolPaginas)
            {
                _context.RolPaginas.Add(rol);
            }
            _context.SaveChanges();
        }
       
       

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}