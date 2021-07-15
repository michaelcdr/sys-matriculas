using System.Collections.Generic;

namespace SysMatriculas.Dominio.Responses
{
    public class ColecaoPaginada<T> where T : class
    {
        public IEnumerable<T> Itens { get; set; }

        public int RecordsTotal { get; set; }

        public int RecordsFiltered { get; set; }

        public ColecaoPaginada(int recordTotal, int recordsFiltered, IEnumerable<T> itens)
        {
            RecordsFiltered = recordsFiltered;
            RecordsTotal = RecordsTotal;
            Itens = itens;
        }
    }
}
