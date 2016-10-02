using System.Web.Http;
using System.Web.Http.Routing;
using Kite.Base.Api.Filters;
using Kite.Base.Dominio.Entidades;
using Kite.Base.Dominio.Servicos;
using Kite.Base.Dominio.Util;
using Kite.Base.Util;

namespace Kite.Base.Api.Controllers
{
    [Autorizacao]
    public class ControllerBaseConsulta<T> : ApiController where T : EntidadeBase
    {
        protected ServicoConsulta<T> Servico { get; set; }

        public ControllerBaseConsulta()
        {
            Servico = Kernel.Get<ServicoConsulta<T>>();
        }

        public virtual IHttpActionResult Get()
        {
            var entidades = Servico.Consulta();

            if (entidades == null)
                return BadRequest(Servico.Mensagens.ToMessageBoxString());

            return Ok(entidades);
        }

        public virtual IHttpActionResult Get(long id)
        {
            var entidade = Servico.Retorna(id);

            if (entidade == null)
                return NotFound();

            return Ok(entidade);
        }

        public virtual IHttpActionResult GetPage(int page)
        {
            var result = Servico.ConsultaPaginada(page);

            if (result == null)
                return BadRequest(Servico.Mensagens.ToMessageBoxString());

            var helper = new UrlHelper(Request);

            if (page > 0)
                result.Anterior = helper.Link("DefaultApi", new { page = page - 1 });
            else
                result.Anterior = string.Empty;

            if (page < result.TotalPaginas)
                result.Proxima = helper.Link("DefaultApi", new { page = page + 1 });
            else
                result.Proxima = string.Empty;

            return Ok(result);
        }
    }
}