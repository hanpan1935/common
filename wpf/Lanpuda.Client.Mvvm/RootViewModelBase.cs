using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Timing;
using Volo.Abp.Http.Client;

namespace Lanpuda.Client.Mvvm
{
    public class RootViewModelBase : ViewModelBase, ITransientDependency, IDataErrorInfo
    {
        protected IWindowService WindowService { get { return this.GetService<IWindowService>(); } }
        protected IModuleManager regionManager;

        public bool IsLoading
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public string PageTitle
        {
            get { return GetProperty(() => PageTitle); }
            set { SetProperty(() => PageTitle, value); }
        }

        //protected Type ObjectMapperContext { get; set; }
        //protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
        //    ObjectMapperContext == null
        //        ? provider.GetRequiredService<IObjectMapper>()
        //        : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));

        //public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        public RootViewModelBase()
        {
            regionManager = ModuleManager.DefaultManager;
        }


        protected virtual void HandleException(Exception ex)
        {
            Type type = ex.GetType();


            if (type == typeof(AbpRemoteCallException))
            {

                var exception = (AbpRemoteCallException)ex;
                MessageBox.Show(ex.Message + exception.Details, "错误!");
            }
            else
            {
                MessageBox.Show(ex.Message, "错误!");
            }
            //throw ex;
        }


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
