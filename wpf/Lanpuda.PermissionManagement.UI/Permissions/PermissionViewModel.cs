using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using Lanpuda.Client.Mvvm;
using NUglify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Volo.Abp.PermissionManagement;

namespace Lanpuda.PermissionManagement.UI.Permissions
{
    public class PermissionViewModel : RootViewModelBase
    {
        private readonly IPermissionAppService _permissionAppService;
        public PermissionModel Model { get; set; }
        public string? ProviderName { get; set; }
        public string? ProviderKey { get; set; }
        private GetPermissionListResultDto? getPermissionListResultDto;

        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        public PermissionViewModel(IPermissionAppService permissionAppService)
        {
            _permissionAppService = permissionAppService;
            Model = new PermissionModel();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                getPermissionListResultDto = await _permissionAppService.GetAsync(ProviderName, ProviderKey);
                Model.EntityDisplayName = getPermissionListResultDto.EntityDisplayName;
                foreach (var group in getPermissionListResultDto.Groups)
                {
                    PermissionGroupModel groupModel = new PermissionGroupModel();
                    groupModel.Name = group.Name;
                    groupModel.DisplayName = group.DisplayName;
                    groupModel.DisplayNameKey = group.DisplayNameKey;
                    groupModel.DisplayNameResource = group.DisplayNameResource;
                    //转成树形
                    var tops = group.Permissions.Where(m => string.IsNullOrEmpty(m.ParentName)).ToList();

                    foreach (var permission in tops)
                    {
                        PermissionGrantInfoModel permissionModel = new PermissionGrantInfoModel();
                        permissionModel.Name = permission.Name;
                        permissionModel.DisplayName = permission.DisplayName;
                        permissionModel.ParentName = permission.ParentName;
                        permissionModel.IsGranted = permission.IsGranted;
                        permissionModel.AllowedProviders = permission.AllowedProviders;
                        permissionModel.ChildrenList = SetChildrens(group.Permissions, permission.Name, groupModel);
                        permissionModel.Group= groupModel;
                        groupModel.Permissions.Add(permissionModel);
                    }
                    Model.Groups.Add(groupModel);
                }

                this.Model.SelectedGroup = Model.Groups.FirstOrDefault();
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        [Command]
        public void Close()
        {
            CurrentWindowService.Close();
        }



        [AsyncCommand]
        public async Task SaveAsync()
        {
            try
            {
                if (getPermissionListResultDto == null)
                {
                    return;
                }
                this.IsLoading = true;
                UpdatePermissionsDto dto = new UpdatePermissionsDto();

                List<UpdatePermissionDto> details = new List<UpdatePermissionDto>();
                foreach (var group in Model.Groups)
                {
                    foreach (var permission in group.Permissions)
                    {
                        GetChildrenForSave(details, permission);
                    }
                }
                dto.Permissions = details.ToArray();
                await _permissionAppService.UpdateAsync(this.ProviderName, this.ProviderKey, dto);
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        private ObservableCollection<PermissionGrantInfoModel> SetChildrens(List<PermissionGrantInfoDto> permissions, string parentName, PermissionGroupModel groupModel)
        {
            var childrens = permissions.Where(m => m.ParentName == parentName).ToList();

            ObservableCollection<PermissionGrantInfoModel> childrensList = new ObservableCollection<PermissionGrantInfoModel>();
            foreach (var permission in childrens)
            {
                PermissionGrantInfoModel permissionModel = new PermissionGrantInfoModel();
                permissionModel.Name = permission.Name;
                permissionModel.DisplayName = permission.DisplayName;
                permissionModel.ParentName = permission.ParentName;
                permissionModel.IsGranted = permission.IsGranted;
                permissionModel.AllowedProviders = permission.AllowedProviders;
                permissionModel.GrantedProviders = permission.GrantedProviders;
                permissionModel.ChildrenList = SetChildrens(permissions, permission.Name, groupModel);
                permissionModel.Group = groupModel;
                childrensList.Add(permissionModel);
            }
            return childrensList;
        }


        private void GetChildrenForSave(List<UpdatePermissionDto> details, PermissionGrantInfoModel permission)
        {
            UpdatePermissionDto permissionDto = new UpdatePermissionDto();
            permissionDto.Name = permission.Name;
            permissionDto.IsGranted = permission.IsGranted;
            details.Add(permissionDto);
            foreach (var item in permission.ChildrenList)
            {
                GetChildrenForSave(details, item);
            }
        }


      
    }
}
