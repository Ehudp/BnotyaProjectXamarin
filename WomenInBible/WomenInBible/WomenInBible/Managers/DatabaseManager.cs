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
using Xamarin.Forms;

namespace WomenInBible.Managers
{
    public class DatabaseManager
    {
        private SQLiteAsyncConnection _database;

        public DatabaseManager()
        {
            _database = new SQLiteAsyncConnection(DependencyService.Get<ISQLite>().GetConnectionDelegate());
            _database.CreateTableAsync<Woman>();
            _database.CreateTableAsync<Card>();
            _database.CreateTableAsync<Insight>();
            _database.CreateTableAsync<Answer>();
            _database.CreateTableAsync<Question>();
        }

        public async Task<T> QueryAsync<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            return await _database.Table<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> QuerySelectedAsync<T, TValue>(Expression<Func<T, bool>> predicate, Expression<Func<T, TValue>> func)
            where T : new()            
        {
            return await _database.Table<T>().Where(predicate).OrderBy<TValue>(func).ToListAsync();            
        } 

        public async Task<IEnumerable<T>> QueryAllAsync<T, TValue>(Expression<Func<T, TValue>> func) where T : new()
        {
            return await _database.Table<T>().OrderBy(func).ToListAsync();
        }

        public async Task<int> InsertAsync<T>(T item, Expression<Func<T, bool>> func) where T : new()
        {
            T current = await QueryAsync(func);
            if (current == null)
                return await _database.InsertAsync(item);
            return 0;
        }

        public async Task<int> UpdateAsync<T>(T item, Expression<Func<T, bool>> func) where T : IModel, new()
        {
            T current = await QueryAsync(func);
            if (current != null)
            {
                current.FillAllProperties(item);
                return await _database.UpdateAsync(current);
            }
            return 0;
        }

        public async Task DeleteAsync<T>(T item, Expression<Func<T, bool>> func) where T : new()
        {
            T current = await QueryAsync(func);
            if (current != null)            
                await _database.DeleteAsync(current);            
        }        
    }
}
