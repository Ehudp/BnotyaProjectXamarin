using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WomenInBible.Models
{
    public class ActionSheetOption
    {
        public string Text { get; set; }
        public Action Action { get; set; }

        public ActionSheetOption(string text, Action action = null)
        {
            this.Text = text;
            this.Action = (action ?? (() => { }));
        }
    }
}
