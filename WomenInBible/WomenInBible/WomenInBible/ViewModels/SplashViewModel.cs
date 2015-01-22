using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WomenInBible.CustomControls;
using WomenInBible.Managers;
using Xamarin.Forms;

namespace WomenInBible.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        public string BackgroundImage
        {
            get { return "background.png"; }            
        } 
        
        public Color ProgressColor
        {
            get { return Color.FromHex("3498DB"); }
        }
                
        public Color ProgressBackgroundColor
        {
            get { return Color.FromHex("B4BCBC"); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value, () => IsLoading); }
        }        

        public async void AfterLoading() //TODO: fix this
        {
            IsLoading = true;
            await Task.Delay(10000).ConfigureAwait(false);
            IsLoading = false;
            App.SetMainPage();
        }        
    }
}
