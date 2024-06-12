using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.Identity.UI.Organizations;
using Lanpuda.Identity.UI.Roles;
using Lanpuda.Identity.UI.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Identity.UI
{
    public class IdentityUIBootstraper
    {
        private IModuleManager _moduleManager;
        private readonly IServiceProvider _serviceProvider;

        public IdentityUIBootstraper(IModuleManager moduleManager, IServiceProvider serviceProvider)
        {
            _moduleManager = moduleManager;
            _serviceProvider = serviceProvider;
        }


        public void Load()
        {
            var user = new Module(IdentityUIViewKeys.User_Paged, () =>
            {
                var viewModel = _serviceProvider.GetService<UserPagedViewModel>();
                return viewModel;
            },
           typeof(UserPagedView)
           );
            _moduleManager.Register(RegionNames.MainContentRegion, user);


            var role = new Module(IdentityUIViewKeys.Role_Paged, () =>
            {
                var viewModel = _serviceProvider.GetService<RolePagedViewModel>();
                return viewModel;
            },
            typeof(RolePagedView)
            );
            _moduleManager.Register(RegionNames.MainContentRegion, role);



            var org = new Module(IdentityUIViewKeys.Organization, () =>
            {
                var viewModel = _serviceProvider.GetService<OrganizationViewModel>();
                return viewModel;
            },
            typeof(OrganizationView)
            );
            _moduleManager.Register(RegionNames.MainContentRegion, org);


        }

    }
}
