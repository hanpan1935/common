using Volo.Abp.Modularity;

namespace Lanpuda.Server;

[DependsOn(
    typeof(ServerApplicationModule),
    typeof(ServerDomainTestModule)
)]
public class ServerApplicationTestModule : AbpModule
{

}
