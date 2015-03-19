using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WomenInBible.Services;
using WomenInBible.Models;
using System.Threading;
using Xamarin.Forms;
using Android.Text;
using Android.Text.Method;

[assembly: Xamarin.Forms.Dependency(typeof(WomenInBible.Droid.Implementations.UserDialogService_Android))]
namespace WomenInBible.Droid.Implementations
{
    public class UserDialogService_Android : AbstractUserDialogService
    {
        public override void ShowAlert(AlertConfig config)
        {
            RequestMainThread(() =>
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetPositiveButton(config.OkText, (o, e) =>
                    {
                        if (config.OnOk != null)
                            config.OnOk();
                    })
                    .Show()
            );
        }

        public override void ShowActionSheet(ActionSheetConfig config)
        {
            var array = config
                .Options
                .Select(x => x.Text)
                .ToArray();

            RequestMainThread(() =>
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetTitle(config.Title)
                    .SetItems(array, (sender, args) => config.Options[args.Which].Action())
                    .Show()
            );
        }

        public override void ShowConfirm(ConfirmConfig config)
        {
            RequestMainThread(() =>
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetPositiveButton(config.OkText, (o, e) => config.OnConfirm(true))
                    .SetNegativeButton(config.CancelText, (o, e) => config.OnConfirm(false))
                    .Show()
            );
        }

        public override void ShowPrompt(PromptConfig config)
        {
            RequestMainThread(() =>
            {
                var txt = new EditText(Forms.Context)
                {
                    Hint = config.Placeholder
                };
                txt.SetMaxLines(1);
                if (config.IsSecure)
                {
                    txt.TransformationMethod = PasswordTransformationMethod.Instance;
                    txt.InputType = InputTypes.ClassText | InputTypes.TextVariationPassword;
                }
                new AlertDialog
                    .Builder(Forms.Context)
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetView(txt)
                    .SetPositiveButton(config.OkText, (o, e) =>
                        config.OnResult(new PromptResult
                        {
                            Ok = true,
                            Text = txt.Text
                        })
                    )
                    .SetNegativeButton(config.CancelText, (o, e) =>
                        config.OnResult(new PromptResult
                        {
                            Ok = false,
                            Text = txt.Text
                        })
                    )
                    .Show();
            });
        }


        public override void ShowToast(ToastConfig config)
        {
            RequestMainThread(() =>
            {
                config.OnClick = config.OnClick ?? (() => { });

                ToastLength timeout = ToastLength.Short;

                if (config.TimeoutSeconds >= 3)
                    timeout = ToastLength.Long;

                var myHandler = new Handler();

                myHandler.Post(() => {
                    var toast = global::Android.Widget.Toast.MakeText(Forms.Context, config.Message, timeout);
                    // TODO: add OnClick
                    toast.Show();
                });                
            });
        }

        public static void RequestMainThread(Action action)
        {
            if (Android.App.Application.SynchronizationContext == SynchronizationContext.Current)
                action();            
        }
    }
}