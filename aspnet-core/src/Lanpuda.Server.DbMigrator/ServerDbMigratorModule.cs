using Lanpuda.Server.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lanpuda.Server.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ServerEntityFrameworkCoreModule),
    typeof(ServerApplicationContractsModule)
    )]
public class ServerDbMigratorModule : AbpModule
{
}
