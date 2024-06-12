using Lanpuda.Server.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lanpuda.Server.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ServerController : AbpControllerBase
{
    protected ServerController()
    {
        LocalizationResource = typeof(ServerResource);
    }
}
