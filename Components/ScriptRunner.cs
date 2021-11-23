using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace cXML.TypeGen
{
  public interface IScriptRunner
  {
    string RunScript(string exe, string arguments);
  }

  public class ScriptRunner : IScriptRunner
  {
    public string RunScript(string exe, string arguments)
    {
      var outputLines = new List<string>();
      using var script = Process.Start(new ProcessStartInfo
      {
        FileName = "perl",
        Arguments = arguments,
        UseShellExecute = false,
        RedirectStandardOutput = true,
      });
      script.OutputDataReceived += (_, a) => outputLines.Add(a.Data);
      script.BeginOutputReadLine();
      script.WaitForExit();
      return string.Join(Environment.NewLine, outputLines);
    }
  }
}
