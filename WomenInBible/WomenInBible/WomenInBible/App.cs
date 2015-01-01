using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WomenInBible.CustomViews;
using WomenInBible.Managers;
using WomenInBible.ViewModels;
using WomenInBible.Views;
using Xamarin.Forms;

namespace WomenInBible
{
    public class App
    {       
        public static INavigation Navigation { get; set; }

        public static Page GetMainPage()
        {
            NavigationManager.RegisterView(typeof(HomeViewModel), typeof(HomeView));
            NavigationManager.RegisterView(typeof(WomenListViewModel), typeof(WomenListView));
            NavigationManager.RegisterView(typeof(TehilotViewModel), typeof(TehilotView));
            NavigationManager.RegisterView(typeof(MenuViewModel), typeof(MenuView));
            NavigationManager.RegisterView(typeof(CardViewModel), typeof(CardView));
            
            var rootPage = new MasterDetailPage();
            rootPage.Master = new MenuPage(rootPage);
            //rootPage.Master = new MenuViewModel().ResolveView();
            rootPage.Detail = new NavigationPage(new HomeViewModel().ResolveView());
            Navigation = rootPage.Detail.Navigation;
            return rootPage;
        } 
    }
}
