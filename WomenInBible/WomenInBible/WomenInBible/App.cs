using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.CustomViews;
using WomenInBible.Managers;
using WomenInBible.Models;
using WomenInBible.ViewModels;
using WomenInBible.Views;
using Xamarin.Forms;

namespace WomenInBible
{
    public class App : Application 
    {       
        public static INavigation Navigation { get; set; }
        public static DatabaseManager DBManager { get; set; }

        public App()
        {
            MainPage = GetMainPage();
        }

        public static Page GetMainPage()
        {
            NavigationManager.RegisterView(typeof(HomeViewModel), typeof(HomeView));
            NavigationManager.RegisterView(typeof(WomenListViewModel), typeof(WomenListView));
            NavigationManager.RegisterView(typeof(TehilotViewModel), typeof(TehilotView));
            NavigationManager.RegisterView(typeof(MenuViewModel), typeof(MenuView));
            NavigationManager.RegisterView(typeof(CardViewModel), typeof(CardView));

            DBManager = new DatabaseManager();
            CreateDb();
            
            var rootPage = new MasterDetailPage();
            rootPage.Master = new MenuPage(rootPage);
            //rootPage.Master = new MenuViewModel().ResolveView();
            rootPage.Detail = new NavigationPage(new HomeViewModel().ResolveView());
            Navigation = rootPage.Detail.Navigation;
            return rootPage;
        }

        public static void CreateDb() // TODO: For tests only
        {
            Task.Run(async () =>
                {
                    Question question1 = new Question { Content = "Who is the President of US?" };
                    Question question2 = new Question { Content = "Who is the President of Mars?" };
                    Answer answer1 = new Answer { Content = "Yoda" };
                    Answer answer2 = new Answer { Content = "Bibi" };
                    // Inserting answers in DB
                    answer1.Id = await DBManager.InsertAsync<Answer>(answer1, x => x.Id == answer1.Id);
                    answer2.Id = await DBManager.InsertAsync<Answer>(answer2, x => x.Id == answer2.Id);
                    // Inserting questions in DB
                    await Question.CreateQuestion(question1, new List<Answer> { answer1, answer2 }, answer1.Id);
                    await Question.CreateQuestion(question2, new List<Answer> { answer1, answer2 }, answer2.Id);
                });
        }
    }
}
