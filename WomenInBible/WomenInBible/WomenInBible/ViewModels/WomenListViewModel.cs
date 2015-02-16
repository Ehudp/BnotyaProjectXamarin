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
        private ObservableCollection<Woman> _originalWomenList;

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

        private ICommand _openInsightListCommand;
        public ICommand OpenInsightListCommand
        {
            get
            {
                return _openInsightListCommand ?? (_openInsightListCommand = new Command(
                  async () => await ShowViewModel<InsightListViewModel>(), () => true));
            }
        }

        private ICommand _openTriviaCommand;
        public ICommand OpenTriviaCommand
        {
            get
            {
                return _openTriviaCommand ?? (_openTriviaCommand = new Command(
                  async () => await ShowViewModel<TriviaViewModel>(), () => true));
            }
        }

        private ICommand _openCardCommand;
        public ICommand OpenCardCommand
        {
            get
            {
                return _openCardCommand ?? (_openCardCommand = new Command(
                  async () =>
                  {
                      var navParam = new Dictionary<string, object>();
                      navParam.Add("CardId", SelectedWoman.CardId);
                      await ShowViewModel<CardViewModel>(navParam);
                  }, () => true));
            }
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command(
                  (text) =>
                  {
                      if (!string.IsNullOrEmpty(SearchText))                      
                          WomenList = new ObservableCollection<Woman>(
                              _originalWomenList.Where(woman => woman.Name.ToLower().Contains(SearchText)));
                      else
                          WomenList = _originalWomenList;
                  }, (text) => true));
            }
        }

        private ICommand _clearSearchCommand;
        public ICommand ClearSearchCommand
        {
            get
            {
                return _clearSearchCommand ?? (_clearSearchCommand = new Command(
                  () => SearchText = "", () => true));
            }
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

            var result = Task.Run(async () =>
                await IoC.Resolve<DatabaseManager>().QueryAllAsync<Woman, int>(woman => woman.Id))
                .ConfigureAwait(false).GetAwaiter().GetResult();

            _originalWomenList = new ObservableCollection<Woman>(result); 
            WomenList = new ObservableCollection<Woman>(result);            
        }
    }
}
