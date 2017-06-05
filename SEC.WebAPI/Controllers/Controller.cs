using SEC.WebAPI.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SEC.WebAPI.Controllers
{
    public class Controller : ApiController
    {
        protected HttpResponseMessage Erro(string mensagemDeErro)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, EnvelopeRetorno.Erro(mensagemDeErro));
        }

        protected new HttpResponseMessage Ok()
        {
            return Request.CreateResponse(HttpStatusCode.OK, EnvelopeRetorno.Ok());
        }

        protected new HttpResponseMessage Ok<T>(T result)
        {
            return Request.CreateResponse(HttpStatusCode.OK, EnvelopeRetorno.Ok(result));
        }
    }
}