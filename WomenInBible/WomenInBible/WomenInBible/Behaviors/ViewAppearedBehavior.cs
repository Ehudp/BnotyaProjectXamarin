using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Messages;
using Xamarin.Forms;

namespace WomenInBible.Behaviors
{
    public class ViewAppearedBehavior : Behavior<ContentPage>
    {
        static readonly BindablePropertyKey IsLoadedPropertyKey =
            BindableProperty.CreateReadOnly("IsLoaded", typeof(bool), typeof(ViewAppearedBehavior), false);
        public static readonly BindableProperty IsLoadedProperty = IsLoadedPropertyKey.BindableProperty;

        public bool IsLoaded
        {
            get { return (bool)base.GetValue(IsLoadedProperty); }
            private set { base.SetValue(IsLoadedPropertyKey, value); }
        }

        protected override void OnAttachedTo(ContentPage page)
        {
            page.Appearing += OnPageAppearing;
        }

        void OnPageAppearing(object sender, EventArgs e)
        {
            IsLoaded = true;
            MessagingCenter.Send<ViewAppearedMessage>(new ViewAppearedMessage(), "View appeared"); 
        }

        protected override void OnDetachingFrom(ContentPage page)
        {
            page.Appearing -= OnPageAppearing;
        }
    }
}
