using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WomenInBible.CustomViews
{
    public class MenuCell : MenuHeaderCell
    {
        public MenuPage Host { get; set; }
        private Image _image;

        public MenuCell(MenuPage host)
        {
            Host = host;

            _image = new Image
            {
                Source = "ic_action_search.png"
            };

            _label = new Label
            {
                YAlign = TextAlignment.Center,
                TextColor = Color.White,
                FontAttributes = FontAttributes.None,
                FontSize = 14
            };

            Height = 40;

            View = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.FromHex("#15776f"),
                Children = { _image, _label }
            };
        }

        protected override void OnTapped()
        {
            base.OnTapped();
            Host.Selected(_label.Text);
        }
    }
}
