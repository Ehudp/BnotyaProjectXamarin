using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;
using WomenInBible.Views;
using WomenInBible.Managers;
using Xamarin.Forms;

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
        private MasterDetailPage _master;
        private NavigationPage _home, _women, _tehilot;
        private TableView _tableView;

        public MenuPage(MasterDetailPage master)
        {
            _master = master;

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
                //				HeaderTemplate = new DataTemplate (typeof(MenuHeader)),
                Intent = TableIntent.Data,
                HasUnevenRows = false
            };

            Content = new StackLayout
            {
                BackgroundColor = Color.Gray,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { _tableView }
            };
        }

        public void Selected(string item)
        {
            switch (item)
            {                
                case "Open Home":
                    if (_home == null)
                        _home = new NavigationPage(new HomeViewModel().ResolveView());
                    _master.Detail = _home;
                    App.Navigation = _home.Navigation;
                    break;                
                case "Open Tehilot":
                    if (_tehilot == null)
                        _tehilot = new NavigationPage(new TehilotViewModel().ResolveView());
                    _master.Detail = _tehilot;
                    App.Navigation = _tehilot.Navigation;
                    break;
                case "Open Women List":
                    if (_women == null)
                        _women = new NavigationPage(new WomenListViewModel().ResolveView());
                    _master.Detail = _women;
                    App.Navigation = _women.Navigation;
                    break;
                case "Open Trivia Page":
                    break;
                case "Open Insight List":
                    break;
            };
            _master.IsPresented = false;  // close the slide-out
        }
    }
}

