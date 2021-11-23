using System;
using System.Threading.Tasks;

namespace cXML.TypeGen
{
    public class TypeGenerator
  {
    private readonly IcXmlDtdDownloader _cXmlDtdDownloader;
    private readonly IDtd2XsdDownloader _dtd2XsdDownloader;
    private readonly IScriptRunner _scriptRunner;
    private readonly IFileHandler _fileHandler;

    public TypeGenerator(IcXmlDtdDownloader cXmlDtdDownloader, IDtd2XsdDownloader dtd2XsdDownloader, IScriptRunner scriptRunner, IFileHandler fileHandler)
    {
      _cXmlDtdDownloader = cXmlDtdDownloader;
      _dtd2XsdDownloader = dtd2XsdDownloader;
      _scriptRunner = scriptRunner;
      _fileHandler = fileHandler;
    }

    public async Task GenerateTypes(Options options)
    {
      var converterPath = await _dtd2XsdDownloader.Download(options.DtdToXsdConverterPath);
      var dtdPath = await _cXmlDtdDownloader.Download(options.DtdPath);
      var xsd = _scriptRunner.RunScript("perl", $"{converterPath} {dtdPath}");
      var xsdPath = await _fileHandler.StoreLocalFile(xsd);

      Console.WriteLine($"used {converterPath} with {dtdPath} to generate {xsdPath}");
    }
  }
}
