using DevExpress.Mvvm;
using Lanpuda.Client.Theme.Entities;
using Lanpuda.Client.Theme.Services.MenuServices;
using Lanpuda.Client.Theme.Services.SettingsServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Volo.Abp.DependencyInjection;

namespace Lanpuda.Client.Theme.Layouts.DefaultLayouts.Widgets.MainHeaders
{
    public class MainHeaderViewModel : ViewModelBase, ITransientDependency
    {
        private readonly ISettingsService _settingsService;
        private readonly IMenuService _menuService;
        public BitmapImage Avatar { get; set; }
        public string UserName { get; set; }

        public ObservableCollection<AppModule> Modules { get; set; }

        public AppModule? SelectedModel
        {
            get { return GetProperty(() => SelectedModel); }
            set { SetProperty(() => SelectedModel, value, OnSelectedModuleChanged); }
        }

        

        public MainHeaderViewModel(ISettingsService settingsService, IMenuService menuService)
        {
            _settingsService = settingsService;
            _menuService = menuService;
            Avatar = _settingsService.GetUserAvatar();
            UserName = _settingsService.GetSurname() + _settingsService.GetName();

            Modules = new ObservableCollection<AppModule>();

            var modules = _menuService.GetAllAppModules();
            foreach (var item in modules)
            {
                Modules.Add(item);
            }

            SelectedModel = Modules.FirstOrDefault();
        }


        private void OnSelectedModuleChanged()
        {
            Messenger.Default.Send(SelectedModel);
        }

    }
}
