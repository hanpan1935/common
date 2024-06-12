using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.CultureMap
{
    public class CultureMapInfo
    {
        public string TargetCulture { get; set; }

        public List<string> SourceCultures { get; set; }

        public CultureMapInfo() {
            TargetCulture = string.Empty;
            SourceCultures = new List<string>();
        }
    }
}
