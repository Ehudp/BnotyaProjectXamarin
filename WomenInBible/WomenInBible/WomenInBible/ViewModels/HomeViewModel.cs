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
        private int _count = 0;

        private string _openWomenListButtonTitle;
        public string OpenWomenListButtonTitle
        {
            get { return _openWomenListButtonTitle; }
            set { SetProperty<string>(ref _openWomenListButtonTitle, value, () => OpenWomenListButtonTitle); }
        }

        private string _openTehilotButtonTitle;
        public string OpenTehilotButtonTitle
        {
            get { return _openTehilotButtonTitle; }
            set { SetProperty<string>(ref _openTehilotButtonTitle, value, () => OpenTehilotButtonTitle); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty<string>(ref _title, value, () => Title); }
        }

        public ICommand OpenWomenListCommand
        {
            get
            {
                return new Command(async (arg) =>
                    {
                        _count++;
                        await NavigationManager.NavigateTo(new WomenListViewModel { Title = "Test " + _count });
                    });
            }
        }

        public ICommand OpenTehilotCommand
        {
            get { return new Command(async (arg) => await NavigationManager.NavigateTo(new TehilotViewModel())); }
        }

        public HomeViewModel()
        {
            OpenWomenListButtonTitle = "Open Women List";
            OpenTehilotButtonTitle = "Open Tehilot Page";
        }
    }
}
