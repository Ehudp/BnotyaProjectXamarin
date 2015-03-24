using System;
using System.Collections.Generic;
using System.Text;
using WomenInBible.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(WomenInBible.iOS.Methods_iOS))]
namespace WomenInBible.iOS
{
    public class Methods_iOS : IMethods
    {
        public void Close_App()
        {
            
        }
    }
}
