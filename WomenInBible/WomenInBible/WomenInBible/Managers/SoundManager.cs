using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Services.SoundService;

namespace WomenInBible.Managers
{
    public class SoundManager
    {
        public static ISoundService SoundService
        {
            get
            {
                return DependencyService.Get<ISoundService>();
            }
        }
    }
}
