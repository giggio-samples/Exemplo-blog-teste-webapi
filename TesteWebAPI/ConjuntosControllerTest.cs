using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace TesteWebAPI
{
    [TestClass]
    public class ConjuntosControllerTest : ControllerApiTesteBase
    {
        [TestMethod]
        public void TailComUmElemento()
        {
            using (var cliente = new HttpClient(servidor))
            {
                var conteudo = new[] {1};
                using (var request = CriarRequest("api/Conjuntos/Tail", HttpMethod.Get, conteudo))
                {
                    using (var response = cliente.SendAsync(request, new CancellationTokenSource().Token).Result)
                    {
                        response.Content.ReadAsStringAsync().Result.Should().Be(JsonConvert.SerializeObject(new int[] {}));
                    }
                }
            }
        }

        [TestMethod]
        public void TailComDoisElementos()
        {
            using (var cliente = new HttpClient(servidor))
            {
                var conteudo = new[] {1, 2, 3};
                using (var request = CriarRequest("api/Conjuntos/Tail", HttpMethod.Get, conteudo))
                {
                    using (var response = cliente.SendAsync(request, new CancellationTokenSource().Token).Result)
                    {
                        var retorno = response.Content.ReadAsStringAsync().Result;
                        retorno.Should().Be(JsonConvert.SerializeObject(new[] {2, 3}));
                        retorno.Should().Be("[2,3]");
                    }
                }
            }
        }

        [TestMethod]
        public void TailVazio()
        {
            using (var cliente = new HttpClient(servidor))
            {
                var conteudo = new int[] {};
                using (var request = CriarRequest("api/Conjuntos/Tail", HttpMethod.Get, conteudo))
                {
                    using (var response = cliente.SendAsync(request, new CancellationTokenSource().Token).Result)
                    {
                        response.Content.Should().NotBeNull();
                        response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
                        var retorno = response.Content.ReadAsStringAsync().Result;
                        retorno.Should().Be(JsonConvert.SerializeObject(new {mensagem = "Tail vazio"}));
                        retorno.Should().Be("{\"mensagem\":\"Tail vazio\"}");
                        Assert.AreEqual("Tail vazio", (string)((dynamic)JsonConvert.DeserializeObject(retorno)).mensagem);
                        Assert.AreEqual("Tail vazio", (string)retorno.DeserializarJson().mensagem);
                    }
                }
            }
        }
    }
}
