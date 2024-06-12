using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Account.UI.Login;
using Lanpuda.Account.UI.Manage;
using Lanpuda.Client.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Lanpuda.Account.UI
{
    public class AccountUIBootstraper
    {
        private IModuleManager _moduleManager;
        private readonly IServiceProvider _serviceProvider;
        public AccountUIBootstraper(IModuleManager moduleManager, IServiceProvider serviceProvider)
        {
            _moduleManager = moduleManager;
            _serviceProvider = serviceProvider;
        }


        public void Load()
        {
            var login = new Module(AccountUIViewKeys.Login, () =>
            {
                try
                {
                    var viewModel = _serviceProvider.GetService<LoginViewModel>();
                    return viewModel;
                }
                catch (Exception)
                {
                    throw;
                }
            },
            typeof(LoginView)
            );
            _moduleManager.Register(RegionNames.MainWindow, login);

            var manage = new Module(AccountUIViewKeys.Manage, () =>
            {
                var viewModel = _serviceProvider.GetService<ManageViewModel>();
                return viewModel;
            },
            typeof(ManageView)
            );
            _moduleManager.Register(RegionNames.MainContentRegion, manage);
        }
    }
}
