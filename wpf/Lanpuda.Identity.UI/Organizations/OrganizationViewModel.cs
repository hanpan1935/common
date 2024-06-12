using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Lanpuda.Identity.UI.Organizations
{
    public class OrganizationViewModel : RootViewModelBase
    {
        public OrganizationViewModel () 
        {
            this.PageTitle = "组织机构";
        } 
    }
}
