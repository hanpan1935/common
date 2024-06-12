using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Lanpuda.Identity.UI.Roles.Edits
{
    public class RoleEditModel : ModelBase
    {
        public Guid? Id { get; set; }


        [Required(ErrorMessage = "必填")]
        [DynamicStringLength(typeof(IdentityRoleConsts), "MaxNameLength", null)]
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public bool IsDefault
        {
            get { return GetProperty(() => IsDefault); }
            set { SetProperty(() => IsDefault, value); }
        }

        public bool IsPublic
        {
            get { return GetProperty(() => IsPublic); }
            set { SetProperty(() => IsPublic, value); }
        }

    }
}
