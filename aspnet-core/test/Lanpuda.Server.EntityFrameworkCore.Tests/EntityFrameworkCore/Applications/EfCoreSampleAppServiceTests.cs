using Lanpuda.Server.Samples;
using Xunit;

namespace Lanpuda.Server.EntityFrameworkCore.Applications;

[Collection(ServerTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ServerEntityFrameworkCoreTestModule>
{

}
