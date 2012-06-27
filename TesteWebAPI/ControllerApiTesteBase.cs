using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TesteWebAPI
{
    [TestClass]
    public abstract class ControllerApiTesteBase
    {
        protected HttpServer servidor;
        protected const string urlBase = "http://algumaurl.com/";

        [TestInitialize]
        public void Setup()
        {
            var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional});
            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
            servidor = new HttpServer(config);
        }

        [TestCleanup]
        public void DesfazServer()
        {
            if (servidor != null)
                servidor.Dispose();
        }

        protected HttpRequestMessage CriarRequest(string url, HttpMethod method, object content = null, string mediaType = "application/json", MediaTypeFormatter formatter = null)
        {
            var request = new HttpRequestMessage { RequestUri = new Uri(urlBase + url), Method = method };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            if (content!=null)
                request.Content = new ObjectContent(content.GetType(), content, formatter ?? new JsonMediaTypeFormatter());
            return request;
        }
    }
}
