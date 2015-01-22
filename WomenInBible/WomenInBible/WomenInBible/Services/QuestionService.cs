using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Managers;
using WomenInBible.Models;
using WomenInBible.ViewModels;

namespace WomenInBible.Services
{
    public class QuestionService
    {
        public async Task<int> CreateQuestion(Question question, List<Answer> answers, int correctAnswerId)
        {
            question.Answers = new ObservableCollection<Answer>(answers);
            question.AnswerIds = answers.Select(x => x.Id).ToArray();
            question.CorrectAnswerId = correctAnswerId;
            return await IoC.Resolve<DatabaseManager>().InsertAsync(question, x => x.Id == question.Id);
        }

        public async Task DeleteQuestion(Question question, bool shouldDeleteAllQuestionsAnswers)
        {
            // before deleting a question
            // check if answers under it should also be deleted
            if (shouldDeleteAllQuestionsAnswers)
            {
                foreach (var answer in question.Answers)
                {
                    await IoC.Resolve<DatabaseManager>().DeleteAsync(answer, x => x.Id == answer.Id);
                }
            }

            await IoC.Resolve<DatabaseManager>().DeleteAsync(question, x => x.Id == question.Id);
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestion(Question question)
        {
            var answers = new List<Answer>();
            foreach (var answerId in question.AnswerIds)
            {
                var answer = await IoC.Resolve<DatabaseManager>().QueryAsync<Answer>(x => x.Id == answerId);
                answers.Add(answer);
            }

            return answers;
        }
    }
}
