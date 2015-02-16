using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WomenInBible.Managers;
using Xamarin.Forms;

namespace WomenInBible.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, () => Title); }
        }

        private string _userProfileTitle;
        public string UserProfileTitle
        {
            get { return _userProfileTitle; }
            set { SetProperty(ref _userProfileTitle, value, () => UserProfileTitle); }
        }

        private string _userNamePlaceholder;
        public string UserNamePlaceholder
        {
            get { return _userNamePlaceholder; }
            set { SetProperty(ref _userNamePlaceholder, value, () => UserNamePlaceholder); }
        }

        private string _userNameText;
        public string UserNameText
        {
            get { return _userNameText; }
            set 
            { 
                SetProperty(ref _userNameText, value, () => UserNameText);
                ((Command)ApproveUserNameCommand).ChangeCanExecute();
            }
        }

        private string _musicPrefTitle;
        public string MusicPrefTitle
        {
            get { return _musicPrefTitle; }
            set { SetProperty(ref _musicPrefTitle, value, () => MusicPrefTitle); }
        }

        private string _notificationPrefTitle;
        public string NotificationPrefTitle
        {
            get { return _notificationPrefTitle; }
            set { SetProperty(ref _notificationPrefTitle, value, () => NotificationPrefTitle); }
        }

        private string _approveUserNameButtonTitle;
        public string ApproveUserNameButtonTitle
        {
            get { return _approveUserNameButtonTitle; }
            set { SetProperty(ref _approveUserNameButtonTitle, value, () => ApproveUserNameButtonTitle); }
        }

        private string _clearUserNameButtonTitle;
        public string ClearUserNameButtonTitle
        {
            get { return _clearUserNameButtonTitle; }
            set { SetProperty(ref _clearUserNameButtonTitle, value, () => ClearUserNameButtonTitle); }
        }

        private ICommand _approveUserNameCommand;
        public ICommand ApproveUserNameCommand
        {
            get
            {
                return _approveUserNameCommand ?? (_approveUserNameCommand = new Command(
                  () => IoC.Resolve<SettingsManager>().StringSetting = UserNameText, 
                  () => !string.IsNullOrEmpty(UserNameText)));
            }
        }

        private ICommand _clearUserNameCommand;
        public ICommand ClearUserNameCommand
        {
            get
            {
                return _clearUserNameCommand ?? (_clearUserNameCommand = new Command(
                  () => UserNameText = "", () => true));
            }
        }

        public SettingsViewModel()
        {
            Title = "Settings";
            UserProfileTitle = "User Profile";
            UserNamePlaceholder = "Enter User Name";
            MusicPrefTitle = "Music Preferences";
            NotificationPrefTitle = "Notification Preferences";
            ApproveUserNameButtonTitle = "OK";
            ClearUserNameButtonTitle = "Clear";
        }

        //protected async override void ParametersReceived(Dictionary<string, object> navigationParameters)
        //{            
            
        //}
    }
}
