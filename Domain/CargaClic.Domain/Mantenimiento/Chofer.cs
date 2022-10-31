using CargaClic.Common;

namespace CargaClic.Domain.Mantenimiento
{
    public class Chofer : Entity
    {
        public int? idchofer { get; set; }
        public string nombrechofer { get; set; }
        public string apellidochofer { get; set; }
        public string dni { get; set; }
        public string brevete { get; set; }

    }
}