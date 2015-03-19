using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content.Res;
using Android.Media;
using Xamarin.Forms.Labs.Services.SoundService;
using Android.Content;

[assembly: Xamarin.Forms.Dependency(typeof(WomenInBible.Droid.Implementations.SoundService_Android))]
namespace WomenInBible.Droid.Implementations
{
    public class SoundService_Android : ISoundService
    {
        private bool _isScrubbing;

        private MediaPlayer _player;

        private double _volume;
        public double Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        public double CurrentTime
        {
            get
            {
                return _player == null ? 0 : TimeSpan.FromMilliseconds(_player.CurrentPosition).TotalSeconds;
            }
        }

        public bool IsPlaying
        {
            get
            {
                return _player.IsPlaying;
            }
        }

        public SoundFile CurrentFile { get; private set; }

        public event EventHandler SoundFileFinished;

        private async Task StartPlayerAsyncFromAssetsFolder(AssetFileDescriptor fp)
        {
            try
            {
                if (_player == null)
                    _player = new MediaPlayer();
                else
                    _player.Reset();

                if (fp == null)
                    throw new FileNotFoundException("Make sure you set your file in the Assets folder");

                await _player.SetDataSourceAsync(fp.FileDescriptor);

                _player.Prepared += (s, e) =>
                    {
                        // Set AudioManager volume and request focus
                        var audioManager = (AudioManager)Application.Context.GetSystemService(Context.AudioService);
                        audioManager.SetStreamVolume(Android.Media.Stream.Music, (int)Volume, 0);
                        AudioManager.IOnAudioFocusChangeListener listener = new OnAudioFocusChangeListener(this);
                        var ret = audioManager.RequestAudioFocus(listener, Android.Media.Stream.Music, AudioFocus.Gain);

                        _player.SetVolume((float)Volume, (float)Volume);
                    };

                _player.Prepare();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        public Task<SoundFile> PlayAsync(string filename, string extension = null)
        {
            return Task.Run(async () =>
            {
                if (_player == null || string.Compare(filename, CurrentFile.Filename) > 0)
                {
                    await SetMediaAsync(filename);
                }

                _player.Start();
                return CurrentFile;
            });
        }

        public Task<SoundFile> SetMediaAsync(string filename)
        {
            return Task.Run(async () =>
            {
                CurrentFile = new SoundFile { Filename = filename };
                await StartPlayerAsyncFromAssetsFolder(Application.Context.Assets.OpenFd(filename));
                CurrentFile.Duration = TimeSpan.FromSeconds(_player.Duration);
                return CurrentFile;
            });
        }

        public async Task GoToAsync(double position)
        {
            await Task.Run(() =>
                    {
                        if (_isScrubbing) return;
                        _isScrubbing = true;
                        _player.SeekTo(TimeSpan.FromSeconds(position).Milliseconds);
                        _isScrubbing = false;
                    });
        }

        public void Play()
        {
            if (_player != null && !_player.IsPlaying)
                _player.Start();
        }

        public void Stop()
        {
            if ((_player == null)) return;

            if (_player.IsPlaying)
                _player.Stop();

            _player.Release();
            _player = null;
        }

        public void Pause()
        {
            if (_player != null && _player.IsPlaying)
                _player.Pause();
        }
    }
}