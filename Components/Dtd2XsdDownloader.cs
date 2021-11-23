using System.IO;
using System.Threading.Tasks;

namespace cXML.TypeGen
{
  public interface IDtd2XsdDownloader
  {
    Task<string> Download(string scriptPath);
  }

    public class Dtd2XsdDownloader : IDtd2XsdDownloader
    {
      private readonly IPathHelper _pathHelper;
      private readonly IFileHandler _fileRetriever;

      public Dtd2XsdDownloader(IPathHelper pathHelper, IFileHandler fileRetriever)
      {
        _pathHelper = pathHelper;
        _fileRetriever = fileRetriever;
      }
      public async Task<string> Download(string scriptPath)
      {
        if (string.IsNullOrEmpty(scriptPath) || !File.Exists(scriptPath))
        {
          scriptPath = await _fileRetriever.DownloadRemoteFile("https://www.w3.org/2000/04/schema_hack/dtd2xsd.pl");
        }

        return scriptPath;
      }
    }
}
