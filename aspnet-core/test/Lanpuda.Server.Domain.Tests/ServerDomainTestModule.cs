using Volo.Abp.Modularity;

namespace Lanpuda.Server;

[DependsOn(
    typeof(ServerDomainModule),
    typeof(ServerTestBaseModule)
)]
public class ServerDomainTestModule : AbpModule
{

}
