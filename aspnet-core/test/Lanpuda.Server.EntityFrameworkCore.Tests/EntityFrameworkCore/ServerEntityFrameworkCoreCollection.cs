using Xunit;

namespace Lanpuda.Server.EntityFrameworkCore;

[CollectionDefinition(ServerTestConsts.CollectionDefinitionName)]
public class ServerEntityFrameworkCoreCollection : ICollectionFixture<ServerEntityFrameworkCoreFixture>
{

}
