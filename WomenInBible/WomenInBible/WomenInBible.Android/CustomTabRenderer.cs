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
using System.ComponentModel;
using Android.Graphics.Drawables;
using Xamarin.Forms;

//[assembly: ExportRenderer(typeof(TabbedPage), typeof(WomenInBible.Droid.CustomTabRenderer))]
namespace WomenInBible.Droid
{
     public class CustomTabRenderer : TabbedRenderer
        {
            private Activity activity;
            private const string COLOR = "#f7f8f8";
       //This flag is used in the case when the app is not completely closed, and the user return back.
        private bool isFirstDesign = true;

         private readonly CustomGestureListener _listener;
        private readonly GestureDetector _detector;

        TabbedPage _page;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //activity = this.Context as Activity;
            _page = (TabbedPage)sender;

            
        }        

        public CustomTabRenderer()
        {
            _listener = new CustomGestureListener ();
            _detector = new GestureDetector (_listener);

        }

        //protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
        //{
        //    base.OnElementChanged (e);

        //    if (e.NewElement == null) {
        //        if (this.GenericMotion != null) {
        //            this.GenericMotion -= HandleGenericMotion;
        //        }
        //        if (this.Touch != null) {
        //            this.Touch -= HandleTouch;
        //        }
        //    }

        //    if (e.OldElement == null) {
        //        this.GenericMotion += HandleGenericMotion;
        //        this.Touch += HandleTouch;
        //    }
        //}

        void HandleTouch (object sender, TouchEventArgs e)
        {
            _detector.OnTouchEvent (e.Event);
        }

        void HandleGenericMotion (object sender, GenericMotionEventArgs e)
        {
            _detector.OnTouchEvent (e.Event);
        }

        //protected override void OnHandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnHandlePropertyChanged(sender, e);
        //    _page = (TabbedPage)sender;
        //    activity = this.Context as Activity;
        //}         

        protected override void OnWindowVisibilityChanged(ViewStates visibility)
        {
            base.OnWindowVisibilityChanged(visibility);
            if (isFirstDesign)
            {
                _page.PagesChanged += _page_PagesChanged;
                _page.CurrentPageChanged += _page_CurrentPageChanged;                

                this.Touch += HandleTouch;
                this.GenericMotion += HandleGenericMotion;
                //ActionBar actionBar = activity.ActionBar;

                //ColorDrawable colorDrawable = new ColorDrawable(Android.Graphics.Color.ParseColor(COLOR));
                //actionBar.SetStackedBackgroundDrawable(colorDrawable);
                //ActionBarTabsSetup(actionBar);

                isFirstDesign = false;
            }
        }

        void _page_CurrentPageChanged(object sender, EventArgs e)
        {
            
        }

        void _page_PagesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }         

        //private void ActionBarTabsSetup(ActionBar actionBar)
        //{
        //    Android.App.ActionBar.Tab keypad = actionBar.GetTabAt(0);
        //    if (TabIsEmpty(keypad))
        //        TabSetup(keypad, Resource.Drawable.card1);

        //    Android.App.ActionBar.Tab contacts = actionBar.GetTabAt(1);
        //    if (TabIsEmpty(contacts))
        //        TabSetup(contacts, Resource.Drawable.card1a);

        //    Android.App.ActionBar.Tab favorites = actionBar.GetTabAt(2);
        //    if (TabIsEmpty(favorites))
        //        TabSetup(favorites, Resource.Drawable.card1b);

        //    Android.App.ActionBar.Tab callsLog = actionBar.GetTabAt(3);
        //    if (TabIsEmpty(callsLog))
        //        TabSetup(callsLog, Resource.Drawable.card1);

        //    Android.App.ActionBar.Tab chat = actionBar.GetTabAt(4);
        //    if (TabIsEmpty(chat))
        //        TabSetup(chat, Resource.Drawable.card1);
        //}

        //private bool TabIsEmpty(ActionBar.Tab tab)
        //{
        //    if (tab != null)
        //        if (tab.CustomView == null)
        //            return true;
        //    return false;
        //}

        //private void TabSetup(ActionBar.Tab tab, int resourceID)
        //{
        //    ImageView iv = new ImageView(activity);
        //    iv.SetImageResource(resourceID);
        //    iv.SetPadding(-35, 8, -35, 16);

        //    tab.SetCustomView(iv);
        //}
    }
}