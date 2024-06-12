using DevExpress.Mvvm;
using Lanpuda.Client.Theme.ACL;
using Lanpuda.Client.Theme.Entities;
using Lanpuda.Client.Theme.Services.MenuServices;
using Lanpuda.Client.Theme.Services.SettingsServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Volo.Abp.DependencyInjection;

namespace Lanpuda.Client.Theme.Layouts.DefaultLayouts.Widgets.TreeMenus
{
    public class TreeMenuViewModel : ViewModelBase,ITransientDependency
    {
        private readonly ISettingsService _settingsService;
        private readonly IMenuService _menuService;
        public string CurrentUserName { get; set; }
        public string CurrentUserEmail { get; set; }
        public BitmapImage Avatar { get; set; }
        public ObservableCollection<MenuItem> MenuList { get; set; }


        public TreeMenuViewModel(ISettingsService settingsService, IMenuService menuService)
        {
            _settingsService = settingsService;
            _menuService = menuService;
            CurrentUserName = _settingsService.GetSurname() + _settingsService.GetName();
            CurrentUserEmail = _settingsService.GetUserEmail();
            Avatar = _settingsService.GetUserAvatar();
            Messenger.Default.Register<AppModule>(this, OnModuleChanged);
            MenuList = new ObservableCollection<MenuItem>();
            OnInitial();
        }


        private void OnInitial()
        {
            var menusList = _menuService.GetDefaultModuleMenus();
            this.MenuList.Clear();
            foreach (var item in menusList)
            {
                MenuList.Add(item); 
            }
            var frist = MenuList.FirstOrDefault();
            if (frist != null)
            {
                frist.OpenViewCommand.Execute(null);
            }
        }


        private void OnModuleChanged(AppModule message)
        {

            var menus = _menuService.GetMenusByModuleKey(message.Key);

            this.MenuList.Clear();
            foreach (var item in menus)
            {
                MenuList.Add(item);
            }
            var frist = MenuList.FirstOrDefault();
            if (frist != null)
            {
                if (string.IsNullOrEmpty(frist.TargetKey) )
                {
                    frist.IsExpanded = true;
                    var children = frist.Children.FirstOrDefault();
                    if (children != null)
                    {
                        children.OpenViewCommand.Execute(null);
                    }
                }
                else
                {
                    frist.OpenViewCommand.Execute(null);
                }
            }

            //var allMenus = _menuService.GetAllMenus();

            //List<MenuItem> grantedMenu = new List<MenuItem>();
            //foreach (var menu in allMenus)
            //{
            //    if (!string.IsNullOrEmpty(menu.PermissionName))
            //    {
            //        if (ClientPermissions.GrantedPolicies.ContainsKey(menu.PermissionName))
            //        {
            //            MenuItem menuItem = new MenuItem();
            //            menuItem.MenuHeader = menu.MenuHeader;
            //            menuItem.TargetKey = menu.TargetKey;
            //            menuItem.MenuIcon = menu.MenuIcon;
            //            menuItem.PermissionName = menu.PermissionName;
            //            menuItem.LocalizationKey = menu.LocalizationKey;
            //            grantedMenu.Add(menuItem);

            //            SetChildrenMenu(menuItem, menu.Children);
            //        }
            //    }
            //    else
            //    {
            //        MenuItem menuItem = new MenuItem();
            //        menuItem.MenuHeader = menu.MenuHeader;
            //        menuItem.TargetKey = menu.TargetKey;
            //        menuItem.MenuIcon = menu.MenuIcon;
            //        menuItem.PermissionName = menu.PermissionName;
            //        menuItem.LocalizationKey = menu.LocalizationKey;
            //        grantedMenu.Add(menuItem);
            //        SetChildrenMenu(menuItem, menu.Children);
            //    }
            //}
            //MenuList = new ObservableCollection<MenuItem>(grantedMenu);


            //var identity = MenuList.Where(m => m.MenuHeader == "身份标识").FirstOrDefault();

            //if (identity != null)
            //{
            //    if (identity.Children.Count == 0)
            //    {
            //        MenuList.Remove(identity);
            //    }
            //}



            //var frist = MenuList.FirstOrDefault();
            //if (frist != null)
            //{
            //    frist.OpenViewCommand.Execute(null);
            //}
        }

        private void SetChildrenMenu(MenuItem prentMenu ,List<MenuItem> childrenMenus)
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
        
    }
}
