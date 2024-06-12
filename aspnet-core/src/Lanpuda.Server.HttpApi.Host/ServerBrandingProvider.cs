using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Lanpuda.Server;

[Dependency(ReplaceServices = true)]
public class ServerBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Server";
}
