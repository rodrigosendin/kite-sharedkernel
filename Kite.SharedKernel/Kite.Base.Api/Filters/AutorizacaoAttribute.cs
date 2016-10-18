using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Kite.Base.Api.Token;

namespace Kite.Base.Api.Filters
{
    public class AutorizacaoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Dominio.ViewModels.Token token = null;
            try
            {
                token = actionContext.RecuperarToken();
            }
            catch (Exception)
            {
                TokenExpirado(actionContext);
            }

            if (token == null || DateTime.Now.Subtract(token.Data) > TimeSpan.FromHours(8))
                TokenExpirado(actionContext);

            base.OnActionExecuting(actionContext);
        }

        public static void TokenExpirado(HttpActionContext context)
        {
            context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized, 
                "Usuário não Autorizado!");
        }
    }
}
