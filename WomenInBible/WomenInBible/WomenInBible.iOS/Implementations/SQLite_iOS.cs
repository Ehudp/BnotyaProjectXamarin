using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WomenInBible.Interfaces;
using SQLite.Net.Platform.XamarinIOS;

[assembly: Xamarin.Forms.Dependency(typeof(WomenInBible.iOS.SQLite_iOS))]
namespace WomenInBible.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public Func<SQLiteConnectionWithLock> GetConnectionDelegate()
        {
            var sqliteFilename = "BnotyaSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection           
            return new Func<SQLiteConnectionWithLock>(
                () => new SQLiteConnectionWithLock(new SQLitePlatformIOS(), new SQLiteConnectionString(path, storeDateTimeAsTicks: false)));            
        }
    }
}
