using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.CultureMap
{
    public static class LanpudaApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLanpudaRequestLocalization(this IApplicationBuilder app,
       Action<RequestLocalizationOptions> optionsAction = null)
        {
            return app.UseAbpRequestLocalization(options =>
            {
                options.RequestCultureProviders.Insert(0, new LanpudaCultureMapRequestCultureProvider());
                optionsAction?.Invoke(options);
            });
        }
    }
}
