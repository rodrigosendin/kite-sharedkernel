using System;
using System.Web.Http;
using System.Web.Http.Routing;
using Kite.Base.Api.Filters;
using Kite.Base.Api.Token;
using Kite.Base.Dominio.Entidades;
using Kite.Base.Dominio.Servicos;
using Kite.Base.Dominio.Util;
using Kite.Base.Util;

namespace Kite.Base.Api.Controllers
{
    [Autorizacao]
    public class ControllerBase<T> : ControllerBaseConsulta<T> where T : EntidadeBase, IAggregateRoot
    {
        protected new Servico<T> Servico { get; set; }

        public ControllerBase()
        {
            Servico = Kernel.Get<Servico<T>>();
        }

        public virtual IHttpActionResult Post([FromBody]T entidade)
        {
            try
            {
                var token = ActionContext.RecuperarToken();

                var ok = Servico.Inclui((T)entidade.VincularColecoes(), token.Login);
                if (ok == false)
                    return BadRequest(Servico.Mensagens.ToMessageBoxString());

                var helper = new UrlHelper(Request);
                var location = helper.Link("DefaultApi", new { id = entidade.Id });

                return Created(location, entidade);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public virtual IHttpActionResult Put([FromBody]T entidade)
        {
            try
            {
                var token = ActionContext.RecuperarToken();

                var existe = Servico.Retorna(entidade.Id) != null;
                if (existe == false)
                    return NotFound();

                var ok = Servico.Altera((T)entidade.VincularColecoes(), token.Login);
                if (ok == false)
                    return BadRequest(Servico.Mensagens.ToMessageBoxString());

                return Ok(entidade);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public virtual IHttpActionResult Delete(long id)
        {
            try
            {
                var entidade = Servico.Retorna(id);
                if (entidade == null)
                    return NotFound();

                var ok = Servico.Exclui(id);
                if (ok == false)
                    return BadRequest(Servico.Mensagens.ToMessageBoxString());

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}