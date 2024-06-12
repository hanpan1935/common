using Volo.Abp.Modularity;

namespace Lanpuda.Server;

public abstract class ServerApplicationTestBase<TStartupModule> : ServerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
