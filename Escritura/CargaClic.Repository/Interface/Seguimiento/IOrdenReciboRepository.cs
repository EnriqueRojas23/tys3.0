
using System.Collections.Generic;
using System.Threading.Tasks;
using CargaClic.Repository.Contracts.Seguimiento;

namespace CargaClic.Repository.Interface
{
    public interface ISeguimientoRepository
    {
        Task<IEnumerable<GetAllOrdenTransporteResult>> GetAllOrdenTransporte(int? idscliente, string numcp , string fecinicio,
        string fecfin, string grr, string docreferencia, int? idestado, int? iddestino, int idusuario);
        Task<IEnumerable<ListarClientesResult>> GetAllClientes(string idscliente);
        Task<IEnumerable<ListarUbigeoResult>> GetListarUbigeo(string criterio);
     
    }
}