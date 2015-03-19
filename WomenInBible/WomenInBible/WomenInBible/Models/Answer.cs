using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Interfaces;
using WomenInBible.ViewModels;
using Xamarin.Forms.Labs.Data;

namespace WomenInBible.Models
{
    public class Answer : ObservableObject, IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value, () => Content); }
        }

        [Indexed]
        public int QuestionId { get; set; }

        public void FillAllProperties<T>(T item)
        {
            var answer = item as Answer;
            Content = answer.Content;
            QuestionId = answer.QuestionId;
        }
    }
}
