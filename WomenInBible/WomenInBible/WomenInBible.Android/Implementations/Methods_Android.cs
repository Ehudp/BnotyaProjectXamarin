using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WomenInBible.Managers;
using WomenInBible.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Methods_Android))]
namespace WomenInBible.Droid
{
    public class Methods_Android : IMethods
    {
        public void Close_App()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}