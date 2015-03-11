using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WomenInBible.Behaviors
{
    public class ButtonClickTriggerAction : TriggerAction<Button>
    {
        protected async override void Invoke(Button button)
        {
            var color = button.BackgroundColor;
            button.BackgroundColor = Color.Aqua;
            await button.ScaleTo(0.9, 100, Easing.Linear);
            button.BackgroundColor = color;            
            button.ScaleTo(1, 100, Easing.Linear);
        }
    }
}
