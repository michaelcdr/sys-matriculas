using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SysMatriculas.Dominio.Requests
{
    [DataContract]
    public class DataTableRequest
    {
        [DataMember]
        public List<DataTableColumn> columns { get; set; }

        [DataMember]
        public int draw { get; set; }

        [DataMember]
        public int length { get; set; }

        [DataMember]
        public List<DataOrder> order { get; set; }

        [DataMember]
        public DataTableSearch search { get; set; }

        [DataMember]
        public int start { get; set; }
    }
}
