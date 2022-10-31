namespace CargaClic.Repository.Contracts.Seguimiento
{
    public class ListarArchivosResult
    {
        public long idarchivo { get; set; }
        public long idordentrabajo { get; set; }
        public string nombrearchivo { get; set; }
        public string rutaacceso { get; set; }
        public string extension { get; set; }

    }
}