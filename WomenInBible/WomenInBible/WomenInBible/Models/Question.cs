using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.ViewModels;

namespace WomenInBible.Models
{
    public class Question : ViewModelBase, IModel
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
        public ObservableCollection<Answer> Answers
        {
            get { return _answers; }
            set { SetProperty(ref _answers, value, () => Answers); }
        }

        private int[] _answerIds;
        public int[] AnswerIds
        {
            get { return _answerIds; }
            set { SetProperty(ref _answerIds, value, () => AnswerIds); }
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
            Answers = question.Answers;
            AnswerIds = question.AnswerIds;
            CorrectAnswerId = question.CorrectAnswerId;
        }

        public static async Task<int> CreateQuestion(Question question, List<Answer> answers, int correctAnswerId)
        {
            question.Answers = new ObservableCollection<Answer>(answers);
            question.AnswerIds = answers.Select(x => x.Id).ToArray();
            question.CorrectAnswerId = correctAnswerId;
            return await App.DBManager.InsertAsync(question, x => x.Id == question.Id);
        }

        public static async Task DeleteQuestion(Question question, bool shouldDeleteAllQuestionsAnswers)
        {
            // before deleting a question
            // check if answers under it should also be deleted
            if (shouldDeleteAllQuestionsAnswers)
            {
                foreach (var answer in question.Answers)
                {
                    await App.DBManager.DeleteAsync(answer, x => x.Id == answer.Id);
                }
            }

            await App.DBManager.DeleteAsync(question, x => x.Id == question.Id);
        }

        public static async Task<IEnumerable<Answer>> GetAnswersByQuestion(Question question)
        {
            var answers = new List<Answer>();
            foreach (var answerId in question.AnswerIds)
            {
                var answer = await App.DBManager.QueryAsync<Answer>(x => x.Id == answerId);
                answers.Add(answer);
            }

            return answers;
        }
    }
}
