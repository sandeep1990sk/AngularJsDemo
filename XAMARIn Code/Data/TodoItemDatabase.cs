using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace myCIIEmployee
{
    public class TodoItemDatabase
    {
        static object locker = new object();

        SQLiteConnection database;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public TodoItemDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            // create the tables
            database.CreateTable<LogedInUser>();
        }

        public LogedInUser IsUserLogedIn()
        {
            lock (locker)
            {
                return database.Table<LogedInUser>().FirstOrDefault();
            }
        }
        
        public int SaveItem(LogedInUser item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int Logout()
        {
            lock (locker)
            {
                return database.Delete<LogedInUser>(Application.Current.Properties["EmployeeId"]);
            }
        }
    }
}

