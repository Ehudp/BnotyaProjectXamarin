using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WomenInBible.CustomViews
{
    public class MenuHeader : ViewCell
    {
        public MenuHeader()
        {
            var label = new Label()
            {
                Text = "Bnotya App",
                TextColor = Color.Gray,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20
            };           

            Height = 60;

            View = new StackLayout
            {
                Padding = new Thickness(20),
                BackgroundColor = Color.Yellow,
                Children = { label }
            };
        }
    }
}
