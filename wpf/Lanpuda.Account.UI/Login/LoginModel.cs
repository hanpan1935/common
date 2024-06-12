using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Account.UI.Login
{
    public class LoginModel : ModelBase
    {
        [Required]
        public string UserName
        {
            get { return GetProperty(() => UserName); }
            set { SetProperty(() => UserName, value); }
        }


        [Required]
        public string Password
        {
            get { return GetProperty(() => Password); }
            set { SetProperty(() => Password, value); }
        }


        public bool RememberMe
        {
            get { return GetProperty(() => RememberMe); }
            set { SetProperty(() => RememberMe, value); }
        }
    }
}
