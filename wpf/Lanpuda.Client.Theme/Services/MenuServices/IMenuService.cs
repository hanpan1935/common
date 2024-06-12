using Lanpuda.Client.Theme.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Client.Theme.Services.MenuServices
{
    public interface IMenuService
    {
        public void SetMenusByModuleKey(string moduleName, List<MenuItem> menuItems);

        public void ClearByModuleKey(string moduleName);

        List<MenuItem> GetMenusByModuleKey(string moduleName);

        List<MenuItem> GetAllMenus();

        List<MenuItem> GetDefaultModuleMenus();

        void SetAppModules(List<AppModule> appModules);
        void AddAppModule(AppModule appModule);
        List<AppModule> GetAllAppModules();
    }
}
