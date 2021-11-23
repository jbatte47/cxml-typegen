using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace cXML.TypeGen
{
  public interface IFileHandler
  {
    Task<string> DownloadRemoteFile(string url);
    Task<string> ReadLocalFile(string path);
    Task<string> StoreLocalFile(string contents);
    string ExtractCompressedFile(string path, string fileName);
    bool IsMissing(string path);
  }

  public class FileHandler : IFileHandler
  {
    private readonly IPathHelper _pathHelper;

    public FileHandler(IPathHelper pathHelper)
    {
      _pathHelper = pathHelper;
    }

    public Task<string> ReadLocalFile(string path)
    {
      return File.ReadAllTextAsync(path);
    }

    public async Task<string> DownloadRemoteFile(string url)
    {
      var path = _pathHelper.GetTempPath();
      await new WebClient().DownloadFileTaskAsync(url, path);
      return path;
    }

    public async Task<string> StoreLocalFile(string contents)
    {
      var path = _pathHelper.GetTempPath();
      await File.WriteAllTextAsync(path, contents);
      return path;
    }

    public string ExtractCompressedFile(string path, string fileName)
    {
      using var archive = ZipFile.OpenRead(path);
      var cxmlEntry = archive.Entries.Single(e => e.Name == fileName);
      var tmpDtdPath = _pathHelper.GetTempPath();
      cxmlEntry.ExtractToFile(tmpDtdPath);
      return tmpDtdPath;
    }

    public bool IsMissing(string path) => string.IsNullOrEmpty(path) || !File.Exists(path);
  }
}
