using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;
using WomenInBible.Views;
using Xamarin.Forms;

namespace WomenInBible.Managers
{
    public static class NavigationManager
    {
        private static Dictionary<Type, Type> _viewCollection;

        public static void RegisterView(Type viewModelType, Type viewType)
        {
            if (_viewCollection == null)
                _viewCollection = new Dictionary<Type, Type>();

            if (!_viewCollection.ContainsKey(viewModelType))
                _viewCollection.Add(viewModelType, viewType);            
        }

        public static Page ResolveView(this ViewModelBase viewModel)
        {
            var viewModelType = viewModel.GetType();
            if (_viewCollection != null && _viewCollection.ContainsKey(viewModelType))
            {                
                Page view = (Page)Activator.CreateInstance(_viewCollection[viewModelType]);
                view.BindingContext = viewModel;
                return view;
            }
            else
                throw new Exception("View Model not registered!");
        }

        public async static Task NavigateTo(ViewModelBase toViewModel)
        {
            var toViewModelType = toViewModel.GetType();

            if (_viewCollection.ContainsKey(toViewModelType))
            {
                Page view = (Page)Activator.CreateInstance(_viewCollection[toViewModelType]);
                view.BindingContext = toViewModel;
                await App.Navigation.PushAsync(view);                 
            }
            else
                throw new Exception("View Model not registered!");
        }
    }
}
