using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Linq;
using Xamarin.Forms.Labs.Mvvm;
using System.Threading.Tasks;

namespace WomenInBible.ViewModels
{
    public class ViewModelBase : ViewModel
    {
        protected void RaisePropertyChanged<T>(params Expression<Func<T>>[] expressions)
        {
            foreach (var expression in expressions)
            {
                MemberExpression memberExpression = (MemberExpression)expression.Body;
                NotifyPropertyChanged(memberExpression.Member.Name);
            }
        }        

        protected bool SetProperty<T>(ref T backingField, T value, params Expression<Func<T>>[] expressions)
        {
            var changed = !EqualityComparer<T>.Default.Equals(backingField, value);

            if (changed)
            {
                backingField = value;
                RaisePropertyChanged(expressions);
            }

            return changed;
        } 

        protected async Task ShowViewModelModal<T>(string key, object value) where T : ViewModelBase
        {
            await ShowViewModelModal<T>(new Dictionary<string, object> { { key, value } });
        }

        protected async Task ShowViewModelModal<T>(Dictionary<string, object> navigationParameters = null) where T : ViewModelBase
        {
            await Navigation.PushModalAsync<T>((x, y) => { });
        }

        protected async Task ShowViewModel<T>(string key, object value) where T : ViewModelBase
        {
            await ShowViewModel<T>(new Dictionary<string, object> { { key, value } });
        }

        protected async Task ShowViewModel<T>(Dictionary<string, object> navigationParameters = null) where T : ViewModelBase
        {
            //await Navigation.PushAsync(ViewFactory.CreatePage<T>());

            await Navigation.PushAsync<T>((x, y) =>
            {
                x.ParametersReceived(navigationParameters);
            });
        }

        protected virtual void ParametersReceived(Dictionary<string, object> navigationParameters)
        {

        }

        internal virtual void OnLoaded()
        {

        }

        internal virtual void OnClosed()
        {

        }
    }
}

