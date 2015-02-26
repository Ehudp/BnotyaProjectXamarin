using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.CustomControls;
using WomenInBible.CustomViews;
using WomenInBible.Managers;
using WomenInBible.Models;
using WomenInBible.Services;
using WomenInBible.ViewModels;
using WomenInBible.Views;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Mvvm;
using Xamarin.Forms.Labs.Services;

namespace WomenInBible
{
    public class App : Application
    {
        public static INavigation Navigation { get; set; }

        public App()
        {
            InitApp();
            //var vm = new SplashViewModel();
            //MainPage = vm.ResolveView();
            //vm.AfterLoading();

            MainPage = GetMainPage();
        }

        public static MasterDetailPage GetMainPage()
        {
            var rootPage = new MasterDetailPage();
            rootPage.Master = new MenuPage(rootPage);
            //rootPage.Master = new MenuViewModel().ResolveView();
            rootPage.Detail = new NavigationPage(ViewFactory.CreatePage<HomeViewModel>());
            Navigation = rootPage.Detail.Navigation;

            return rootPage;
        }

        public static void SetMainPage()
        {
            Current.MainPage = GetMainPage();
        }        

        private void InitApp()
        {
            if (!Resolver.IsSet)
            {
                Resolver.SetResolver(IoC.Container.GetResolver());
            }           

            ViewFactory.Register<HomeView, HomeViewModel>();
            ViewFactory.Register<WomenListView, WomenListViewModel>();
            ViewFactory.Register<TehilotView, TehilotViewModel>();            
            ViewFactory.Register<CardView, CardViewModel>();
            ViewFactory.Register<SplashView, SplashViewModel>();
            ViewFactory.Register<TriviaView, TriviaViewModel>();
            ViewFactory.Register<InsightListView, InsightListViewModel>();
            ViewFactory.Register<InsightView, InsightViewModel>();
            ViewFactory.Register<SettingsView, SettingsViewModel>();    
        }
    }
}
