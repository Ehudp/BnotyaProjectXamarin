﻿using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Interfaces;
using WomenInBible.Managers;
using WomenInBible.ViewModels;
using Xamarin.Forms.Labs.Data;

namespace WomenInBible.Models
{
    public class Question : ObservableObject, IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value, () => Content); }
        }        

        private ObservableCollection<Answer> _answers;
        [Ignore]
        public ObservableCollection<Answer> Answers
        {
            get { return _answers; }
            set { SetProperty(ref _answers, value, () => Answers); }
        }

        private int _correctAnswerId;
        public int CorrectAnswerId
        {
            get { return _correctAnswerId; }
            set { SetProperty(ref _correctAnswerId, value, () => CorrectAnswerId); }
        }

        public void FillAllProperties<T>(T item)
        {
            var question = item as Question;
            Content = question.Content;            
            CorrectAnswerId = question.CorrectAnswerId;
        }        
    }
}
