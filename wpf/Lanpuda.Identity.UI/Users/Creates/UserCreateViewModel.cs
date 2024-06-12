using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Identity.UI.Users.Edits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Lanpuda.Identity.UI.Users.Creates
{
    public class UserCreateViewModel : EditViewModelBase<UserCreateModel>
    {
        private readonly IIdentityUserAppService _identityUserAppService;
        public Func<Task>? Refresh { get; set; }
        public ObservableCollection<AssignableRoleModel> RoleSource { get; set; }

        public UserCreateViewModel(IIdentityUserAppService identityUserAppService)
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
                this.Model.IsActive = true;
                this.Model.LockoutEnabled = true;

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
            await CreateAsync();
        }


        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }


        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                IdentityUserCreateDto dto = new IdentityUserCreateDto();
                dto.Password = Model.Password;
                dto.UserName = Model.UserName;
                dto.Name= Model.Name;
                dto.Surname = Model.Surname;
                dto.Email = Model.Email;
                dto.PhoneNumber= Model.PhoneNumber;
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

                await _identityUserAppService.CreateAsync(dto);
                if (Refresh != null)
                {
                    await Refresh();
                    this.CurrentWindowService.Close();
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
    }
}
