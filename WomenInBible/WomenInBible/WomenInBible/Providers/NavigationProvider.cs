using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;
using Xamarin.Forms.Labs.Mvvm;

namespace WomenInBible.Providers
{
    public class NavigationProvider
    {
        public async Task ShowViewModelModal<T>(string key, object value) where T : ViewModelBase
        {
            await ShowViewModelModal<T>(new Dictionary<string, object> { { key, value } });
        }

        public async Task ShowViewModelModal<T>(Dictionary<string, object> navigationParameters = null) where T : ViewModelBase
        {
            var nav = new ViewModelNavigation(App.Navigation);
            await nav.PushModalAsync<T>((x, y) => x.ParametersReceived(navigationParameters));
        }

        public async Task ShowViewModel<T>(string key, object value) where T : ViewModelBase
        {
            await ShowViewModel<T>(new Dictionary<string, object> { { key, value } });
        }

        public async Task ShowViewModel<T>(Dictionary<string, object> navigationParameters = null) where T : ViewModelBase
        {            
            var nav = new ViewModelNavigation(App.Navigation);
            await nav.PushAsync<T>((x, y) => x.ParametersReceived(navigationParameters));
        }
    }
}
