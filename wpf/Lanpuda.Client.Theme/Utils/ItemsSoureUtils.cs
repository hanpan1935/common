using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Client.Theme.Utils
{
    public static class ItemsSoureUtils
    {
        public static Dictionary<string,bool> GetBoolDictionary()
        {
            var dic = new Dictionary<string, bool>();
            dic.Add("是", true);
            dic.Add("否", false);

            return dic;
        }
    }
}
