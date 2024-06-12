using Lanpuda.Client.Theme.ACL;
using Lanpuda.Client.Theme.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Client.Theme.Services.MenuServices
{
    public class MenuService : IMenuService
    {
        private List<AppModule> _appModules;
        private Dictionary<string, List<MenuItem>>  _moduleMenuItems { get; set; }  


        public MenuService()
        {
            _moduleMenuItems = new Dictionary<string, List<MenuItem>>();
            _appModules = new List<AppModule>();
        }


        public void SetMenusByModuleKey(string moduleName,List<MenuItem> menuItems)
        {
            _moduleMenuItems[moduleName] = menuItems;
        }

        public List<MenuItem> GetMenusByModuleKey(string moduleKey)
        {

            var menuItems = _moduleMenuItems[moduleKey];
            List<MenuItem> grantedMenu = GetGrandMenu(menuItems);
            return grantedMenu;
        }
        public void ClearByModuleKey(string moduleName)
        {
            _moduleMenuItems[moduleName].Clear();
        }

        public List<MenuItem> GetAllMenus()
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            foreach (var item in _moduleMenuItems)
            {
                menuItems.AddRange(item.Value);
            }
            return menuItems;
        }

        public List<Entities.MenuItem> GetDefaultModuleMenus()
        {
            var defaultModule = _appModules.Where(m=>m.IsDefault == true).FirstOrDefault();
            if (defaultModule == null)
            {
                return new List<MenuItem>();
            }

            string moduleKey = defaultModule.Key;
            var defaultModuleMenus = _moduleMenuItems[moduleKey];

            if (defaultModuleMenus != null)
            {
                List<Entities.MenuItem> grantedMenu = GetGrandMenu(defaultModuleMenus);
                return grantedMenu;
            }
            else
            {
                return new List<MenuItem>();
            }
        }
        private List<Entities.MenuItem> GetGrandMenu(List<MenuItem> menuItems)
        {
            List<MenuItem> grantedMenu = new List<MenuItem>();
            foreach (var menu in menuItems)
            {
                if (!string.IsNullOrEmpty(menu.PermissionName))
                {
                    if (ClientPermissions.GrantedPolicies.ContainsKey(menu.PermissionName))
                    {
                        MenuItem menuItem = new MenuItem();
                        menuItem.MenuHeader = menu.MenuHeader;
                        menuItem.TargetKey = menu.TargetKey;
                        menuItem.MenuIcon = menu.MenuIcon;
                        menuItem.PermissionName = menu.PermissionName;
                        menuItem.LocalizationKey = menu.LocalizationKey;
                        grantedMenu.Add(menuItem);

                        SetChildrenMenu(menuItem, menu.Children);
                    }
                }
                else
                {
                    MenuItem menuItem = new MenuItem();
                    menuItem.MenuHeader = menu.MenuHeader;
                    menuItem.TargetKey = menu.TargetKey;
                    menuItem.MenuIcon = menu.MenuIcon;
                    menuItem.PermissionName = menu.PermissionName;
                    menuItem.LocalizationKey = menu.LocalizationKey;
                    grantedMenu.Add(menuItem);
                    SetChildrenMenu(menuItem, menu.Children);
                }
            }

            return grantedMenu;
        }

        private void SetChildrenMenu(MenuItem prentMenu, List<MenuItem> childrenMenus)
        {
            foreach (var childMenu in childrenMenus)
            {
                if (!string.IsNullOrEmpty(childMenu.PermissionName))
                {
                    if (ClientPermissions.GrantedPolicies.ContainsKey(childMenu.PermissionName))
                    {
                        MenuItem menuItem = new MenuItem();
                        menuItem.MenuHeader = childMenu.MenuHeader;
                        menuItem.TargetKey = childMenu.TargetKey;
                        menuItem.MenuIcon = childMenu.MenuIcon;
                        menuItem.PermissionName = childMenu.PermissionName;
                        menuItem.LocalizationKey = childMenu.LocalizationKey;
                        prentMenu.Children.Add(menuItem);

                        SetChildrenMenu(menuItem, childMenu.Children);
                    }
                }
                else
                {

                    MenuItem menuItem = new MenuItem();
                    menuItem.MenuHeader = childMenu.MenuHeader;
                    menuItem.TargetKey = childMenu.TargetKey;
                    menuItem.MenuIcon = childMenu.MenuIcon;
                    menuItem.PermissionName = childMenu.PermissionName;
                    menuItem.LocalizationKey = childMenu.LocalizationKey;
                    prentMenu.Children.Add(menuItem);

                    SetChildrenMenu(menuItem, childMenu.Children);
                }
            }
        }



        public void SetAppModules(List<AppModule> appModules)
        {
            this._appModules.Clear();
            foreach (var item in appModules)
            {
                _appModules.Add(item);
            }
        }

        public List<AppModule> GetAllAppModules()
        {
            return _appModules;
        }


        public void AddAppModule(AppModule appModule)
        {
            this._appModules.Add(appModule);
        }

    }
}
