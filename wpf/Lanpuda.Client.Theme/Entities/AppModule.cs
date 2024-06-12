using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Client.Theme.Entities
{
    public class AppModule
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }

        public bool IsDefault { get;set; }

        public AppModule()
        {
            this.Key = string.Empty;
            this.DisplayName = string.Empty;
        }
    }
}
