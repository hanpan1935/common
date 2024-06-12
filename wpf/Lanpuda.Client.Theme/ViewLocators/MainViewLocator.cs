using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Client.Theme.ViewLocators
{
    public class MainViewLocator : IViewLocator
    {
        public string GetViewTypeName(Type type)
        {
            return type?.FullName;
        }

        public object ResolveView(string name)
        {
            Type t = ResolveViewType(name);
            return Activator.CreateInstance(t);
        }
        public Type ResolveViewType(string name)
        {
            if (name == "ViewA")
            {
                Assembly assembly = Assembly.LoadFrom(@"ExampleViews.dll");
                return assembly.GetType("ExampleViews.ViewA");
            }
            return null;
        }
    }
}
