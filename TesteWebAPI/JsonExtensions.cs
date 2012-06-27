using Newtonsoft.Json.Linq;

namespace TesteWebAPI
{
    public static class JsonExtensions
    {
        public static dynamic DeserializarJson(this string texto)
        {
            return JObject.Parse(texto);
        }
    }
}
