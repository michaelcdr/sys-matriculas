using System.Runtime.Serialization;

namespace SysMatriculas.Dominio.Requests
{
    public class DataTableColumn
    {
        [DataMember]
        public int data { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public bool orderable { get; set; }

        [DataMember]
        public bool searchable { get; set; }

        [DataMember]
        public DataTableSearch search { get; set; }
    }
}
