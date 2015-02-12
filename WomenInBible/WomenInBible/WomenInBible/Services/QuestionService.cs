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
        public async Task<Question> CreateQuestion(Question question, Answer[] answers, int correctAnswerId)
        {
            var dbManager = IoC.Resolve<DatabaseManager>();

            question.Answers = new ObservableCollection<Answer>();
            question.CorrectAnswerId = correctAnswerId;
            await dbManager.InsertAsync(question);

            foreach (var answer in answers)
            {
                answer.QuestionId = question.Id;
                await dbManager.UpdateAsync(answer, x => x.Id == answer.Id);
                question.Answers.Add(answer);
            }

            return question;
        }

        public async Task DeleteQuestion(Question question, bool shouldDeleteAllQuestionsAnswers)
        {
            var dbManager = IoC.Resolve<DatabaseManager>();

            // before deleting a question
            // check if answers under it should also be deleted
            if (shouldDeleteAllQuestionsAnswers)
            {
                var answers = await dbManager.QuerySelectedAsync<Answer>(x => x.QuestionId == question.Id);
                Parallel.ForEach(answers, async answer => await dbManager.DeleteAsync<Answer>(answer.Id));

                //foreach (var answer in answers)
                //{
                //    await dbManager.DeleteAsync<Answer>(answer.Id);
                //}
            }

            await dbManager.DeleteAsync<Question>(question.Id);
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestion(Question question)
        {
            return await IoC.Resolve<DatabaseManager>().QuerySelectedAsync<Answer, int>(x => x.QuestionId == question.Id, x => x.Id);
        }
    }
}
