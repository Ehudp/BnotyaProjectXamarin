using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using WomenInBible.Managers;

namespace WomenInBible.ViewModels
{
    public class TehilotViewModel : ViewModelBase
    {
        public ICommand OpenWomenListCommand
        {
            get
            {
                return new Command(async (arg) =>
                {
                    await NavigationManager.NavigateTo(new WomenListViewModel { Title = "Test 0"});
                });
            }
        }
    }
}
