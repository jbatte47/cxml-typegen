using System;
using System.IO;

namespace cXML.TypeGen
{
  public interface IPathHelper
  {
    string GetTempPath();
  }

  public class PathHelper : IPathHelper
  {
    public string GetTempPath()
    {
      return Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
    }
  }
}
