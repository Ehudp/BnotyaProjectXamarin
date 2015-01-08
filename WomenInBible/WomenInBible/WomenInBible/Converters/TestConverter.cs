using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WomenInBible.Converters
{
    public class TestConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DebugBreak();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DebugBreak();
            return value;
        }

        [Conditional("DEBUG")]
        void DebugBreak()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
        }
    }
}
