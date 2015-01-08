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
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, () => Title); }
        }

        public ICommand OpenWomenListCommand
        {
            get
            {
                return new Command(async (arg) =>
                {
                    await NavigationManager.NavigateTo(new WomenListViewModel());
                });
            }
        }

        public TehilotViewModel()
        {
            Title = "Tehilot";
        }
    }
}
