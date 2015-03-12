using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WomenInBible.CustomControls;
using WomenInBible.Managers;
using WomenInBible.Messages;
using Xamarin.Forms;

namespace WomenInBible.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        public string SplashImage
        {
            get { return "background.png"; }            
        }

        private string _backgroundImage;
        public string BackgroundImage
        {
            get { return _backgroundImage; }
            set { SetProperty(ref _backgroundImage, value, () => BackgroundImage); }
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

        public SplashViewModel()
        {
            BackgroundImage = "clean_background.png";
            MessagingCenter.Subscribe<ViewAppearedMessage>(this, "View appeared", 
                async (message) => await LoadDataAsync());

            var settings = IoC.Resolve<SettingsManager>();

            if (settings.InitMusicSetting)
            {
                SoundManager.SoundService.Volume = settings.MusicVolumeSetting;                
                SoundManager.SoundService.PlayAsync("music.mp3");
            }
        }

        private async Task LoadDataAsync()
        {            
            // IsLoading = true;
            await IoC.Resolve<DatabaseManager>().InitializationAwaiter; // Create DB
            // IsLoading = false;
            MessagingCenter.Send<SplashFinishedMessage>(new SplashFinishedMessage(), "Splash finished");
            await App.Navigation.PopModalAsync(true);
        }        
    }
}
