using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinAndroid;
using WomenInBible.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(WomenInBible.Droid.Implementations.SQLite_Android))]
namespace WomenInBible.Droid.Implementations
{
    public class SQLite_Android : ISQLite
    {
        public Func<SQLiteConnectionWithLock> GetConnectionDelegate()
        {
            var sqliteFilename = "BnotyaSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection           
            return new Func<SQLiteConnectionWithLock>(
                () => new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(path, storeDateTimeAsTicks: false)));            
        }
    }
}