using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Lanpuda.Identity.UI.Users.Creates
{
    public class UserCreateModel : ModelBase
    {
        [DisableAuditing]
        [Required(ErrorMessage = "必填")]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        public string Password
        {
            get { return GetProperty(() => Password); }
            set { SetProperty(() => Password, value); }
        }

        [Required(ErrorMessage = "必填")]
        [DynamicStringLength(typeof(IdentityUserConsts), "MaxUserNameLength", null)]
        public string UserName
        {
            get { return GetProperty(() => UserName); }
            set { SetProperty(() => UserName, value); }
        }


        [DynamicStringLength(typeof(IdentityUserConsts), "MaxNameLength", null)]
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }



        [DynamicStringLength(typeof(IdentityUserConsts), "MaxSurnameLength", null)]
        public string Surname
        {
            get { return GetProperty(() => Surname); }
            set { SetProperty(() => Surname, value); }
        }

        [Required(ErrorMessage = "必填")]
        [EmailAddress(ErrorMessage = "请输入正确邮箱地址")]
        [DynamicStringLength(typeof(IdentityUserConsts), "MaxEmailLength", null)]
        public string Email
        {
            get { return GetProperty(() => Email); }
            set { SetProperty(() => Email, value); }
        }


        [DynamicStringLength(typeof(IdentityUserConsts), "MaxPhoneNumberLength", null)]
        public string PhoneNumber
        {
            get { return GetProperty(() => PhoneNumber); }
            set { SetProperty(() => PhoneNumber, value); }
        }


        public bool IsActive
        {
            get { return GetProperty(() => IsActive); }
            set { SetProperty(() => IsActive, value); }
        }


        public bool LockoutEnabled
        {
            get { return GetProperty(() => LockoutEnabled); }
            set { SetProperty(() => LockoutEnabled, value); }
        }


        //public string[] RoleNames
        //{
        //    get { return GetProperty(() => RoleNames); }
        //    set { SetProperty(() => RoleNames, value); }
        //}
    }
}
