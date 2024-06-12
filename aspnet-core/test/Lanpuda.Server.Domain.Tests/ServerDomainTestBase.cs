using Volo.Abp.Modularity;

namespace Lanpuda.Server;

/* Inherit from this class for your domain layer tests. */
public abstract class ServerDomainTestBase<TStartupModule> : ServerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
