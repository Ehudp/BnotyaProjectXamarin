using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WomenInBible.Managers;
using WomenInBible.Models;
using Xamarin.Forms;

namespace WomenInBible.ViewModels
{
    public class WomenListViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, () => Title); }
        }

        private string _searchPlaceholder;
        public string SearchPlaceholder
        {
            get { return _searchPlaceholder; }
            set { SetProperty(ref _searchPlaceholder, value, () => SearchPlaceholder); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value, () => SearchText); }
        }

        private string _openInsightListButtonTitle;
        public string OpenInsightListButtonTitle
        {
            get { return _openInsightListButtonTitle; }
            set { SetProperty(ref _openInsightListButtonTitle, value, () => OpenInsightListButtonTitle); }
        }

        private string _openTriviaButtonTitle;
        public string OpenTriviaButtonTitle
        {
            get { return _openTriviaButtonTitle; }
            set { SetProperty(ref _openTriviaButtonTitle, value, () => OpenTriviaButtonTitle); }
        }

        public ICommand OpenInsightListCommand
        {
            get
            {
                return new Command(async (arg) =>
                {
                    await NavigationManager.NavigateTo(new WomenListViewModel());
                });
            }
        }

        public ICommand OpenTriviaCommand
        {
            get { return new Command(async (arg) => await NavigationManager.NavigateTo(new TehilotViewModel())); }
        }

        public ICommand OpenCardCommand
        {
            get { return new Command<Woman>(async (arg) => await NavigationManager.NavigateTo(new CardViewModel(arg.CurrentCard))); }
        }

        private ObservableCollection<Woman> _womenList;
        public ObservableCollection<Woman> WomenList
        {
            get { return _womenList; }
            set { SetProperty(ref _womenList, value, () => WomenList); }
        }

        private Woman _selectedWoman;
        public Woman SelectedWoman
        {
            get { return _selectedWoman; }
            set { SetProperty(ref _selectedWoman, value, () => SelectedWoman); }
        }

        public WomenListViewModel()
        {
            OpenTriviaButtonTitle = "Open Trivia Page";
            OpenInsightListButtonTitle = "Open Insight List";
            SearchPlaceholder = "Search";
            Title = "Women List";

            WomenList = new ObservableCollection<Woman>
            {
                new Woman
                {
                    Name = "T1",
                    Icon = "ic_action_search.png"
                },
                new Woman
                {
                    Name = "T2",
                    Icon = "ic_action_search.png"                    
                }
            };

            CreateCardsAsync();           
        }

        private async Task CreateCardsAsync()
        {
            await Task.Run(() =>
            {
                for (int i = 1; i <= WomenList.Count; i++)
                {
                    WomenList[i - 1].CurrentCard = new Card
                        {
                            Front = string.Format("card{0}.png", i),
                            Back = string.Format("card{0}a.png", i),
                            Insight = string.Format("card{0}b.png", i)
                        };
                }
            }).ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
