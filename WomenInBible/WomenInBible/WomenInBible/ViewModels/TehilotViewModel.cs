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

        private ICommand _openWomenListCommand;
        public ICommand OpenWomenListCommand
        {
            get
            {
                return _openWomenListCommand ?? (_openWomenListCommand = new Command(
                  async () => await ShowViewModel<WomenListViewModel>(), () => true));
            }
        }

        public TehilotViewModel()
        {
            Title = "Tehilot";
        }
    }
}
