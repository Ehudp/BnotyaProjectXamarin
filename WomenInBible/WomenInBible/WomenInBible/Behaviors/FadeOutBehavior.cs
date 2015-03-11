using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Managers;
using WomenInBible.Messages;
using Xamarin.Forms;

namespace WomenInBible.Behaviors
{
    public class FadeOutBehavior : Behavior<VisualElement>
    {
        VisualElement _element;

        public static readonly BindableProperty StartFaddingProperty = BindableProperty.
            Create<FadeOutBehavior, bool>(p => p.StartFadding, default(bool), BindingMode.Default, null,
            StartFaddingChanged);

        public bool StartFadding
        {
            get { return (bool)GetValue(StartFaddingProperty); }
            set { SetValue(StartFaddingProperty, value); }
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create("MaxLength", typeof(int), typeof(FadeOutBehavior), 0);

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        private static async void StartFaddingChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            var behavior = bindable as FadeOutBehavior;
            if (newValue)
            {
                await behavior._element.FadeTo(0.2, 1, Easing.Linear);
                while (true)
                {
                    await behavior._element.FadeTo(1, (uint)behavior.MaxLength, Easing.Linear);
                    await behavior._element.FadeTo(0.2, (uint)behavior.MaxLength, Easing.Linear);                    
                }
            }
        }

        protected override void OnAttachedTo(VisualElement element)
        {
            _element = element;
        }

        protected override void OnDetachingFrom(VisualElement element)
        {

        }
    }
}
