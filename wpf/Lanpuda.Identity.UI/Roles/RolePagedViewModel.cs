using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Identity.UI.Roles.Edits;
using Lanpuda.PermissionManagement.UI.Permissions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.Identity;

namespace Lanpuda.Identity.UI.Roles
{
    public class RolePagedViewModel : PagedViewModelBase<IdentityRoleDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityRoleAppService _identityRoleAppService;

        #region 搜索
        public string? Filter
        {
            get { return GetValue<string?>(nameof(Filter)); }
            set { SetValue(value, nameof(Filter)); }
        }
        #endregion


        public RolePagedViewModel(
            IServiceProvider serviceProvider,
            IIdentityRoleAppService identityRoleAppService
            )
        {
            _serviceProvider = serviceProvider;
            _identityRoleAppService = identityRoleAppService;
            this.PageTitle = "角色管理";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            var viewModel = _serviceProvider.GetService<RoleEditViewModel>();
            if (viewModel == null) { return; }
            viewModel.RefreshAsync = this.QueryAsync;
            this.WindowService.Title = "角色-新建";
            this.WindowService.Show("RoleEditView", viewModel);
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            var viewModel = _serviceProvider.GetService<RoleEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Model.Id = this.SelectedModel.Id;
                viewModel.RefreshAsync = this.QueryAsync;
                this.WindowService.Title = "角色-编辑";
                this.WindowService.Show("RoleEditView", viewModel);
            }
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Filter = string.Empty;
            await QueryAsync();
        }

        [Command]
        public void Permissions()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            var viewModel = _serviceProvider.GetService<PermissionViewModel>();
            if (viewModel != null)
            {
                viewModel.ProviderName = "R";
                viewModel.ProviderKey = this.SelectedModel.Name.ToString();
                this.WindowService.Title = "角色-权限";
                this.WindowService.Show("PermissionView", viewModel);
            }
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _identityRoleAppService.DeleteAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                GetIdentityRolesInput requestDto = new GetIdentityRolesInput();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Filter = this.Filter;
                var result = await _identityRoleAppService.GetListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.Clear();
                this.PagedDatas.CanNotify = false;
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.PagedDatas.CanNotify = true;
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}
