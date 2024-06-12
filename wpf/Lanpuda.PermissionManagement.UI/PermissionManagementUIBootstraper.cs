using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.PermissionManagement.UI.Permissions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.PermissionManagement.UI
{
    public class PermissionManagementUIBootstraper
    {
        private IModuleManager _moduleManager;
        private readonly IServiceProvider _serviceProvider;

        public PermissionManagementUIBootstraper(IModuleManager moduleManager, IServiceProvider serviceProvider)
        {
            _moduleManager = moduleManager;
            _serviceProvider = serviceProvider;
        }


        public void Load()
        {
            var permission = new Module(PermissionManagementUIViewKeys.Permission, () =>
            {
                var viewModel = _serviceProvider.GetService<PermissionViewModel>();
                return viewModel;
            },
           typeof(PermissionView)
           );
            _moduleManager.Register(RegionNames.MainContentRegion, permission);


            
        }
    }
}
