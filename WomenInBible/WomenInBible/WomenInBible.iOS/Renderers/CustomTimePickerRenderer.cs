#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif
using System;
using System.Collections.Generic;
using System.Text;
using WomenInBible.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TimePicker), typeof(CustomTimePickerRenderer))]
namespace WomenInBible.iOS.Renderers
{
    public class CustomTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            var timePicker = (UIDatePicker)Control.InputView;
            timePicker.Locale = new NSLocale("no_nb");
        }
    }
}
