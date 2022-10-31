
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CargaClic.API.Dtos.Seguimiento;
using CargaClic.Domain.Seguimiento;
using CargaClic.Repository.Contracts.Seguimiento;
using Webapi.Dtos;

namespace CargaClic.Repository.Interface
{
    public interface ISeguimientoRepository
    {
         Task<IEnumerable<GetAllOrdenTransporteResult>> GetAllOrdenTransporte(int? idscliente, string numcp , string fecinicio,
        string fecfin, string grr, string docreferencia, int? idestado, int? iddestino, int? idusuario, int? iddestinatario, int? idtipo, int? idproveedor,   string guiarecojo = "", string clave = "" );

         Task<IEnumerable<OtsRetornoResult>> GetAllOrdenTransporteDocument(int? idscliente, string numcp , string fecinicio,
        string fecfin, string grr,  int? iddestinatario, int? idproveedor  );

        Task<IEnumerable<GetAllOrdenTransporteResult>> GetAllOrdenTransporteGeneral(string numcp ,string guiarecojo , string clave = "");

        Task<IEnumerable<IncidenciaResult>> GetAllIncidencias(long idordentrabajo);
        Task<GetAllOrdenTransporteResult> GetOrdenRecojo(long idordentrabajo);
        Task<GetAllOrdenTransporteResult> GetOrdenRecojoxid(long idordentrabajo);

        
         Task<int> RegisterCargaMasiva (CargaMasivaForRegister cargaMasiva , IEnumerable<CargaMasivaDetalleForRegister> cargaMasivaDetalle );
         Task<int> ActualizarProveedor (CambiarProveedorCommand command );
         Task<int> SetCamToOrder (SetCamToOrderForUpdate command );

        Task<ObtenerOrdenTrabajoDto> ObtenerCamOrden(string cam);
        Task<IEnumerable<ListarClientesResult>> GetAllClientes(string idscliente);
        Task<IEnumerable<ListarProveedorxClienteDto>> GetAllDestinatarios(int idcliente);
       
        Task<IEnumerable<ListarUbigeoResult>> GetListarUbigeo(string criterio);
        Task<IEnumerable<ListarArchivosResult>> GetListarArchivos(long? idarchivo, long? idorden);
        Task<IEnumerable<ListarGuiasResult>> GetListarGuias(long? idorden);
        Task<Int16> ConfirmarEntrega(InsertarActualizarEtapaCommand  orden);
        Task<bool> ConfirmarRetornoDocumentario(LiquidacionForUpdateDto  orden);

        Task<IEnumerable<CalendarioResult>> GetListarCalendario();
        Task<IEnumerable<GetDocumentoResult>> GetAllDocumentos(int site_id);

        Task<IEnumerable<PendientesLiquidacionResult>>  getListarPendientesFacturacionOS(string numhojaruta);

        Task<long> RegistrarOrdenTransporte(IEnumerable<OrdenTrabajoForRegister> ordenes);


        Task<long> RegistrarOrdenRecojo(OrdenRecojoRegister ordenrecojo);
        Task<long> UpdateOrdenRecojo(OrdenRecojoRegister ordenrecojo);

        Task<long> RegistarManifiesto(int idvehiculo, int  idusuarioregistro);

        Task<IEnumerable<PendientesLiquidacionResult>> GetAllPendienteLiquidacionxDNI(string dni, string fec_ini, string fec_fin);

        Task<IEnumerable<PendientesLiquidacionResult>> GetAllPendienteLiquidacionRepartidorxDNI(string dni, string fec_ini, string fec_fin);
        Task<IEnumerable<PendientesLiquidacionResult>> GetAllPendienteLiquidacionxDNIOT(string dni, string fec_ini, string fec_fin);
        Task<IEnumerable<PendientesLiquidacionResult>> GetAllPendienteLiquidacionesxManifiesto(int idmanifiesto);

        Task<IEnumerable<PendientesLiquidacionResult>> getAllOrdersxManifiesto(int idmanifiesto);
        Task<IEnumerable<DetalleOT>> getAllPendientesIngresos(string numcp);
        Task<IEnumerable<DetalleOT>> getAllPendientesDespachos(string numcp);
        Task<IEnumerable<ManifiestoxEstados>> getAllManifiestosPorEstado(int? idestado);
        



        Task<IEnumerable<PendientesLiquidacionResult>> getAllManifiestoPendientes(string hojaruta);



        Task<IEnumerable<ListaGuiaRemisionBlancoResult>> GetGuiaRemisionBlancoPorVehiculo(int idmanifiesto, long idordentrabajo);      
        Task<IEnumerable<GuiaRemisionBlanco>> RegistroGuiaRemisionBlanco(GuiaRemisionBlancoForRegister guia);
        Task<long> EliminarGuiaRemisionBlanco(long id);
        Task<IEnumerable<ListarSustentoResult>> GetListarSustentoxHR(string hojaruta);
    }
}
