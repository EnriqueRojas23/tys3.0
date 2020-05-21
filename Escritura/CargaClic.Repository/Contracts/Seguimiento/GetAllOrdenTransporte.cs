using System;

namespace CargaClic.Repository
{
    public class GetAllOrdenTransporteResult
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public string razonsocial { get; set; }
        // public int idtipotransporte { get; set; }
        // public int idconceptocobro { get; set; }
        public string destino { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public string tipotransporte { get; set; }
        public string conceptocobro { get; set; }
       //   public long idpreliquidacion { get; set; }
        public DateTime fecharegistro { get; set; }

        public DateTime? fechadespacho { get; set; }
        public DateTime? fechaentrega { get; set; }
        public DateTime? fecharecojo { get; set; }

        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public int bulto{ get; set; }
        public string docgeneral { get; set; }

        public string GRR { get; set; }
        public string estado { get; set; }
        

    }
}


// [11:29, 11/25/2019] Exel: usuario: SSPP
// [11:29, 11/25/2019] Exel: Clave: MZdICi