using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WomenInBible.Managers;
using Xamarin.Forms;

namespace WomenInBible.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public ICommand OpenHomeCommand
        {
            get { return new Command(async (arg) => await NavigationManager.NavigateTo(new HomeViewModel())); }
        }

        public ICommand OpenTehilotCommand
        {
            get { return new Command(async (arg) => await NavigationManager.NavigateTo(new TehilotViewModel())); }
        }

        public ICommand OpenWomenListCommand
        {
            get
            {
                return new Command(async (arg) =>
                {
                    await NavigationManager.NavigateTo(new WomenListViewModel { Title = "Test 0" });
                });
            }
        }
    }
}
