using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SysMatriculas.Persistencia.DTOs.DataTables;

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

[DataContract]
public class DataTableRequestDisciplinas : DataTableRequest
{ 
    public string Pesquisa { get; set; }
    public int? CurriculoId { get; set; }
}

public class DataOrder
{
    [DataMember]
    public int column { get; set; }

    [DataMember]
    public string dir { get; set; }
}

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
public class DataTableSearch
{
    [DataMember]
    public bool regex { get; set; }

    [DataMember]
    public string value { get; set; }
}

public class DataTableResult<T>
{
    public int Draw { get; set; }
    public int RecordsTotal { get; set; }
    public int RecordsFiltered { get; set; }
    public IEnumerable<T> Data { get; set; }
}