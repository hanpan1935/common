using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.Client.Theme.ACL
{
    public class ACLExtension : System.Windows.Markup.MarkupExtension
    {
        private string _permissionName;
        public ACLExtension(string permissionName)
        {
            _permissionName = permissionName;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // bool hasPermission = ACL.ClientPermissions.GrantedPolicies[_permissionName];

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return System.Windows.Visibility.Visible;
            }


            bool hasPermission = ACL.ClientPermissions.GrantedPolicies.ContainsKey(_permissionName);

            if (hasPermission)
            {
                return System.Windows.Visibility.Visible;
            }
            else
            {
                return System.Windows.Visibility.Collapsed;
            }
        }
    }
}
