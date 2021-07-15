using System.Runtime.Serialization;

namespace SysMatriculas.Dominio.Requests
{
    public class DataOrder
    {
        [DataMember]
        public int column { get; set; }

        [DataMember]
        public string dir { get; set; }
    }
}
