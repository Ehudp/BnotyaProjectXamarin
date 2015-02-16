using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services;

namespace WomenInBible.Managers
{
    public static class IoC
    {
        internal static SimpleContainer Container { get; set; }

        static IoC()
        {
            Container = new SimpleContainer();
        }
        
        public static T Resolve<T>() where T : class, new()
        {
            var result = Container.GetResolver().Resolve<T>();
            if(result == null)
                result = Register<T>();
            return result;
        }

        public static T Register<T>() where T : class, new()
        {
            var instance = new T();
            Container.Register<T>(instance);
            return instance;
        }
    }
}
