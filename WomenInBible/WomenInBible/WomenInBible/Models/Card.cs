using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Data;

namespace WomenInBible.Models
{
    public class Card : ObservableObject, IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _front;
        public string Front
        {
            get { return _front; }
            set { SetProperty(ref _front, value, () => Front); }
        }

        private string _back;
        public string Back
        {
            get { return _back; }
            set { SetProperty(ref _back, value, () => Back); }
        }

        private string _insight;
        public string Insight
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
