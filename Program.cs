
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Autofac;
using CommandLine;

namespace cXML.TypeGen
{
  public class Options
  {
    [Option('n', "namespace", Required = false, HelpText = "The namespace to generate C# classes for.", Default = "Api.cXML")]
    public string CSharpNamespace { get; set; }

    [Option('f', "filename", Required = false, HelpText = "The name of the output file to generate.", Default = "Model.cs")]
    public string OutputFileName { get; set; }

    [Option('o', "output", Required = false, HelpText = "The path to write output to.", Default = "./")]
    public string OutputDirectory { get; set; }

    [Option('d', "dtd", Required = false, HelpText = "The path to the cXML DTD file to use. If not provided, will be downloaded from the source. More info: http://cxml.org/downloads.html")]
    public string DtdPath { get; set; }

    [Option('c', "converter", Required = false, HelpText = "The path to the DTD-to-XSD converter to use. If not provided, will be downloaded from the source. More info: https://www.w3.org/2000/04/schema_hack/")]
    public string DtdToXsdConverterPath { get; set; }
  }

  class Program
  {
    static Task<int> Main(string[] args) => Parser.Default.ParseArguments<Options>(args).MapResult(Run, Fail);

    static async Task<int> Run(Options options)
    {
      Console.WriteLine($"output namespace: {options.CSharpNamespace}");
      Console.WriteLine($"output filename: {options.OutputFileName}");
      Console.WriteLine($"output directory: {options.OutputDirectory}");
      Console.WriteLine($"dtd path: {options.DtdPath ?? "<not specified; will download>"}");
      Console.WriteLine($"converter path: {options.DtdToXsdConverterPath ?? "<not specified; will download>"}");

      try
      {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new TypeGenModule());
        var generator = builder.Build().Resolve<TypeGenerator>();
        await generator.GenerateTypes(options);
      }
      catch(Exception ex)
      {
        Console.Error.WriteLine(ex);
        return -1;
      }

      return 0;
    }

    static Task<int> Fail(IEnumerable<Error> errors)
    {
      foreach (var error in errors)
      {
        Console.Error.WriteLine(error);
      }

      return Task.FromResult(-1);
    }
  }
}
