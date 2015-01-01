using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Models;

namespace WomenInBible.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private Card _card;
        public Card CurrentCard
        {
            get { return _card; }
            set { SetProperty(ref _card, value, () => CurrentCard); }
        }

        public CardViewModel(Card card)
        {
            CurrentCard = card;
        }
    }
}
