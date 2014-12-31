using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WomenInBible.CustomViews
{
    public class MenuCell : ViewCell
    {
        private Label _label;

        public string Text
        {
            get { return _label.Text; }
            set { _label.Text = value; }
        }

        public MenuPage Host { get; set; }

        public MenuCell(MenuPage host = null)
        {
            Host = host;

            _label = new Label
            {
                YAlign = TextAlignment.Center,
                TextColor = Color.White
            };

            Height = 40;

            if (Host != null)
            {
                _label.Font = Font.SystemFontOfSize(NamedSize.Medium);
                View = new StackLayout
                {
                    Padding = new Thickness(20, 0, 0, 0),
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    BackgroundColor = Color.Aqua,
                    Children = { _label }
                };                
            }
            else
            {
                _label.Font = Font.BoldSystemFontOfSize(NamedSize.Large);
                View = new StackLayout
                {
                    Padding = new Thickness(4, 0, 0, 0),
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    BackgroundColor = Color.Purple,
                    Children = { _label }
                };                             
            }
        }

        protected override void OnTapped()
        {
            if (Host == null) return;

            base.OnTapped();
            Host.Selected(_label.Text);
        }
    }
}
