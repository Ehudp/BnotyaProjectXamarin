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
            question.Answers = new ObservableCollection<Answer>();
            question.CorrectAnswerId = correctAnswerId;
            await IoC.Resolve<DatabaseManager>().InsertAsync(question);

            foreach (var answer in answers)
            {
                answer.QuestionId = question.Id;                
                await IoC.Resolve<DatabaseManager>().UpdateAsync(answer, x => x.Id == answer.Id);
                question.Answers.Add(answer);
            }            

            return question;
        }

        public async Task DeleteQuestion(Question question, bool shouldDeleteAllQuestionsAnswers)
        {
            // before deleting a question
            // check if answers under it should also be deleted
            if (shouldDeleteAllQuestionsAnswers)
            {
                foreach (var answer in question.Answers)
                {
                    await IoC.Resolve<DatabaseManager>().DeleteAsync<Answer>(x => x.Id == answer.Id);
                }
            }

            await IoC.Resolve<DatabaseManager>().DeleteAsync<Question>(x => x.Id == question.Id);
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestion(Question question)
        {
            return await IoC.Resolve<DatabaseManager>().QuerySelectedAsync<Answer, int>(x => x.QuestionId == question.Id, x => x.Id);            
        }
    }
}
