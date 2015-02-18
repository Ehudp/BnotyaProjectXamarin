using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WomenInBible.CustomViews
{
    public class CustomEntry : Entry
    {
        public static BindableProperty TextChangedCommandProperty =
            BindableProperty.Create<CustomEntry, ICommand>(x => x.TextChangedCommand, null);

        public ICommand TextChangedCommand
        {
            get { return (ICommand)this.GetValue(TextChangedCommandProperty); }
            set { this.SetValue(TextChangedCommandProperty, value); }
        }

        public CustomEntry()
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
