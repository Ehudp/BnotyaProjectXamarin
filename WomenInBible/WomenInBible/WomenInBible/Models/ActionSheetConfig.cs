using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WomenInBible.Models
{
    public class ActionSheetConfig
    {
        public string Title { get; set; }        
        public IList<ActionSheetOption> Options { get; set; }


        public ActionSheetConfig()
        {
            this.Options = new List<ActionSheetOption>();
        }

        public ActionSheetConfig Add(string text, Action action = null)
        {
            this.Options.Add(new ActionSheetOption(text, action));
            return this;
        }
    }
}
