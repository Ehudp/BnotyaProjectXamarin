using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Linq;
using Xamarin.Forms.Labs.Mvvm;
using System.Threading.Tasks;
using WomenInBible.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using WomenInBible.Models;
using WomenInBible.Managers;

namespace WomenInBible.ViewModels
{
    public class ViewModelBase : ViewModel
    {
        private string _result;
        public string Result
        {
            get { return _result; }
            private set { SetProperty(ref _result, value); }
        }

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
            await Navigation.PushModalAsync<T>((x, y) => x.ParametersReceived(navigationParameters));
        }

        protected async Task ShowViewModel<T>(string key, object value) where T : ViewModelBase
        {
            await ShowViewModel<T>(new Dictionary<string, object> { { key, value } });
        }

        protected async Task ShowViewModel<T>(Dictionary<string, object> navigationParameters = null) where T : ViewModelBase
        {
            await Navigation.PushAsync<T>((x, y) => x.ParametersReceived(navigationParameters));
        }

        public virtual void ParametersReceived(Dictionary<string, object> navigationParameters)
        {

        }

        internal virtual void OnLoaded()
        {

        }

        internal virtual void OnClosed()
        {

        }

        public ICommand ShowAlertCommand
        {
            get
            {
                return new Command<AlertConfig>(async (alert) =>
                {
                    var service = DependencyService.Get<IUserDialogService>();
                    await service.ShowAlertAsync(alert);
                    alert.OnOk();                    
                });
            }
        }

        public ICommand ShowActionSheetCommand
        {
            get
            {
                return new Command(() =>
                {
                    var cfg = new ActionSheetConfig { Title = "Test Title" };
                    for (var i = 0; i < 10; i++)
                    {
                        var display = (i + 1);
                        cfg.Add("Option " + display, () => Result = String.Format("Option {0} Selected", display));
                    }
                    var service = DependencyService.Get<IUserDialogService>();
                    service.ShowActionSheet(cfg);
                });
            }
        }

        public ICommand ShowConfirmCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var service = DependencyService.Get<IUserDialogService>();
                    var r = await service.ShowConfirmAsync("Pick a choice", "Pick Title", "Yes", "No");
                    var text = (r ? "Yes" : "No");
                    Result = "Confirmation Choice: " + text;
                });
            }
        }

        public ICommand ShowToastCommand
        {
            get
            {
                return new Command<ToastConfig>(async (toast) =>
                {
                    var service = DependencyService.Get<IUserDialogService>();
                    await service.ShowToastAsync(toast);                    
                });
            }
        }

        public ICommand ShowPrompt
        {
            get { return ShowPromptCommand(false); }
        }

        public ICommand ShowPromptSecure
        {
            get { return ShowPromptCommand(true); }
        }

        private ICommand ShowPromptCommand(bool secure)
        {
            return new Command(async () =>
            {
                var type = (secure ? "secure text" : "text");
                var service = DependencyService.Get<IUserDialogService>();
                var r = await service.ShowPromptAsync(String.Format("Enter a {0} value", type.ToUpper()), secure: secure);
                this.Result = (r.Ok
                    ? "OK " + r.Text
                    : secure + " Prompt Cancelled"
                );
            });
        }
    }
}

