using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WomenInBible.CustomViews
{
    public class MenuHeaderCell : ViewCell
    {
        protected Label _label;

        public string Text
        {
            get { return _label.Text; }
            set { _label.Text = value; }
        }

        public MenuHeaderCell()
        {
            _label = new Label
            {
                YAlign = TextAlignment.Center,
                TextColor = Color.White,                
                FontAttributes = FontAttributes.Bold,
                FontSize = 18
            };

            Height = 40;
            
            View = new StackLayout
            {
                Padding = new Thickness(4, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.FromHex("#233442"),
                Children = { _label }
            };
        }

        protected override void OnTapped()
        {
            return;
        }
    }
}
