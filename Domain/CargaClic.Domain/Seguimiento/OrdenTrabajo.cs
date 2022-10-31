using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CargaClic.Common;

namespace CargaClic.Domain.Seguimiento
{
   public class OrdenTrabajo : Entity
    {
        [Key]
        public long idordentrabajo { get; set; }
        public long? idmanifiesto { get; set; }
        public long? idcarga { get; set; }
        public int idtipotransporte { get; set; }
        public int idconceptocobro { get; set; }
        public int? idestacionorigen { get; set; }
        public int? idorigen { get; set; }
        public int iddestino { get; set; }
        public int identregara { get; set; }
        public int idtipomercaderia { get; set; }
        public int idcliente { get; set; }
        public int idclientetipounidad { get; set; }
        public int idformula { get; set; }
        public int idremitente { get; set; }
        public int idremitentedireccion { get; set; }
        public int iddestinatario { get; set; }
        public int iddestinatariodireccion { get; set; }
        public string dnipersonarecojo { get; set; }
        public string personarecojo { get; set; }
        public string numcp { get; set; }
        public int? idvehiculo { get; set; }
        public string guiarecojo { get; set; }
        public string guiatercero { get; set; }
        public decimal? peso { get; set; }
        public decimal? volumen { get; set; }
        public int? bulto { get; set; }
        public string docgeneral { get; set; }
        public int iddescripciongeneral { get; set; }
        public decimal? subtotal { get; set; }
        public decimal? igv { get; set; }
        public decimal? total { get; set; }
        public string dni { get; set; }
        public string placa { get; set; }
        public string chofer { get; set; }
        public string conceptocobro { get; set; }
        public string puntopartida { get; set; }
        public int idestado { get; set; }
        public int? idtipooperacion { get; set; }
        public int? idruta { get; set; }
        public int? idestaciondestino { get; set; }
        public int? idagencia { get; set; }
        public long? idcamioncompleto { get; set; }
        public string cepan { get; set; }
        public string esembarque { get; set; }
        public DateTime? fecharegistro { get; set; }
        public decimal base1 { get; set; }
        public decimal tarifa { get; set; }
        public decimal minimo { get; set; }
        public int? mesfacturacion { get; set; }
        public long? idpreliquidacion { get; set; }
        public decimal? pesovol { get; set; }
        public long? iddespacho { get; set; }
        public bool activo { get; set; }

        public int? idusuarioentrega { get; set; }
        public string guiatransportista { get; set; }

        public DateTime? fechaentrega { get; set; }
        public DateTime? fecharecojo { get; set; }
        public DateTime? fechaplanificacion { get; set; }
        public DateTime? fechaconfirmacion { get; set; }
        public DateTime? fechacomprobantepago { get; set; }
        public DateTime? fechaliquidaciondocumentaria { get; set; }

        public DateTime? fechadespacho { get; set; }
        public DateTime? fechallegadadestino { get; set; }

        public DateTime? fechaentregaconciliacion { get; set; }
        public int? idusuarioconciliacion { get; set; }
        public bool? archivado { get; set; }
        public int idusuarioregistro { get; set; }
        public decimal? lineaconsumida { get; set; }
        public decimal? recargo { get; set; }

        public bool? registrorapido { get; set; }
        public bool reintegrotributario { get; set; }
        public bool? nofacturable { get; set; }

        public decimal? costoextra { get; set; }
        public bool? devolucion { get; set; }
        public int idtarifa { get; set; }
        public bool? camioncompleto { get; set; }

        public decimal? subtotalfinal { get; set; }
        public bool? facturado { get; set; }
        public string clave_consulta { get; set; }
        public long? idcomprobantepago { get; set; }
        public int? idestacioncreacion { get; set; }
        public string serie { get; set; }
        public int? tipoorden {get;set;}
        public int? idproveedor {get;set;}
        public int? idchofer {get;set;}
        public bool? liquidacion_conductor {get;set; }

        public int? iddocumentoliq {get;set;}
        public string observacionliq {get;set;}
        public long? oriflameid {get;set;}
        public string telefonorecojo  {get;set;}

        public int? idestadocliente {get;set;}
        public string ordernum {get;set;}
        public string clientnum {get;set;}

        public DateTime? fecvisita1 {get;set;}
        public string motivo1 {get;set;}

        public DateTime? fecvisita2 {get;set;}
        public string motivo2 {get;set;}

        public DateTime? fecvisita3 {get;set;}
        public string motivo3 {get;set;}

        public string observacionvisita {get;set;}
        public int? incidenciaentregaid {get;set; }

        
       public string personaentrega {get;set;}
       public string documentoentrega {get;set;}
       public int? canal_confirmacion_id {get;set;}

       public string cam {get;set;}
       public bool? enzona {get;set;}


    
    }
}