﻿using System;
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
                ((Command)ClearUserNameCommand).ChangeCanExecute();
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

        private bool _notificationToggled;
        public bool NotificationToggled
        {
            get { return _notificationToggled; }
            set { SetProperty(ref _notificationToggled, value, () => NotificationToggled); }
        }

        private string _notificationTimeTitle;
        public string NotificationTimeTitle
        {
            get { return _notificationTimeTitle; }
            set { SetProperty(ref _notificationTimeTitle, value, () => NotificationTimeTitle); }
        }

        private TimeSpan _notificationTime;
        public TimeSpan NotificationTime
        {
            get { return _notificationTime; }
            set 
            { 
                SetProperty(ref _notificationTime, value, () => NotificationTime);
                IoC.Resolve<SettingsManager>().NotificationTimeSetting = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(NotificationTime);
            }
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

        private string _clearUserNameButtonImage;
        public string ClearUserNameButtonImage
        {
            get { return _clearUserNameButtonImage; }
            set { SetProperty(ref _clearUserNameButtonImage, value, () => ClearUserNameButtonImage); }
        }

        private bool _musicToggled;
        public bool MusicToggled
        {
            get { return _musicToggled; }
            set { SetProperty(ref _musicToggled, value, () => MusicToggled); }
        }

        private double _musicVolume;
        public double MusicVolume
        {
            get { return _musicVolume; }
            set 
            { 
                SetProperty(ref _musicVolume, value, () => MusicVolume);
                int volume = (int)MusicVolume;
                MusicVolumeDetail = string.Format("Current value is {0}", volume);
                IoC.Resolve<SettingsManager>().MusicVolumeSetting = volume;
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
                  () => UserNameText = "", () => !string.IsNullOrEmpty(UserNameText)));
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
            ClearUserNameButtonImage = "clear_gray.png";
            MusicSwitchTitle = "Music";
            MusicSwitchLegend = "Music On/Off";
            NotificationSwitchTitle = "Notification";
            NotificationSwitchLegend = "Notification On/Off";
            NotificationTimeTitle = "Notification Time";
            MusicVolumeTitle = "Music Volume";
            MusicVolumeDetail = string.Format("Current value is {0}", (int)MusicVolume);          
        }

        public override void ParametersReceived(Dictionary<string, object> navigationParameters)
        {
            var settings = IoC.Resolve<SettingsManager>();

            MusicVolume = settings.MusicVolumeSetting;
            NotificationToggled = settings.InitNotificationSetting;
            MusicToggled = settings.InitMusicSetting;
            NotificationTime = settings.NotificationTimeSetting.TimeOfDay;
            UserNameText = settings.UserNameSetting;
        }
    }
}
