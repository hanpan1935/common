using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Lanpuda.Identity.UI.Users.Edits
{
    public class UserEditViewModel : EditViewModelBase<UserEditModel>
    {
        private readonly IIdentityUserAppService _identityUserAppService;
        public Func<Task>? Refresh { get; set; }
        public ObservableCollection<AssignableRoleModel> RoleSource { get; set; }

        public UserEditViewModel(IIdentityUserAppService identityUserAppService)
        {
            _identityUserAppService = identityUserAppService;
            RoleSource = new ObservableCollection<AssignableRoleModel>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var roles = await _identityUserAppService.GetAssignableRolesAsync();
                RoleSource.Clear();
                foreach (var role in roles.Items)
                {
                    AssignableRoleModel model = new AssignableRoleModel();
                    model.Name = role.Name;
                    RoleSource.Add(model);
                }



                Guid id = (Guid)this.Model.Id;
                var result = await _identityUserAppService.GetAsync(id);
                this.Model.Id = id;
                this.Model.UserName = result.UserName;
                this.Model.Name = result.Name;
                this.Model.Surname = result.Surname;
                this.Model.Email = result.Email;
                this.Model.PhoneNumber = result.PhoneNumber;
                this.Model.IsActive = result.IsActive;
                this.Model.LockoutEnabled = result.LockoutEnabled;
                var rolesList = await _identityUserAppService.GetRolesAsync(id);
                foreach (var role in rolesList.Items)
                {
                    var exRole = this.RoleSource.Where(m => m.Name.Equals(role.Name)).FirstOrDefault();
                    if (exRole != null)
                    {
                        exRole.IsAssigned = true;
                    }
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
            await UpdateAsync();
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
                IdentityUserUpdateDto dto = new IdentityUserUpdateDto();
                dto.Password = Model.Password;
                dto.UserName = Model.UserName;
                dto.Name = Model.Name;
                dto.Surname = Model.Surname;
                dto.Email = Model.Email;
                dto.PhoneNumber = Model.PhoneNumber;
                dto.IsActive = Model.IsActive;
                dto.LockoutEnabled = Model.LockoutEnabled;

                List<AssignableRoleModel> roleList = new List<AssignableRoleModel>();
                foreach (var item in RoleSource)
                {
                    if (item.IsAssigned == true)
                    {
                        roleList.Add(item);
                    }
                }
                dto.RoleNames = new string[roleList.Count];
                for (int i = 0; i < roleList.Count; i++)
                {
                    dto.RoleNames[i] = roleList[i].Name;
                }

                var result = await _identityUserAppService.UpdateAsync(this.Model.Id, dto);
                if (Refresh != null)
                {
                    await Refresh();
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
