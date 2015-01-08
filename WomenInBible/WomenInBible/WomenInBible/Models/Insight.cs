using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;

namespace WomenInBible.Models
{
    public class Insight : ViewModelBase, IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, () => Name); }
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set { SetProperty(ref _isFavorite, value, () => IsFavorite); }
        }

        public void FillAllProperties<T>(T item)
        {
            var insight = item as Insight;
            Name = insight.Name;
            IsFavorite = insight.IsFavorite;
        }

        public static async Task<IEnumerable<Insight>> GetFavoriteInsights()
        {
            return await App.DBManager.QuerySelectedAsync<Insight, string>(x => x.IsFavorite, x => x.Name);
        }
    }
}
