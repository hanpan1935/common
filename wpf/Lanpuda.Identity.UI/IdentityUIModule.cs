using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp;
using Lanpuda.Client.Theme;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Http.Client.IdentityModel;
using Lanpuda.PermissionManagement.UI;
using DevExpress.Mvvm.UI;
using System.Reflection;
using Lanpuda.Client.Theme.Services.MenuServices;
using Lanpuda.Client.Theme.Entities;

namespace Lanpuda.Identity.UI
{
    [DependsOn(typeof(ThemeModule))]
    [DependsOn(typeof(AbpHttpClientIdentityModelModule))]
    [DependsOn(typeof(AbpIdentityHttpApiClientModule))]
    [DependsOn(typeof(PermissionManagementUIModule))]
    public class IdentityUIModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IdentityUIBootstraper>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }


        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);
            var bootstraper = context.ServiceProvider.GetRequiredService<IdentityUIBootstraper>();
            bootstraper.Load();

            var menuService = context.ServiceProvider.GetRequiredService<IMenuService>();
            List<MenuItem> menuItems = new List<MenuItem>();
            menuService.SetMenusByModuleKey("IdentityUI", menuItems);

            MenuItem identityMenu = new MenuItem() { MenuHeader = "身份标识" };
            //
            MenuItem userMenu = new MenuItem() { MenuHeader = "用户管理", TargetKey = IdentityUIViewKeys.User_Paged, PermissionName = IdentityPermissions.Users.Default };

            MenuItem roleMenu = new MenuItem() { MenuHeader = "角色管理", TargetKey = IdentityUIViewKeys.Role_Paged, PermissionName = IdentityPermissions.Roles.Default };

            identityMenu.Children.Add(userMenu);
            identityMenu.Children.Add(roleMenu);
            menuItems.Add(identityMenu);
        }
    }
}
