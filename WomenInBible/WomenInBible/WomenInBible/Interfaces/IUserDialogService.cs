using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Models;

namespace WomenInBible.Interfaces
{
    public interface IUserDialogService
    {
        void ShowAlert(AlertConfig config);
        void ShowActionSheet(ActionSheetConfig config);
        void ShowConfirm(ConfirmConfig config);
        void ShowPrompt(PromptConfig config);        
        void ShowToast(ToastConfig config);

        Task ShowAlertAsync(string message, string title = null, string okText = "OK");
        Task ShowAlertAsync(AlertConfig config);
        Task<bool> ShowConfirmAsync(string message, string title = null, string okText = "OK", string cancelText = "Cancel");
        Task<bool> ShowConfirmAsync(ConfirmConfig config);        
        Task<PromptResult> ShowPromptAsync(string message, string title = null, string okText = "OK", string cancelText = "Cancel", string placeholder = "", bool secure = false);
        Task<PromptResult> ShowPromptAsync(PromptConfig config);
        Task ShowToastAsync(ToastConfig config);
    }
}
