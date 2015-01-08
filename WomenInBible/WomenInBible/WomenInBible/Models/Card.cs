using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;
using Xamarin.Forms;

namespace WomenInBible.Models
{
    public class Card : ViewModelBase, IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private ImageSource _front;
        public ImageSource Front
        {
            get { return _front; }
            set { SetProperty(ref _front, value, () => Front); }
        }

        private ImageSource _back;
        public ImageSource Back
        {
            get { return _back; }
            set { SetProperty(ref _back, value, () => Back); }
        }

        private ImageSource _insight;
        public ImageSource Insight
        {
            get { return _insight; }
            set { SetProperty(ref _insight, value, () => Insight); }
        }

        public void FillAllProperties<T>(T item)
        {
            var card = item as Card;
            Front = card.Front;
            Back = card.Back;
            Insight = card.Insight;
        }
    }
}
