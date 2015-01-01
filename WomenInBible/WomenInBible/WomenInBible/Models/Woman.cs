using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;
using Xamarin.Forms;

namespace WomenInBible.Models
{
    public class Woman : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, () => Name); }
        }

        private ImageSource _icon;
        public ImageSource Icon
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
    }
}
