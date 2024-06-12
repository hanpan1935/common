using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Identity.UI.Roles.Edits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Lanpuda.Identity.UI.Roles.Edits
{
    public class RoleEditViewModel : EditViewModelBase<RoleEditModel>
    {
        private readonly IIdentityRoleAppService _identityRoleAppService;
        public Func<Task>? RefreshAsync { get; set; }

        public RoleEditViewModel(IIdentityRoleAppService identityRoleAppService)
        {
            _identityRoleAppService = identityRoleAppService;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                if (this.Model.Id != null)
                {
                    var result = await _identityRoleAppService.GetAsync((Guid)this.Model.Id);
                    Model.Name = result.Name;
                    Model.IsDefault = result.IsDefault;
                    Model.IsPublic = result.IsPublic;
                }
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



        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();
            }
        }


        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }

        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                if (this.Model.Id == null)
                {
                    return;
                }
                IdentityRoleUpdateDto dto = new IdentityRoleUpdateDto();
                dto.Name = Model.Name;
                dto.IsDefault = Model.IsDefault;
                dto.IsPublic = Model.IsPublic;
                var result = await _identityRoleAppService.UpdateAsync((Guid)this.Model.Id, dto);
                if (RefreshAsync != null)
                {
                    await RefreshAsync();
                    this.CurrentWindowService.Close();
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

        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                IdentityRoleCreateDto dto = new IdentityRoleCreateDto();
                dto.Name = Model.Name;
                dto.IsDefault = Model.IsDefault;
                dto.IsPublic = Model.IsPublic;
                var result = await _identityRoleAppService.CreateAsync(dto);
                if (RefreshAsync != null)
                {
                    await RefreshAsync();
                    this.CurrentWindowService.Close();
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
    }
}
