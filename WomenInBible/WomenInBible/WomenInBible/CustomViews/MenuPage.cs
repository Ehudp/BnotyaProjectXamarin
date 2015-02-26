using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;
using WomenInBible.Views;
using WomenInBible.Managers;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Mvvm;
using WomenInBible.Providers;

namespace WomenInBible.CustomViews
{
    /// <summary>
    /// Required for PlatformRenderer
    /// </summary>
    public class MenuTableView : TableView
    {
    }

    public class MenuPage : ContentPage
    {
        private MasterDetailPage _mainPage;
        private TableView _tableView;

        public MenuPage(MasterDetailPage mainPage)
        {
            _mainPage = mainPage;

            Title = "Bnotya App";
            Icon = "ic_drawer.png";

            var section = new TableSection() {
                                    new MenuHeaderCell {Text = "Home"},
                                    new MenuCell(this) {Text = "Open Home"},                                          
                                    new MenuHeaderCell {Text = "Tehilot"},
                                    new MenuCell(this) {Text = "Open Tehilot"},
                                    new MenuHeaderCell {Text = "Women"},
                                    new MenuCell(this) {Text = "Open Women List"},
                                    new MenuCell(this) {Text = "Open Trivia Page"},
                                    new MenuCell(this) {Text = "Open Insight List"},
                                    };

            var root = new TableRoot() { section };

            _tableView = new MenuTableView()
            {
                Root = root,
                // HeaderTemplate = new DataTemplate (typeof(MenuHeader)),
                Intent = TableIntent.Data,
                HasUnevenRows = false
            };

            Content = new StackLayout
            {
                BackgroundColor = Color.Gray,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { _tableView }
            };

            ToolbarItems.Add(new ToolbarItem("Test", "ic_action_mail.png", () => { }));
            ToolbarItems.Add(new ToolbarItem("Test", "ic_action_overflow_light.png", async () => await OnActivation()));
        }

        public void Selected(string item)
        {
            switch (item)
            {
                case "Open Home":
                    var home = new NavigationPage(ViewFactory.CreatePage<HomeViewModel>());
                    _mainPage.Detail = home;
                    App.Navigation = home.Navigation;
                    break;
                case "Open Tehilot":
                    var tehilot = new NavigationPage(ViewFactory.CreatePage<TehilotViewModel>());
                    _mainPage.Detail = tehilot;
                    App.Navigation = tehilot.Navigation;
                    break;
                case "Open Women List":
                    var women = new NavigationPage(ViewFactory.CreatePage<WomenListViewModel>());
                    _mainPage.Detail = women;
                    App.Navigation = women.Navigation;
                    break;
                case "Open Trivia Page":
                    var trivia = new NavigationPage(ViewFactory.CreatePage<TriviaViewModel>());
                    _mainPage.Detail = trivia;
                    App.Navigation = trivia.Navigation;
                    break;
                case "Open Insight List":
                    var insights = new NavigationPage(ViewFactory.CreatePage<InsightListViewModel>());
                    _mainPage.Detail = insights;
                    App.Navigation = insights.Navigation;
                    break;
            };
            _mainPage.IsPresented = false;  // close the slide-out
        }

        private async Task OnActivation()
        {
            string action = "";

            if (App.Navigation.NavigationStack[App.Navigation.NavigationStack.Count - 1] is SettingsView)
                action = await DisplayActionSheet("Menu", "Cancel", null, "About", "Home");
            else
            {
                if (Device.OS == TargetPlatform.Android)
                    action = await DisplayActionSheet("Menu", "Cancel", null, "Settings", "About", "Exit");
                else if (Device.OS == TargetPlatform.iOS)
                    action = await DisplayActionSheet("Menu", "Cancel", null, "Settings", "About");
            }

            switch (action)
            {
                case "Settings":
                    await IoC.Resolve<NavigationProvider>().ShowViewModel<SettingsViewModel>();
                    break;

                case "About":
                    
                    break;

                case "Exit":
                    DependencyService.Get<IMethods>().Close_App();
                    break;

                case "Home":
                    var home = new NavigationPage(ViewFactory.CreatePage<HomeViewModel>());
                    _mainPage.Detail = home;
                    App.Navigation = home.Navigation;
                    break;
            }
        }
    }
}