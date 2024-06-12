using Lanpuda.Server.Samples;
using Xunit;

namespace Lanpuda.Server.EntityFrameworkCore.Domains;

[Collection(ServerTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ServerEntityFrameworkCoreTestModule>
{

}
