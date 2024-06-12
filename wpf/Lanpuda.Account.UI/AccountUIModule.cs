using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp;
using Lanpuda.Client.Theme;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Http.Client.IdentityModel;
using Lanpuda.Client.Theme.Services.MenuServices;
using Volo.Abp.Identity;
using Lanpuda.Client.Theme.Entities;

namespace Lanpuda.Account.UI
{
    [DependsOn(typeof(ThemeModule))]
    [DependsOn(typeof(AbpHttpClientIdentityModelModule))]  
    [DependsOn(typeof(AbpAccountHttpApiClientModule))]
    [DependsOn(typeof(AbpAspNetCoreMvcContractsModule))]
    public class AccountUIModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<AccountUIBootstraper>();

            //context.Services.AddStaticHttpClientProxies(typeof(AbpAspNetCoreMvcClientCommonModule).Assembly );
        }


        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }


        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);

            var bootstraper = context.ServiceProvider.GetRequiredService<AccountUIBootstraper>();
            bootstraper.Load();

            var menuService = context.ServiceProvider.GetRequiredService<IMenuService>();
            List<MenuItem> menuItems = new List<MenuItem>();
            menuService.SetMenusByModuleKey("AccountUI", menuItems);

            MenuItem profileMenu = new MenuItem() { MenuHeader = "个人设置" ,TargetKey = AccountUIViewKeys.Manage };
            menuItems.Add(profileMenu);
        }
    }
}
