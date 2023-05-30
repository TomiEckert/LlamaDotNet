using LLama;

namespace LlamaGeneral;

public class Models
{
    public static LLamaModel GetModel(string repository, string name, string path)
    {
        path = GetPathFromRepositoryAndName(repository, name, path);
        return GetModel(path);
    }

    public static LLamaModel GetModel(string path)
    {
        var parameters = new LLamaParams(
                model: path,
                n_gpu_layers: 50,
                n_ctx: 2048,
                n_batch: 2048,
                interactive: true
            );

        return new LLamaModel(parameters);
    }

    public static IEnumerable<string> GetRepositories(string path)
    {
        var result = new List<string>();
        var authors = Directory.GetDirectories(path);
        foreach (var author in authors)
        {
            var models = Directory.GetDirectories(author);
            foreach (var model in models)
            {
                result.Add(model.Replace(path + "\\", "").Replace("\\", "/"));
            }
        }
        return result;
    }

    public static IEnumerable<string> GetVersions(string repository, string path)
    {
        var result = new List<string>();
        var versions = Directory.GetFiles(Path.Combine(path, repository));
        foreach (var version in versions)
        {
            result.Add(version.Split('\\').Last());
        }
        return result;
    }

    private static string GetPathFromRepositoryAndName(string repository, string name, string path)
    {
        return Path.Combine(path, repository, name);
    }
}
