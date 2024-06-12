using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.PermissionManagement;

namespace Lanpuda.PermissionManagement.UI.Permissions
{
    public class PermissionGrantInfoModel : ModelBase
    {
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public string DisplayName
        {
            get { return GetProperty(() => DisplayName); }
            set { SetProperty(() => DisplayName, value); }
        }

        public string? ParentName
        {
            get { return GetProperty(() => ParentName); }
            set { SetProperty(() => ParentName, value); }
        }

        public bool IsGranted
        {
            get { return GetProperty(() => IsGranted); }
            set { SetProperty(() => IsGranted, value, OnIsGrantedChanged); }
        }

        public List<string> AllowedProviders
        {
            get { return GetProperty(() => AllowedProviders); }
            set { SetProperty(() => AllowedProviders, value); }
        }

        public List<ProviderInfoDto> GrantedProviders
        {
            get { return GetProperty(() => GrantedProviders); }
            set { SetProperty(() => GrantedProviders, value); }
        }


        public ObservableCollection<PermissionGrantInfoModel> ChildrenList
        {
            get { return GetProperty(() => ChildrenList); }
            set { SetProperty(() => ChildrenList, value); }
        }

        


        /// <summary>
        /// 所属组
        /// </summary>
        public PermissionGroupModel? Group { get; set; }

        public PermissionGrantInfoModel()
        {
            ChildrenList = new ObservableCollection<PermissionGrantInfoModel>();
        }


        private void OnIsGrantedChanged()
        {
            if (Group != null)
            {
                Group.NotifyGrantedCountChanged();
            }

            foreach (var item in ChildrenList)
            {
                item.IsGranted = IsGranted;
            }
        }

    }
}
