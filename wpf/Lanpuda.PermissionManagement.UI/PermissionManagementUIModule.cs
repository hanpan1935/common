using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Client.Theme;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.PermissionManagement;

namespace Lanpuda.PermissionManagement.UI
{
    [DependsOn(typeof(ThemeModule))]
    [DependsOn(typeof(AbpHttpClientIdentityModelModule))]
    [DependsOn(typeof(AbpPermissionManagementHttpApiClientModule))]
    public class PermissionManagementUIModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<PermissionManagementUIBootstraper>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }


        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);
            var bootstraper = context.ServiceProvider.GetRequiredService<PermissionManagementUIBootstraper>();
            bootstraper.Load();
        }
    }
}
