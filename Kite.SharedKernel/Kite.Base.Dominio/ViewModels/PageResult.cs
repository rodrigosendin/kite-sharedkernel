using System.Collections.Generic;
using Kite.Base.Dominio.Entidades;

namespace Kite.Base.Dominio.ViewModels
{
    public class PageResult<T> where T : IEntidade
    {
        public int PaginaAtual { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }

        public string Anterior { get; set; }
        public string Proxima { get; set; }

        public IList<T> Resultado { get; set; }
    }
}