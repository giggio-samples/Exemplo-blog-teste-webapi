using System.Linq;
using System.Web.Http;

namespace TesteWebAPI.Server
{
    public class ConjuntosController : ApiController
    {
        [HttpGet]
        public object Tail(int[] ns)
        {
            if (ns.Length == 0)
            {
                return new {mensagem = "Tail vazio"};
            }
            return ns.Skip(1).ToArray();
        }
    }
}
