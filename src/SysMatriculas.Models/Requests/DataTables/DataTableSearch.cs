using System.Runtime.Serialization;

namespace SysMatriculas.Dominio.Requests
{
    public class DataTableSearch
    {
        [DataMember]
        public bool regex { get; set; }

        [DataMember]
        public string value { get; set; }
    }
}
