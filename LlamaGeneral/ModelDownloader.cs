using HtmlAgilityPack;
using LlamaGeneral.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LlamaGeneral
{
    public class ModelDownloader
    {
        // html/body/div/main/div[2]/section/div[3]/ul/li[3]/div/a
        // html/body/div/main/div[2]/section/div[3]/ul/li[3]/div/a/span

        public void DownloadModel(string url, string path)
        {
            path = Path.Combine(path, GetRepositoryFromDownloadUrl(url));
            Directory.CreateDirectory(path);
            path = Path.Combine(path, url.Split('/').Last());
            var client = new HttpClient();
            var stream = client.GetStreamAsync(url).GetAwaiter().GetResult();
            var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            stream.CopyTo(fs);
            fs.Flush();
            fs.Close();
        }
        public void DownloadModel(string repository, string version, string path)
        {
            var url = GetDownloadUrl(repository, version);
            DownloadModel(url, path);
        }

        public IEnumerable<string> GetVersionsOfModel(string repository) {
            
            var document = new HtmlDocument();
            document.FromUrl(GetRepoUrl(repository));
            var table = document.DocumentNode.SelectSingleNode("html/body/div/main/div[2]/section/div[3]/ul");
            List<string> versions = new();
            for (int i = 1; i < table.ChildNodes.Count; i++)
            {
                if (table.ChildNodes[i].Name != "li") continue;

                var version = table.SelectSingleNode($"li[{i}]/div/a/span").InnerText;
                if (version[^4..] == ".bin")
                    versions.Add(version);
            }
            return versions;
        }

        private string GetRepoUrl(string repository) => $"https://huggingface.co/{repository}/tree/main";
        private string GetDownloadUrl(string repository, string version) => $"https://huggingface.co/{repository}/resolve/main/{version}";
        public string GetRepositoryFromDownloadUrl(string url) => string.Join("/", url.Split('/').Take(new Range(new Index(3), new Index(5))));
    }
}
