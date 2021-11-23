using System.Threading.Tasks;

namespace cXML.TypeGen
{
  public interface IcXmlDtdDownloader
  {
    Task<string> Download(string dtdPath);
  }

  public class cXmlDtdDownloader : IcXmlDtdDownloader
  {
    private readonly IPathHelper _pathHelper;
    private readonly IFileHandler _file;

    public cXmlDtdDownloader(IPathHelper pathHelper, IFileHandler file)
    {
      _pathHelper = pathHelper;
      _file = file;
    }

    public async Task<string> Download(string dtdPath)
    {
      if (_file.IsMissing(dtdPath))
      {
        var tmpZipPath = await _file.DownloadRemoteFile("http://xml.cxml.org/current/cXML_DTDs.zip");
        dtdPath = _file.ExtractCompressedFile(tmpZipPath, "cXML.dtd");
      }

      return dtdPath;
    }
  }
}
