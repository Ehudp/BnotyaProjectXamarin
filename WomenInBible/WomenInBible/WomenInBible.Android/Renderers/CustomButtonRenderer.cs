using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using WomenInBible.CustomViews;

[assembly: ExportRendererAttribute(typeof(CustomButton), typeof(WomenInBible.Droid.Renderers.CustomButtonRenderer))]
namespace WomenInBible.Droid.Renderers
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        public CustomButtonRenderer()
            : base()
        {
            SetWillNotDraw(false);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var drawable = Context.Resources.GetDrawable(Resource.Drawable.btn_image);
                Control.SetBackgroundDrawable(drawable);
                Control.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 18);
                Control.SetTypeface(null, global::Android.Graphics.TypefaceStyle.Bold);
                var color = Context.Resources.GetColor(Resource.Drawable.btn_text_color);
                Control.SetTextColor(color);                
                Control.SetPadding(30, 10, 30, 10);
                var shadowColor = Context.Resources.GetColor(Resource.Color.btn_shadow);
                Control.SetShadowLayer(2, 1, -1, shadowColor);
                Control.SetLayerType(LayerType.Software, null);
            }
        }
    }
}