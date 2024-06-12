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

namespace Lanpuda.Account.UI.Manage
{
    public class ManageModel : ModelBase
    {

        //[Required(ErrorMessage ="必填")]
        //[DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        public string CurrentPassword
        {
            get { return GetProperty(() => CurrentPassword); }
            set { SetProperty(() => CurrentPassword, value); }
        }

        //[Required(ErrorMessage = "必填")]
        //[DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        public string NewPassword
        {
            get { return GetProperty(() => NewPassword); }
            set { SetProperty(() => NewPassword, value); }
        }


        //[Required(ErrorMessage = "必填")]
        //[DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        public string ConfirmNewPassword
        {
            get { return GetProperty(() => ConfirmNewPassword); }
            set { SetProperty(() => ConfirmNewPassword, value); }
        }

        ////////////////
        [Required(ErrorMessage = "必填")]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
        public string UserName
        {
            get { return GetProperty(() => UserName); }
            set { SetProperty(() => UserName, value); }
        }

        [Required(ErrorMessage = "必填")]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
        public string Email
        {
            get { return GetProperty(() => Email); }
            set { SetProperty(() => Email, value); }
        }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxNameLength))]
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxSurnameLength))]
        public string Surname
        {
            get { return GetProperty(() => Surname); }
            set { SetProperty(() => Surname, value); }
        }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        public string PhoneNumber
        {
            get { return GetProperty(() => PhoneNumber); }
            set { SetProperty(() => PhoneNumber, value); }
        }

        public string ConcurrencyStamp
        {
            get { return GetProperty(() => ConcurrencyStamp); }
            set { SetProperty(() => ConcurrencyStamp, value); }
        }
    }
}
