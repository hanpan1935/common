using Lanpuda.Client.Theme.Services.MenuServices;
using Lanpuda.Client.Theme.Services.SettingsServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Lanpuda.Client.Theme
{
    [DependsOn(typeof(AbpAutofacModule))]
    public class ThemeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IMenuService, MenuService>();
            context.Services.AddSingleton<ISettingsService, SettingsService>();
            var manager = DevExpress.Mvvm.ModuleInjection.ModuleManager.DefaultManager;
            context.Services.AddSingleton<DevExpress.Mvvm.ModuleInjection.IModuleManager>(manager);
            context.Services.AddSingleton<ThemeBootstraper>();
        }


        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }


        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);

            var bootstraper = context.ServiceProvider.GetRequiredService<ThemeBootstraper>();
            bootstraper.Load();
        }
    }
}
