using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WomenInBible.Models
{
    public class ToastConfig
    {
        public string Message { get; set; }
        public int TimeoutSeconds { get; set; }
        public Action OnClick { get; set; }

        public ToastConfig()
        {
            TimeoutSeconds = 3;
        }
    }
}
