using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;

namespace WomenInBible.Models
{
    public class Answer : ViewModelBase, IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value, () => Content); }
        }

        public void FillAllProperties<T>(T item)
        {
            var answer = item as Answer;
            Content = answer.Content;
        }
    }
}
