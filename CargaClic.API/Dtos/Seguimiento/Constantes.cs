namespace Webapi.Common
{
    public sealed class Constantes
    {
         public enum EstadoOT : int
        {
            PendienteProgramacion = 6,
            PendienteInicioCarga = 7,
            PendienteDespacho = 8,
            PendienteRecepcionIntermedia = 9,
            PendienteRecepcionDestino = 10,
            PendienteEntrega = 11,
            PendienteRetornoDocumentario = 12,
            Facturado = 13,
            PendienteFacturacion = 21,
            Cerrado = 25
        }
         public enum MaestroEtapa : int
        {
            CargoRetornado = 21,
        
        }
    }
}