using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services;

namespace WomenInBible.ViewModels
{
    public static class IoC
    {
        internal static SimpleContainer Container { get; set; }

        static IoC()
        {
            Container = new SimpleContainer();
        }

        // Summary:
        //     Resolve a dependency.
        //
        // Type parameters:
        //   T:
        //     Type of instance to get.
        //
        // Returns:
        //     An instance of {T} if successful, otherwise null.
        public static T Resolve<T>() where T : class
        {
            return Container.GetResolver().Resolve<T>();
        }

        //
        // Summary:
        //     Registers an instance of T to be stored in the container.
        //
        // Parameters:
        //   instance:
        //     Instance of type T.
        //
        // Type parameters:
        //   T:
        //     Type of instance
        //
        // Returns:
        //     An instance of Xamarin.Forms.Labs.Services.SimpleContainer
        public static void Register<T>(T instance) where T : class
        {
            Container.Register<T>(instance);
        }
    }

}
