using System;

namespace CargaClic.Repository
{
    public class GetAllOrdenTransporteResult
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public string razonsocial { get; set; }
        public string destino { get; set; }
        public string origen {get;set;}
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public string tipotransporte { get; set; }
        public string conceptocobro { get; set; }
        public string telefonorecojo {get;set;}
        public DateTime fecharegistro { get; set; }

        public DateTime? fechadespacho { get; set; }
        public int diasDesdeDespacho {get;set;}
        public DateTime? fechaentrega { get; set; }

        public string horaentrega {get;set;}
        public DateTime? fecharecojo { get; set; }

        public string puntopartida {get;set;}
        public string direccion {get;set;}


        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public int bulto{ get; set; }
        public string docgeneral { get; set; }

        public string GRR { get; set; }
        public string estado { get; set; }
        public string estadomercaderia_entrega {get;set;}
        public string tipoentrega {get;set;}
        public string personaentrega {get;set;}
        public string dnientrega {get;set;}


        public DateTime fechacita {get;set;}
        public string horacita {get;set;}
        public int idtipounidad {get;set;}
        public string centroacopio  {get;set;}
        public string observaciones {get;set;}
        public int idcliente {get;set;}
        
        public string personarecojo {get;set;}
        public long idordenrecojo {get;set;}

        public string chofer {get;set;}
        public string cliente {get;set;}
        public string placa {get;set;}
        public long oriflameid {get;set;}
        public decimal lng {get;set;}
        public decimal lat {get;set;}


        public DateTime?  fecvisita1 {get;set;}
        public DateTime?  fecvisita2 {get;set;}
        public DateTime?  fecvisita3 {get;set;}


        public string motivo1des {get;set;}
        public string motivo2des {get;set;}
        public string motivo3des {get;set;}

        public int? motivo1 {get;set;}
        public int? motivo2 {get;set;}
        public int? motivo3 {get;set;}

        public string observacionvisita {get;set;}
        public string proveedor {get;set;}
        public string enzona {get;set;}



        

    }
    public class IncidenciaResult
    {
        public long ot { get; set; }
        public string numcp { get; set; }
        public DateTime fechaevento { get; set; }
        public string tipoevento { get; set; }
        public int idmaestroevento { get; set; }
        public string evento { get; set; }
        public string observacion { get; set; }
        public string recurso { get; set; }
        public string documento { get; set; }
        public int idusuarioregistro { get; set; }
        public DateTime fecharegistro { get; set; }
       
        public bool autogenerada { get; set; }
        public string usuario { get; set; }
        public string estacionorigen { get; set; }

    }
    public class OtsRetornoResult
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public string guiatransportista { get; set; }
        public string remitente { get; set; }
        public DateTime fecharecojo { get; set; }
        public DateTime fechadespacho { get; set; }
        public string destino { get; set; }
        public string coordinador { get; set; }
        public string UltimaIncidencia { get; set; }
        public string LeadDocumentario { get; set; }
        public int DiasTranscurridos { get; set; }
        public string destinatario { get; set; }
        public string conceptocobro { get; set; }
        public string proveedor {get;set;}
        public DateTime fechaentregareal { get; set; }
        public DateTime? fechaentregaconciliacion { get; set; }
        public string estado {get;set;}

    }
}

