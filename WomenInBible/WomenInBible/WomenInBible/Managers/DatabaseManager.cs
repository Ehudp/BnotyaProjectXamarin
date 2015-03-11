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
        public Task InitializationAwaiter { get { return _initilizationTask; } }

        private Func<SQLiteConnectionWithLock> _connectionDelegate; 

        public DatabaseManager()
        {
            _connectionDelegate = DependencyService.Get<ISQLite>().GetConnectionDelegate();
            _initilizationTask = CreateDb();            
        }        

        private async Task CreateDb() // TODO: For tests only
        {
            await CreateDataBase();                      
            
            // Inserting answers in DB
            Answer answer1 = await InsertAsync(new Answer { Content = "Yoda" });
            Answer answer2 = await InsertAsync(new Answer { Content = "Bibi" });
            Answer answer3 = await InsertAsync(new Answer { Content = "Barak" });
            Answer answer4 = await InsertAsync(new Answer { Content = "Obama" });
            
            // Inserting questions in DB
            await IoC.Resolve<QuestionService>()
                .CreateQuestion(new Question { Content = "Who is the President of US?" }, new[] { answer1, answer2 }, answer1.Id);
            await IoC.Resolve<QuestionService>()
                .CreateQuestion(new Question { Content = "Who is the President of Mars?" }, new[] { answer3, answer4 }, answer4.Id);

            // Inserting women, cards and insights in DB
            var womenList = new List<Woman>
            {
                new Woman
                {
                    Name = "T1",
                    Icon = "ic_action_search.png"
                },
                new Woman
                {
                    Name = "T2",
                    Icon = "ic_action_search.png"                    
                }
            };

            for (int i = 1; i <= womenList.Count; i++)
            {
                var card = new Card
                {
                    Front = string.Format("card{0}.png", i),
                    Back = string.Format("card{0}a.png", i),
                    Insight = string.Format("card{0}b.png", i)
                };

                var insight = new Insight 
                { 
                    InsightImage = string.Format("card{0}b.png", i), 
                    IsFavorite = 0, 
                    Name = womenList[i - 1].Name 
                };

                await InsertAsync(card);
                await InsertAsync(insight);
                womenList[i - 1].CardId = card.Id;
                await InsertAsync(womenList[i - 1]);
            }

            // Check all tables
            //await CheckTables();
        }

        private async Task CreateDataBase() // TODO: For tests only
        {
            var connection = new SQLiteAsyncConnection(_connectionDelegate);
            await connection.DropTableAsync<Card>();
            await connection.DropTableAsync<Woman>();
            await connection.DropTableAsync<Insight>();
            await connection.DropTableAsync<Answer>();
            await connection.DropTableAsync<Question>();

            await connection.CreateTableAsync<Card>();
            await connection.CreateTableAsync<Woman>();
            await connection.CreateTableAsync<Insight>();
            await connection.CreateTableAsync<Answer>();
            await connection.CreateTableAsync<Question>();                      
        }

        private async Task CheckTables() // TODO: For tests only
        {
            var answers = await QueryAllAsync<Answer>();
            var questions = await QueryAllAsync<Question>();
            var insights = await QueryAllAsync<Insight>();
            var cards = await QueryAllAsync<Card>();
            var women = await QueryAllAsync<Woman>();

#if DEBUG
            System.Diagnostics.Debugger.Break();
#endif
        }

        public async Task<T> FindAsync<T>(int id) where T : new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            return await database.FindAsync<T>(id);
        }

        public async Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            return await database.FindAsync<T>(predicate);
        }

        public async Task<List<T>> QuerySelectedAsync<T, TValue>(Expression<Func<T, bool>> predicate, Expression<Func<T, TValue>> orderByFunc)
            where T : new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            return await database.Table<T>().Where(predicate).OrderBy<TValue>(orderByFunc).ToListAsync();
        }

        public async Task<List<T>> QuerySelectedAsync<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            return await database.Table<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> QueryAllAsync<T, TValue>(Expression<Func<T, TValue>> orderByFunc) where T : new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            return await database.Table<T>().OrderBy(orderByFunc).ToListAsync();
        }

        public async Task<List<T>> QueryAllAsync<T>() where T : new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            return await database.Table<T>().ToListAsync();
        }

        public async Task<T> InsertAsync<T>(T item) where T : new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            await database.InsertAsync(item);
            return item; 
        }

        public async Task<T> UpdateAsync<T>(T item, Expression<Func<T, bool>> predicate) where T : IModel, new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            T current = await database.FindAsync<T>(predicate);
            if (current != null)
            {
                current.FillAllProperties(item);                
                await database.UpdateAsync(current);                
            }
            else
            {
                current = item;
                await database.InsertAsync(current);  
            }
            return current;           
        }

        public async Task DeleteAsync<T>(int id) where T : new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            T current = await database.FindAsync<T>(id);
            if (current != null)
            {
                await database.DeleteAsync(id);
            }
        }

        public async Task DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            var database = new SQLiteAsyncConnection(_connectionDelegate);
            T current = await database.FindAsync<T>(predicate);
            if (current != null)
            {                
                await database.DeleteAsync(current);
            }
        }        
    }
}
