using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WomenInBible.Managers;
using WomenInBible.Models;
using Xamarin.Forms;

namespace WomenInBible.ViewModels
{
    public class InsightViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, () => Title); }
        }

        private Insight _currentInsight;
        public Insight CurrentInsight
        {
            get { return _currentInsight; }
            set { SetProperty(ref _currentInsight, value, () => CurrentInsight); }
        }

        private string _makeFavoriteButtonTitle;
        public string MakeFavoriteButtonTitle
        {
            get { return _makeFavoriteButtonTitle; }
            set { SetProperty(ref _makeFavoriteButtonTitle, value, () => MakeFavoriteButtonTitle); }
        }

        private ICommand _makeFavoriteCommand;
        public ICommand MakeFavoriteCommand
        {
            get
            {
                return _makeFavoriteCommand ?? (_makeFavoriteCommand = new Command(
                  async () =>
                {
                    CurrentInsight.IsFavorite = 1;
                    await IoC.Resolve<DatabaseManager>().UpdateAsync<Insight>(CurrentInsight, (ins) => ins.Id == CurrentInsight.Id);
                    await Navigation.PopAsync();
                }, () => true));
            }
        }

        public InsightViewModel()
        {            
            Title = "Insight";
            MakeFavoriteButtonTitle = "Make Favorite";
        }

        public override void ParametersReceived(Dictionary<string, object> navigationParameters)
        {
            CurrentInsight = (Insight)navigationParameters["Insight"];
        }
    }
}
