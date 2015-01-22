using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Managers;
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

        private bool _isFavorite;
        public bool IsFavorite
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

        public void FillAllProperties<T>(T item)
        {
            var insight = item as Insight;
            Name = insight.Name;
            IsFavorite = insight.IsFavorite;
            InsightImage = insight.InsightImage;
        }        
    }
}
