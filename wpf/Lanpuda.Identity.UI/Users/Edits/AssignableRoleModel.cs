using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Identity.UI.Users.Edits
{
    public class AssignableRoleModel : ModelBase
    {
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public bool IsAssigned
        {
            get { return GetProperty(() => IsAssigned); }
            set { SetProperty(() => IsAssigned, value); }
        }
    }
}
