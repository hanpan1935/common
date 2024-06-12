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
    public  class PermissionGroupModel : ModelBase
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

        public string DisplayNameKey
        {
            get { return GetProperty(() => DisplayNameKey); }
            set { SetProperty(() => DisplayNameKey, value); }
        }

        public string DisplayNameResource
        {
            get { return GetProperty(() => DisplayNameResource); }
            set { SetProperty(() => DisplayNameResource, value); }
        }

        public ObservableCollection<PermissionGrantInfoModel> Permissions
        {
            get { return GetProperty(() => Permissions); }
            set { SetProperty(() => Permissions, value); }
        }


        public bool IsGroupGrantAll
        {
            get { return GetProperty(() => IsGroupGrantAll); }
            set { SetProperty(() => IsGroupGrantAll, value, IsGroupGrantAllChanged); }
        }

        public int GrantedCount
        {
            get { return GetGrantedCount(); }
            set { SetProperty(() => GrantedCount, value); }
        }

        public PermissionGroupModel()
        {
            Permissions = new ObservableCollection<PermissionGrantInfoModel>();
        }


        private int GetGrantedCount()
        {
            int count = 0;
            foreach (PermissionGrantInfoModel permission in Permissions)
            {
                GetChildrenCount(permission,ref count);
            }
            return count;
        }

        private void GetChildrenCount(PermissionGrantInfoModel permissionm,ref int count)
        {
            if (permissionm.IsGranted)
            {
                count++;
                foreach (var item in permissionm.ChildrenList)
                {
                    if (item.IsGranted == true)
                    {
                        count++;
                    }
                }
            }
        }


        public void NotifyGrantedCountChanged()
        {
            RaisePropertyChanged(nameof(GrantedCount));
        }

        private void IsGroupGrantAllChanged()
        {
            foreach (var item in Permissions)
            {
                IsGroupGrantAllChangedSetChildren(item);
            }
        }

        private void IsGroupGrantAllChangedSetChildren(PermissionGrantInfoModel permission)
        {
            permission.IsGranted = this.IsGroupGrantAll;
            foreach (var item in permission.ChildrenList)
            {
                item.IsGranted = this.IsGroupGrantAll;
            }
        }
    }
}
