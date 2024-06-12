using DevExpress.Mvvm.DataAnnotations;
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
    public class PermissionModel : ModelBase
    {
        public string EntityDisplayName
        {
            get { return GetProperty(() => EntityDisplayName); }
            set { SetProperty(() => EntityDisplayName, value); }
        }

        public ObservableCollection<PermissionGroupModel> Groups
        {
            get { return GetProperty(() => Groups); }
            set { SetProperty(() => Groups, value); }
        }

        public PermissionGroupModel? SelectedGroup
        {
            get { return GetProperty(() => SelectedGroup); }
            set { SetProperty(() => SelectedGroup, value); }
        }


        public bool IsGrantAll
        {
            get { return GetProperty(() => IsGrantAll); }
            set { SetProperty(() => IsGrantAll, value, IsGrantAllChanged); }
        }

        public PermissionModel()
        {
            Groups = new ObservableCollection<PermissionGroupModel>();
        }


        private void IsGrantAllChanged()
        {
            if (IsGrantAll == true)
            {
                foreach (var group in this.Groups)
                {
                    foreach (var permission in group.Permissions)
                    {
                        GrandAllChildren(permission);
                    }
                }
            }
            else
            {
                foreach (var group in this.Groups)
                {
                    foreach (var permission in group.Permissions)
                    {
                        UnGrandAllChildren(permission);
                    }
                }
            }
        }


        private void GrandAllChildren(PermissionGrantInfoModel permission)
        {
            permission.IsGranted = true;
            foreach (var item in permission.ChildrenList)
            {
                item.IsGranted = true;
                GrandAllChildren(item);
            }
        }


        private void UnGrandAllChildren(PermissionGrantInfoModel permission)
        {
            permission.IsGranted = false;
            foreach (var item in permission.ChildrenList)
            {
                item.IsGranted = false;
                UnGrandAllChildren(item);
            }
        }
    }
}
