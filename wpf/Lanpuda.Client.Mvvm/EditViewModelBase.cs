using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lanpuda.Client.Mvvm
{
    public class EditViewModelBase<T> : RootViewModelBase where T : class, new()
    {
        public T Model
        {
            get { return GetProperty(() => Model); }
            set { SetProperty(() => Model, value); }
        }


        protected ICurrentWindowService CurrentWindowService
        {
            get { return GetService<ICurrentWindowService>(); }
        }


        public EditViewModelBase()
        {
            Model = new T();
        }


        [Command]
        public virtual void Close()
        {
            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }

    }
}
