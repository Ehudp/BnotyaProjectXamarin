﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using WomenInBible.Managers;
using WomenInBible.Models;
using WomenInBible.Services;
using WomenInBible.Messages;

namespace WomenInBible.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _openWomenListButtonTitle;
        public string OpenWomenListButtonTitle
        {
            get { return _openWomenListButtonTitle; }
            set { SetProperty(ref _openWomenListButtonTitle, value, () => OpenWomenListButtonTitle); }
        }

        private string _openTehilotButtonTitle;
        public string OpenTehilotButtonTitle
        {
            get { return _openTehilotButtonTitle; }
            set { SetProperty(ref _openTehilotButtonTitle, value, () => OpenTehilotButtonTitle); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, () => Title); }
        }

        private string _backgroundImage;
        public string BackgroundImage
        {
            get { return _backgroundImage; }
            set { SetProperty(ref _backgroundImage, value, () => BackgroundImage); }
        }

        private bool _isLoaded;
        public bool IsLoaded
        {
            get { return _isLoaded; }
            set { SetProperty(ref _isLoaded, value, () => IsLoaded); }
        } 

        private ICommand _openWomenListCommand;
        public ICommand OpenWomenListCommand
        {
            get
            {
                return _openWomenListCommand ?? (_openWomenListCommand = new Command(
                  async () => await ShowViewModel<WomenListViewModel>(), () => true));
            }
        }

        private ICommand _openTehilotCommand;
        public ICommand OpenTehilotCommand
        {
            get
            {
                return _openTehilotCommand ?? (_openTehilotCommand = new Command(
                  async () => await ShowViewModel<TehilotViewModel>(), () => true));
            }
        }

        private ICommand _openSettingsCommand;
        public ICommand OpenSettingsCommand
        {
            get
            {
                return _openSettingsCommand ?? (_openSettingsCommand = new Command(
                  async () => await ShowViewModel<SettingsViewModel>(), () => true));
            }
        }

        public HomeViewModel()
        {
            IsLoaded = false;
            Title = "Home";
            BackgroundImage = "clean_background.png";
            OpenWomenListButtonTitle = "Open Women List";
            OpenTehilotButtonTitle = "Open Tehilot Page";

            MessagingCenter.Subscribe<SplashFinishedMessage>(this, "Splash finished",
                (message) => IsLoaded = true);            
        }        
    }
}
