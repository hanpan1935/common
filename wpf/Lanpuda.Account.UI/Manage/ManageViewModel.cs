using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using NUglify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Identity;

namespace Lanpuda.Account.UI.Manage
{
    public class ManageViewModel : EditViewModelBase<ManageModel>
    {
        private readonly IProfileAppService _profileAppService;
        public ManageViewModel(IProfileAppService profileAppService)
        {
            this._profileAppService = profileAppService;
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                ProfileDto profile = await _profileAppService.GetAsync();
                this.Model.UserName = profile.UserName;
                Model.Email = profile.Email;
                Model.Name = profile.Name;
                Model.Surname = profile.Surname;
                Model.PhoneNumber = profile.PhoneNumber;
                Model.ConcurrencyStamp = profile.ConcurrencyStamp;
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
        public async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;

                UpdateProfileDto updateProfileDto = new UpdateProfileDto();
                updateProfileDto.UserName = this.Model.UserName;
                updateProfileDto.Email = this.Model.Email;
                updateProfileDto.Name = this.Model.Name;
                updateProfileDto.Surname = this.Model.Surname;
                updateProfileDto.PhoneNumber = this.Model.PhoneNumber;
                updateProfileDto.ConcurrencyStamp = this.Model.ConcurrencyStamp;
                await _profileAppService.UpdateAsync(updateProfileDto);

                await this.InitializeAsync();
                Growl.Success("保存成功");

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        [AsyncCommand]
        public async Task ChangePasswordAsync()
        {
            try
            {
                this.IsLoading = true;
                ChangePasswordInput input = new ChangePasswordInput();
                input.CurrentPassword = Model.CurrentPassword;
                input.NewPassword  = this.Model.NewPassword;
                await _profileAppService.ChangePasswordAsync(input);
                await this.InitializeAsync();
                Growl.Success("保存成功");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }
        }

    }
}
