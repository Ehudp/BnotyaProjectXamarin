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
    public class Woman : ObservableObject, IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, () => Name); }
        }

        private string _icon;
        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value, () => Icon); }
        }

        private Card _card;
        public Card CurrentCard
        {
            get { return _card; }
            set { SetProperty(ref _card, value, () => CurrentCard); }
        }

        private int _cardId;
        public int CardId
        {
            get { return _cardId; }
            set { SetProperty(ref _cardId, value, () => CardId); }
        }

        public void FillAllProperties<T>(T item)
        {
            var woman = item as Woman;
            Name = woman.Name;
            Icon = woman.Icon;
            CurrentCard = woman.CurrentCard;
            CardId = woman.CardId;
        }
    }
}
