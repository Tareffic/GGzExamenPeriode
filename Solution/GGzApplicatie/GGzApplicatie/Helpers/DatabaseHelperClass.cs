using GGzApplicatie.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace GGzApplicatie.Helpers
{

    public class DatabaseHelperClass
    {

        Model.Admin adminData = new Model.Admin();
        Model.Score scoreData = new Model.Score();
        Model.User userData = new Model.User();

        public Model.Admin AdminData(string query)
        {
            using (var statement = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                adminData = statement.Query<Model.Admin>
                            (query).FirstOrDefault();
            }
            return adminData; 
        }
        public Model.User UserData(string query)
        {
            using (var statement = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                userData = statement.Query<Model.User>
                            (query).FirstOrDefault();
            }
            return userData;
        }
        public Model.Score ScoreData(string query)
        {
            using (var statement = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                scoreData = statement.Query<Model.Score>
                            (query).FirstOrDefault();
            }
            return scoreData;
        }
    }
}