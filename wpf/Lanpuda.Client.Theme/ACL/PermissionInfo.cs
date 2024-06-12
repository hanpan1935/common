using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Client.Theme.ACL
{
    public class PermissionInfo
    {
        public string GroupName{get; set;}

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string? ParentName { get; set; }

        public bool IsGranted { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        public PermissionInfo()
        {
            this.GroupName = string.Empty;
            this.Name = string.Empty;
            this.DisplayName = string.Empty;
        }
    }
}
