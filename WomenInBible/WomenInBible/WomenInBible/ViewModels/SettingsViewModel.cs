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

        private string _musicSwitchTitle;
        public string MusicSwitchTitle
        {
            get { return _musicSwitchTitle; }
            set { SetProperty(ref _musicSwitchTitle, value, () => MusicSwitchTitle); }
        }

        private string _musicSwitchLegend;
        public string MusicSwitchLegend
        {
            get { return _musicSwitchLegend; }
            set { SetProperty(ref _musicSwitchLegend, value, () => MusicSwitchLegend); }
        }

        private string _notificationSwitchTitle;
        public string NotificationSwitchTitle
        {
            get { return _notificationSwitchTitle; }
            set { SetProperty(ref _notificationSwitchTitle, value, () => NotificationSwitchTitle); }
        }

        private string _notificationSwitchLegend;
        public string NotificationSwitchLegend
        {
            get { return _notificationSwitchLegend; }
            set { SetProperty(ref _notificationSwitchLegend, value, () => NotificationSwitchLegend); }
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

        private double _musicVolume;
        public double MusicVolume
        {
            get { return _musicVolume; }
            set 
            { 
                SetProperty(ref _musicVolume, value, () => MusicVolume);
                MusicVolumeDetail = string.Format("Current value is {0}", (int)MusicVolume);
            }
        }

        private string _musicVolumeTitle;
        public string MusicVolumeTitle
        {
            get { return _musicVolumeTitle; }
            set { SetProperty(ref _musicVolumeTitle, value, () => MusicVolumeTitle); }
        }

        private string _musicVolumeDetail;
        public string MusicVolumeDetail
        {
            get { return _musicVolumeDetail; }
            set { SetProperty(ref _musicVolumeDetail, value, () => MusicVolumeDetail); }
        }

        private ICommand _approveUserNameCommand;
        public ICommand ApproveUserNameCommand
        {
            get
            {
                return _approveUserNameCommand ?? (_approveUserNameCommand = new Command(
                  () => IoC.Resolve<SettingsManager>().UserNameSetting = UserNameText, 
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

        private ICommand _initMusicCommand;
        public ICommand InitMusicCommand
        {
            get
            {
                return _initMusicCommand ?? (_initMusicCommand = new Command<bool>(
                  (value) => IoC.Resolve<SettingsManager>().InitMusicSetting = value, 
                  (value) => true));
            }
        }

        private ICommand _initNotificationCommand;
        public ICommand InitNotificationCommand
        {
            get
            {
                return _initNotificationCommand ?? (_initNotificationCommand = new Command<bool>(
                  (value) => IoC.Resolve<SettingsManager>().InitNotificationSetting = value,
                  (value) => true));
            }
        }

        public SettingsViewModel()
        {
            Title = "Settings";
            UserProfileTitle = "User Profile";
            UserNamePlaceholder = "Set your username";
            MusicPrefTitle = "Music Preferences";
            NotificationPrefTitle = "Notification Preferences";
            ApproveUserNameButtonTitle = "OK";
            ClearUserNameButtonTitle = "Clear";
            MusicSwitchTitle = "Music";
            MusicSwitchLegend = "Music On/Off";
            NotificationSwitchTitle = "Notification";
            NotificationSwitchLegend = "Notification On/Off";
            MusicVolumeTitle = "Music Volume";
            MusicVolumeDetail = string.Format("Current value is {0}", (int)MusicVolume);

            MusicVolume = 50;            
        }

        //protected async override void ParametersReceived(Dictionary<string, object> navigationParameters)
        //{            
            
        //}
    }
}
