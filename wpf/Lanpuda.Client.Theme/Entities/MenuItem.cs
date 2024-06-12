using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lanpuda.Client.Theme.Entities
{
    public class MenuItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string MenuIcon { get; set; }
        public string MenuHeader { get; set; }
        public string TargetKey { get; set; }
        public string? LocalizationKey { get; set; }
        public List<MenuItem> Children { get; set; }
        public string? PermissionName { get;set; }

        private bool _IsExpanded;
        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set { _IsExpanded = value; OnPropertyChanged("IsExpanded"); }
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; OnPropertyChanged("IsSelected"); }
        }

        private bool _IsGranted;
        public bool IsGranted
        {
            get { return _IsGranted; }
            set { _IsGranted = value; OnPropertyChanged("IsGranted"); }
        }


        public MenuItem()
        {
            this.TargetKey = string.Empty;
            this.MenuHeader = string.Empty;
            this.MenuIcon = string.Empty;
            this.Children = new List<MenuItem>();
            PermissionName = string.Empty;
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand OpenViewCommand
        {
            get => new DelegateCommand(() =>
            {
                if ((this.Children == null || this.Children.Count == 0) &&
                    !string.IsNullOrEmpty(this.TargetKey))
                {
                    // 页面跳转
                    //_regionManager.RequestNavigate("MainContentRegion", this.TargetView);
                    ModuleManager.DefaultManager.InjectOrNavigate(RegionNames.MainContentRegion, TargetKey);

                    bool isInjected = ModuleManager.DefaultManager.IsInjected(RegionNames.MainContentRegion, TargetKey);
                    if (isInjected)
                    {
                        ModuleManager.DefaultManager.Navigate(RegionNames.MainContentRegion, TargetKey);
                    }
                    else
                    {
                        ModuleManager.DefaultManager.Inject(RegionNames.MainContentRegion, TargetKey);
                        ModuleManager.DefaultManager.Navigate(RegionNames.MainContentRegion, TargetKey);
                    }
                    this.IsSelected = true;
                }
                else
                {
                    this.IsExpanded = !this.IsExpanded;
                    this.IsSelected = true;
                }
            });
        }
    }
}
