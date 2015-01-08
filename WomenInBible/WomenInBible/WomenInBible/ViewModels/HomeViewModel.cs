using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using WomenInBible.Managers;

namespace WomenInBible.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _openWomenListButtonTitle;
        public string OpenWomenListButtonTitle
        {
            get { return _openWomenListButtonTitle; }
            set { SetProperty(ref _openWomenListButtonTitle, value, () => OpenWomenListButtonTitle); }
        }

        private string _openTehilotButtonTitle;
        public string OpenTehilotButtonTitle
        {
            get { return _openTehilotButtonTitle; }
            set { SetProperty(ref _openTehilotButtonTitle, value, () => OpenTehilotButtonTitle); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, () => Title); }
        }

        private string _backgroundImage;
        public string BackgroundImage
        {
            get { return _backgroundImage; }
            set { SetProperty(ref _backgroundImage, value, () => BackgroundImage); }
        }

        public ICommand OpenWomenListCommand
        {
            get { return new Command(async (arg) => await NavigationManager.NavigateTo(new WomenListViewModel())); }
        }

        public ICommand OpenTehilotCommand
        {
            get { return new Command(async (arg) => await NavigationManager.NavigateTo(new TehilotViewModel())); }
        }

        public HomeViewModel()
        {
            Title = "Home";
            BackgroundImage = "background.png";
            OpenWomenListButtonTitle = "Open Women List";
            OpenTehilotButtonTitle = "Open Tehilot Page";
        }
    }
}
