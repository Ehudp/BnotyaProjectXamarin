using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Interfaces;
using WomenInBible.Models;

namespace WomenInBible.Services
{
    public abstract class AbstractUserDialogService : IUserDialogService
    {
        public abstract void ShowAlert(AlertConfig config);
        public abstract void ShowActionSheet(ActionSheetConfig config);
        public abstract void ShowConfirm(ConfirmConfig config);
        public abstract void ShowPrompt(PromptConfig config);
        public abstract void ShowToast(ToastConfig config);

        public virtual Task ShowAlertAsync(string message, string title, string okText)
        {
            var tcs = new TaskCompletionSource<object>();
            ShowAlert(new AlertConfig
            {
                Message = message,
                Title = title,
                OkText = okText,
                OnOk = () => tcs.TrySetResult(null)
            });
            return tcs.Task;
        }

        public virtual Task ShowAlertAsync(AlertConfig config)
        {
            var tcs = new TaskCompletionSource<object>();
            config.OnOk = () => tcs.TrySetResult(null);
            ShowAlert(config);
            return tcs.Task;
        }

        public virtual Task<bool> ShowConfirmAsync(string message, string title, string okText, string cancelText)
        {
            var tcs = new TaskCompletionSource<bool>();
            ShowConfirm(new ConfirmConfig
            {
                Message = message,
                CancelText = cancelText,
                OkText = okText,
                OnConfirm = x => tcs.TrySetResult(x)
            });
            return tcs.Task;
        }

        public virtual Task<bool> ShowConfirmAsync(ConfirmConfig config)
        {
            var tcs = new TaskCompletionSource<bool>();
            config.OnConfirm = x => tcs.TrySetResult(x);
            ShowConfirm(config);
            return tcs.Task;
        }

        public virtual Task<PromptResult> ShowPromptAsync(string message, string title, string okText, string cancelText, string placeholder, bool secure)
        {
            var tcs = new TaskCompletionSource<PromptResult>();
            ShowPrompt(new PromptConfig
            {
                Message = message,
                Title = title,
                CancelText = cancelText,
                OkText = okText,
                Placeholder = placeholder,
                IsSecure = secure,
                OnResult = x => tcs.TrySetResult(x)
            });
            return tcs.Task;
        }

        public virtual Task<PromptResult> ShowPromptAsync(PromptConfig config)
        {
            var tcs = new TaskCompletionSource<PromptResult>();
            config.OnResult = x => tcs.TrySetResult(x);
            ShowPrompt(config);
            return tcs.Task;
        }

        public virtual Task ShowToastAsync(ToastConfig config)
        {
            var tcs = new TaskCompletionSource<object>();
            config.OnClick = () => tcs.TrySetResult(null);
            ShowToast(config);
            return tcs.Task;
        }
    }
}
