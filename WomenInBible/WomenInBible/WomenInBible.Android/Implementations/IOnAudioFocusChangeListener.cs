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

namespace WomenInBible.Droid.Implementations
{
    class OnAudioFocusChangeListener : Android.Media.AudioManager.IOnAudioFocusChangeListener
    {
        private SoundService_Android _soundService;

        public OnAudioFocusChangeListener(SoundService_Android soundService)
        {
            _soundService = soundService;
        }

        public void OnAudioFocusChange(Android.Media.AudioFocus focusChange)
        {
            
        }

        public IntPtr Handle { get; private set; }

        public void Dispose()
        {
            
        }
    }
}