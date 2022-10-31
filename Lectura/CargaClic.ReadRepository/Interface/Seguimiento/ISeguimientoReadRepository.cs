using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ReadRepository.Contracts.Mantenimiento.Results;
using Api.ReadRepository.Mantenimiento.Parameters;
using CargaClic.ReadRepository.Contracts.Seguimiento.Results;

namespace CargaClic.ReadRepository.Interface
{
    public interface ISeguimientoReadRepository
    {

        Task<IEnumerable<GetAllValorTablaResult>> GetAllValorTabla(int idvalortabla);
        Task<IEnumerable<OrdenesRecojoResult>> GetAllOrdenesRecojo(int? idcliente, string fec_ini, string fec_fin, int? idestado);
        Task<IEnumerable<PlacasProgramadasResult>> GetAllPlacasProgramadas(string ruc, string placa);
        
        Task<EquipoTransporteResult> GetEquipoTransporteAsociado(long idor);
        Task<IEnumerable<OrdenesRecojoResult>> GetAllOrdenesTrabajoxIds(string ids);
        Task<EquipoTransporteResult> GetEquipoTransporte(string placa);


        Task<IEnumerable<GetCuadrillaResult>> GetAllCuadrilla(long idrecojo);
        Task<IEnumerable<GetAllTarifarioResult>> GetListarTarifasOrden(ListarTarifaOrdenParameters parameters);
        Task<IEnumerable<GetAllUbigeoResult>> GetAllUbigeo(String Criterio);


        Task<IEnumerable<PorEstadoResult>> GetPorEstado(int? idcliente, int? iddestino);

        Task<IEnumerable<GetDespachosATiempo>> GetDespachosATiempo(int? remitente_id,string fec_ini, string fec_fin);
        Task<IEnumerable<GetEntregaVsConciliacionResult>> GetEntregaVsConciliacion(int? remitente_id,string fec_ini, string fec_fin);



        Task<decimal> GetTarifaProveedor(int? iddestino, int proveedorid, int tipounidadid);
         Task<decimal> GetTarifaProveedorProvincia(int? iddestino, int proveedorid, int tipounidadid);


         
        Task<IEnumerable<GetRetornoDocumentario>> GetPendientesRetornoDocumentario(int? remitente_id,string fec_ini, string fec_fin, 
        int? iddepartamento, int? idprovincia);
        Task<IEnumerable<GetRetornoDocumentario>> GetPendientesDespacho(int? remitente_id,string fec_ini, string fec_fin, 
        int? iddepartamento, int? idprovincia);


         Task<IEnumerable<GetRetornoDocumentario>> GetPendientesEntrega(int? remitente_id,string fec_ini, string fec_fin, 
        int? iddepartamento, int? idprovincia);


    }
}

