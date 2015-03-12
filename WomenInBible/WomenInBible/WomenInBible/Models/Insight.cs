using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WomenInBible.Managers;
using WomenInBible.Messages;
using WomenInBible.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Data;

namespace WomenInBible.Models
{
    public class Insight : ObservableObject, IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, () => Name); }
        }

        private int _isFavorite;
        public int IsFavorite
        {
            get { return _isFavorite; }
            set { SetProperty(ref _isFavorite, value, () => IsFavorite); }
        }

        private string _insightImage;
        public string InsightImage
        {
            get { return _insightImage; }
            set { SetProperty(ref _insightImage, value, () => InsightImage); }
        }

        private ICommand _deleteFavoriteInsightCommand;
        [Ignore]
        public ICommand DeleteFavoriteInsightCommand
        {
            get
            {
                return _deleteFavoriteInsightCommand ?? (_deleteFavoriteInsightCommand = new Command(
                 async () =>
                 {
                     IsFavorite = 0;
                     await IoC.Resolve<DatabaseManager>().UpdateAsync<Insight>(this, (ins) => ins.Id == Id);
                     MessagingCenter.Send<FavoriteInsightRemovedMessage>(
                         new FavoriteInsightRemovedMessage { InsightId = Id }, "Favorite Insight removed");
                 }, () => true));
            }
        }

        public void FillAllProperties<T>(T item)
        {
            var insight = item as Insight;
            Name = insight.Name;
            IsFavorite = insight.IsFavorite;
            InsightImage = insight.InsightImage;
        }        
    }
}
