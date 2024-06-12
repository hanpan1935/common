using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Client.Mvvm
{
    public class ModelBase : ViewModelBase, IDataErrorInfo
    {
        
        public virtual bool HasErrors()
        {
            bool isHasErrors = IDataErrorInfoHelper.HasErrors(this);
            return isHasErrors;
        }


        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string result = "";
                result += IDataErrorInfoHelper.GetErrorText(this, columnName);
                return result;
            }
        }
    }
}
