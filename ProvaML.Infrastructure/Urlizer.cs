using System.Web;

namespace ProvaML.Infrastructure
{
    public static class Urlizer
    {
        public static string Sanitizar(string entrada)
        {
            return HttpUtility.HtmlEncode(entrada.Replace(" ", "").Trim().ToLower());
        }
    }
}