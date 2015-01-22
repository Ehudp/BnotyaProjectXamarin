using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WomenInBible.Managers;
using WomenInBible.Models;
using WomenInBible.Services;
using Xamarin.Forms;

namespace WomenInBible.ViewModels
{
    public class InsightListViewModel : ViewModelBase
    {
        Random _random = new Random();

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, () => Title); }
        }

        private ObservableCollection<Insight> _favoriteInsights;
        public ObservableCollection<Insight> FavoriteInsights
        {
            get { return _favoriteInsights; }
            set { SetProperty(ref _favoriteInsights, value, () => FavoriteInsights); }
        }

        private Insight _selectedFavoriteInsight;
        public Insight SelectedFavoriteInsight
        {
            get { return _selectedFavoriteInsight; }
            set { SetProperty(ref _selectedFavoriteInsight, value, () => SelectedFavoriteInsight); }
        }

        private string _undoButtonTitle;
        public string UndoButtonTitle
        {
            get { return _undoButtonTitle; }
            set { SetProperty(ref _undoButtonTitle, value, () => UndoButtonTitle); }
        }

        private string _openRandomInsightButtonTitle;
        public string OpenRandomInsightButtonTitle
        {
            get { return _openRandomInsightButtonTitle; }
            set { SetProperty(ref _openRandomInsightButtonTitle, value, () => OpenRandomInsightButtonTitle); }
        }

        private ICommand _selectFavoriteInsightCommand;
        public ICommand SelectFavoriteInsightCommand
        {
            get
            {
                return _selectFavoriteInsightCommand ?? (_selectFavoriteInsightCommand = new Command(
                  async () =>
                  {
                      var navParam = new Dictionary<string, object>();
                      navParam.Add(SelectedFavoriteInsight.ToString(), SelectedFavoriteInsight);
                      await ShowViewModel<InsightViewModel>(navParam);
                  }, () => true));
            }
        }

        private ICommand _deleteFavoriteInsightCommand;
        public ICommand DeleteFavoriteInsightCommand
        {
            get
            {
                return _deleteFavoriteInsightCommand ?? (_deleteFavoriteInsightCommand = new Command(
                 async () =>
                 {
                     SelectedFavoriteInsight.IsFavorite = false;
                     await IoC.Resolve<DatabaseManager>()
                         .UpdateAsync<Insight>(SelectedFavoriteInsight, (ins) => ins.Id == SelectedFavoriteInsight.Id);
                     FavoriteInsights.Remove(SelectedFavoriteInsight);
                 }, () => true));
            }
        }

        private ICommand _undoCommand;
        public ICommand UndoCommand
        {
            get
            {
                return _undoCommand ?? (_undoCommand = new Command(
                  async () =>
                  {
                      // TODO: add Undo command
                  }, () => true));
            }
        }

        private ICommand _openRandomInsightCommand;
        public ICommand OpenRandomInsightCommand
        {
            get
            {
                return _openRandomInsightCommand ?? (_openRandomInsightCommand = new Command(
                  async () =>
                  {
                      var insights = await IoC.Resolve<DatabaseManager>().QueryAllAsync<Insight, int>((ins) => ins.Id);
                      var random = _random.Next(0, insights.Count);
                      var navParam = new Dictionary<string, object>();
                      navParam.Add("Insight", insights[random]);
                      await ShowViewModel<InsightViewModel>(navParam);
                  }, () => true));
            }
        }

        public InsightListViewModel()
        {
            Title = "Insight List";
            UndoButtonTitle = "Undo";
            OpenRandomInsightButtonTitle = "Open Random Insight";
            
            var results = Task.Run(async () => await IoC.Resolve<InsightService>().
                GetFavoriteInsights()).ConfigureAwait(false).GetAwaiter().GetResult();
            FavoriteInsights = new ObservableCollection<Insight>(results);
        }
    }
}
