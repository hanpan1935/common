using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Client.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class GuidNotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            bool canParse = Guid.TryParse(value.ToString(), out _);

            if (!canParse)
            {
                //throw new InvalidOperationException();
                return false;
            }

            Guid id = (Guid)value;

            if (id == Guid.Empty)
            {
                return false;
            }
            return true;
        }
    }
}
