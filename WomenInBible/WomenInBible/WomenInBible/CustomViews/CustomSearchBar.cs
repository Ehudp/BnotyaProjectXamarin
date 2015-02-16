using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WomenInBible.CustomViews
{
    public class CustomSearchBar : SearchBar
    {
        public static BindableProperty TextChangedCommandProperty =
            BindableProperty.Create<CustomSearchBar, ICommand>(x => x.TextChangedCommand, null);

        public ICommand TextChangedCommand
        {
            get { 
                return (ICommand)this.GetValue(TextChangedCommandProperty);
            }
            set { 
                this.SetValue(TextChangedCommandProperty, value); 
            }
        }

        public CustomSearchBar()
        {
            this.TextChanged += this.OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextChangedCommand != null)
            {
                this.TextChangedCommand.Execute(e.NewTextValue);
            }
        }        
    }
}
