using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lanpuda.Client.Theme.ACL
{
    public static class ClientPermissions 
    {
        public static Dictionary<string, bool> GrantedPolicies { get; set; } = new Dictionary<string, bool>();
    }
}
