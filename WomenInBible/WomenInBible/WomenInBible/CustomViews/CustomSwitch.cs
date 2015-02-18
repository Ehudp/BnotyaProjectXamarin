using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WomenInBible.CustomViews
{
    public class CustomSwitch : Switch
    {
        public static BindableProperty ToggledCommandProperty =
            BindableProperty.Create<CustomSwitch, ICommand>(x => x.ToggledCommand, null);

        public ICommand ToggledCommand
        {
            get { return (ICommand)this.GetValue(ToggledCommandProperty); }
            set { this.SetValue(ToggledCommandProperty, value); }
        }

        public CustomSwitch()
        {
            this.Toggled += this.OnToggled;
        }

        private void OnToggled(object sender, ToggledEventArgs e)
        {
            if (this.ToggledCommand != null)
            {
                this.ToggledCommand.Execute(e.Value);                
            }
        }         
    }
}
