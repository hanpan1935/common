using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.Identity;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Identity.UI.Users.Edits;
using Lanpuda.Identity.UI.Users.Creates;
using Lanpuda.PermissionManagement.UI.Permissions;

namespace Lanpuda.Identity.UI.Users
{
    public class UserPagedViewModel : PagedViewModelBase<IdentityUserDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityUserAppService _identityUserAppService;

        #region 搜索
        public string? Filter
        {
            get { return GetValue<string?>(nameof(Filter)); }
            set { SetValue(value, nameof(Filter)); }
        }
        #endregion


        public UserPagedViewModel(
            IServiceProvider serviceProvider,
            IIdentityUserAppService identityUserAppService
            )
        {
            _serviceProvider = serviceProvider;
            _identityUserAppService = identityUserAppService;
            this.PageTitle = "用户管理";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            var viewModel = _serviceProvider.GetService<UserCreateViewModel>();
            if (viewModel == null) { return; }
            viewModel.Refresh = this.QueryAsync;
            this.WindowService.Title = "用户-新建";
            this.WindowService.Show("UserCreateView", viewModel);
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            var viewModel = _serviceProvider.GetService<UserEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Model.Id = this.SelectedModel.Id;
                viewModel.Refresh = this.QueryAsync;
                this.WindowService.Title = "用户-编辑";
                this.WindowService.Show("UserEditView", viewModel);
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
                viewModel.ProviderName = "U";
                viewModel.ProviderKey = this.SelectedModel.Id.ToString();
                this.WindowService.Title = "用户-权限";
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
                    await _identityUserAppService.DeleteAsync(this.SelectedModel.Id);
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
                GetIdentityUsersInput requestDto = new GetIdentityUsersInput();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Filter = this.Filter;
                var result = await _identityUserAppService.GetListAsync(requestDto);
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
