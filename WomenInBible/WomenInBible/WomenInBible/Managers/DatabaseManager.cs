using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WomenInBible.Models;
using WomenInBible.Services;
using WomenInBible.ViewModels;
using Xamarin.Forms;

namespace WomenInBible.Managers
{
    public class DatabaseManager
    {
        private Task _initilizationTask;

        public DatabaseManager()
        {
            _initilizationTask = CreateDb();
        }

        public Task InitializationAwaiter { get { return _initilizationTask; } }

        public async Task CreateDb() // TODO: For tests only
        {
            await CreateDataBase();
            Question question1 = new Question { Content = "Who is the President of US?" };
            Question question2 = new Question { Content = "Who is the President of Mars?" };
            Answer answer1 = new Answer { Content = "Yoda" };
            Answer answer2 = new Answer { Content = "Bibi" };
            // Inserting answers in DB
            answer1.Id = await InsertAsync<Answer>(answer1, x => x.Id == answer1.Id);
            answer2.Id = await InsertAsync<Answer>(answer2, x => x.Id == answer2.Id);
            // Inserting questions in DB
            await IoC.Resolve<QuestionService>().CreateQuestion(question1, new List<Answer> { answer1, answer2 }, answer1.Id);
            await IoC.Resolve<QuestionService>().CreateQuestion(question2, new List<Answer> { answer1, answer2 }, answer2.Id);
        }

        public async Task CreateDataBase()
        {
            var database = new SQLiteAsyncConnection(DependencyService.Get<ISQLite>().GetConnectionDelegate());
            await database.CreateTableAsync<Card>();
            await database.CreateTableAsync<Woman>();            
            await database.CreateTableAsync<Insight>();
            await database.CreateTableAsync<Answer>();
            await database.CreateTableAsync<Question>();
        }

        public async Task<T> QueryAsync<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            var database = new SQLiteAsyncConnection(DependencyService.Get<ISQLite>().GetConnectionDelegate());
            return await database.Table<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<List<T>> QuerySelectedAsync<T, TValue>(Expression<Func<T, bool>> predicate, Expression<Func<T, TValue>> orderByFunc)
            where T : new()
        {
            var database = new SQLiteAsyncConnection(DependencyService.Get<ISQLite>().GetConnectionDelegate());
            return await database.Table<T>().Where(predicate).OrderBy<TValue>(orderByFunc).ToListAsync();
        }

        public async Task<List<T>> QueryAllAsync<T, TValue>(Expression<Func<T, TValue>> orderByFunc) where T : new()
        {
            var database = new SQLiteAsyncConnection(DependencyService.Get<ISQLite>().GetConnectionDelegate());
            return await database.Table<T>().OrderBy(orderByFunc).ToListAsync();
        }

        public async Task<int> InsertAsync<T>(T item, Expression<Func<T, bool>> predicate) where T : new()
        {
            var database = new SQLiteAsyncConnection(DependencyService.Get<ISQLite>().GetConnectionDelegate());
            return await database.InsertAsync(item);
        }

        public async Task<int> UpdateAsync<T>(T item, Expression<Func<T, bool>> predicate) where T : IModel, new()
        {
            T current = await QueryAsync(predicate);
            if (current != null)
            {
                current.FillAllProperties(item);
                var database = new SQLiteAsyncConnection(DependencyService.Get<ISQLite>().GetConnectionDelegate());
                return await database.UpdateAsync(current);
            }
            return 0;
        }

        public async Task DeleteAsync<T>(T item, Expression<Func<T, bool>> predicate) where T : new()
        {
            T current = await QueryAsync(predicate);
            if (current != null)
            {
                var database = new SQLiteAsyncConnection(DependencyService.Get<ISQLite>().GetConnectionDelegate());
                await database.DeleteAsync(current);
            }
        }
    }
}
