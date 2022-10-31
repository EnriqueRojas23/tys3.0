using System;

namespace CargaClic.API.Dtos.Seguimiento
{
    public class LiquidacionForUpdateDto
    {
        public int? idcliente { get; set; }
        public int? iddestinatario { get; set; }
        public int? iddestino { get; set; }
        public DateTime? fechainicio { get; set; }
        public DateTime? fechafin { get; set; }
        public long idorden { get; set; }
        public string archivo { get; set; }
        public string numcp { get; set; }
        public string grr { get; set; }
        public long idordentrabajo { get; set; }
        public DateTime? fechaentregaconciliacion { get; set; }
        public string horaentregaconciliacion { get; set; }
        public int idusuarioconciliacion { get; set; }
        public bool archivado { get; set; }
        public int idestado { get; set; }
        public int diastranscurridos { get; set; }

    }
}