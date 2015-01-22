using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WomenInBible.ViewModels
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            this.Appearing += BaseContentPage_Appearing;
            this.Disappearing += BaseContentPage_Disappearing;
        }

        void BaseContentPage_Disappearing(object sender, EventArgs e)
        {
            ((ViewModelBase)BindingContext).OnClosed();
        }

        void BaseContentPage_Appearing(object sender, EventArgs e)
        {
            ((ViewModelBase)BindingContext).OnLoaded();
        }
    }
}
