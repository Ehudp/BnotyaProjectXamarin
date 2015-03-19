using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WomenInBible.Models
{
    public class AlertConfig
    {
        public string OkText { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Action OnOk { get; set; }

        public AlertConfig()
        {
            OkText = "OK";
        }
    }
}
