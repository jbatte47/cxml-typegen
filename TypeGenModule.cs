using Autofac;

namespace cXML.TypeGen
{
  public class TypeGenModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(Program).Assembly).AsSelf().AsImplementedInterfaces();
    }
  }
}
