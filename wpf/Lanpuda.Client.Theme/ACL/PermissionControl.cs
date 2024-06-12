using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.Client.Theme.ACL
{
    /// <summary>
    /// 附加属性-权限控制
    /// </summary>
    public class PermissionControl
    {
        public static Visibility GetPermissionName(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(PermissionName);
        }

        public static void SetPermissionName(DependencyObject obj, string value)
        {
            obj.SetValue(PermissionName, value);
        }

        // Using a DependencyProperty as the backing store for VisibilityByPermission.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PermissionName =
            DependencyProperty.RegisterAttached("PermissionName", typeof(string), typeof(PermissionControl), new PropertyMetadata("", OnPermissionChanged));

        private static void OnPermissionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                bool isDesign = DesignerProperties.GetIsInDesignMode(d);

                if (isDesign)
                {
                    element.Visibility = Visibility.Visible;
                }
                else
                {
                    string? permissionName = e.NewValue as string;
                    if (permissionName == null)
                    {
                        permissionName = "";
                    }

                    bool hasPermission = ACL.ClientPermissions.GrantedPolicies.ContainsKey(permissionName);

                    if (hasPermission)
                    {
                        element.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        element.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }
    }
}
