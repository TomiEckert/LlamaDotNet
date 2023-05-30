using HtmlAgilityPack;

namespace LlamaGeneral.Extensions
{
    internal static class HtmlExtension
    {

        public static void FromUrl(this HtmlDocument document, string url)
        {
            using var client = new HttpClient();
            document.LoadHtml(client.GetStringAsync(url).GetAwaiter().GetResult());
        }
    }
}