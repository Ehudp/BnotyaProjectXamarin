using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WomenInBible.Managers;
using WomenInBible.Models;
using WomenInBible.Services;
using Xamarin.Forms;

namespace WomenInBible.ViewModels
{
    public class TriviaViewModel : ViewModelBase
    {
        Random _random = new Random();

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, () => Title); }
        }

        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set { SetProperty(ref _currentQuestion, value, () => CurrentQuestion); }
        }        

        private Answer _selectedAnswer;
        public Answer SelectedAnswer
        {
            get { return _selectedAnswer; }
            set { SetProperty(ref _selectedAnswer, value, () => SelectedAnswer); }
        }

        private ICommand _answerSelectedCommand;
        public ICommand AnswerSelectedCommand
        {
            get
            {
                return _answerSelectedCommand ?? (_answerSelectedCommand = new Command(
                  async () =>
                {
                    if (SelectedAnswer.Id == CurrentQuestion.CorrectAnswerId)
                    {
                        // TODO: Victory
                    }
                    else
                    {
                        // TODO: Error
                    }
                }, () => true));
            }
        }


        public TriviaViewModel()
        {
            Title = "Trivia";
            Task.Run(async () =>
                {
                    var list = await IoC.Resolve<DatabaseManager>().QueryAllAsync<Question, int>((question) => question.Id);                    
                    var randomId = _random.Next(0, list.Count());
                    CurrentQuestion = list[randomId];                   

                }).ConfigureAwait(false);
        }
    }
}
