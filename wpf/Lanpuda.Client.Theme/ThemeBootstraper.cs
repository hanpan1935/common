using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Layouts.DefaultLayouts.Widgets.MainHeaders;
using Lanpuda.Client.Theme.Layouts.DefaultLayouts.Widgets.TreeMenus;
using Lanpuda.Client.Theme.Layouts.DefaultLayouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Client.Theme.Layouts.DefaultLayouts.Widgets.MainFooters;

namespace Lanpuda.Client.Theme
{
    public class ThemeBootstraper
    {
        private IModuleManager _moduleManager;
        private readonly IServiceProvider _serviceProvider;

        public ThemeBootstraper(IModuleManager moduleManager, IServiceProvider serviceProvider)
        {
            _moduleManager = moduleManager;
            _serviceProvider = serviceProvider;
        }


        public void Load()
        {
            var mainHeader = new Module(
               ThemeViewKeys.Header,
               () =>
               {
                   var viewModel = _serviceProvider.GetService<MainHeaderViewModel>();
                   return viewModel;
               },
               typeof(MainHeaderView)
               );


            var treeMenu = new Module(
                ThemeViewKeys.Menu,
                () =>
                {
                    var viewModel = _serviceProvider.GetService<TreeMenuViewModel>();
                    return viewModel;
                },
                typeof(TreeMenuView)
                );
    

            var defaultLayout = new Module(
               ThemeViewKeys.DefaultLayout,
               () =>
               {
                   var viewModel = _serviceProvider.GetService<DefaultLayoutViewModel>();
                   return viewModel;
               },
               typeof(DefaultLayoutView)
               );


            var mainFooter = new Module(
              ThemeViewKeys.Footer,
              () =>
              {
                  var viewModel = _serviceProvider.GetService<MainFooterViewModel>();
                  return viewModel;
              },
              typeof(MainFooterView)
              );

            _moduleManager.Register(RegionNames.MainWindow, defaultLayout);
            _moduleManager.Register(RegionNames.MainHeaderRegion, mainHeader);
            _moduleManager.Register(RegionNames.MenuTreeRegion, treeMenu);
            _moduleManager.Register(RegionNames.MainFooterRegion, mainFooter);
        }
    }
}
