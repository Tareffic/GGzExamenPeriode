using GGzApplicatie.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using System.Linq;

namespace GGzApplicatie.Helpers
{

    public class DatabaseHelperClass
    {
        public string AdminUsername;

        Admin admininfo = new Admin();
        SQLiteConnection ggzdb = new SQLiteConnection("GGzDB.db");
        public void getFirstAdminUsername()
        {

            using (var statement = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                admininfo = statement.Query<Admin>
                            ("select Username from tbl_Admin").FirstOrDefault();
            }
            AdminUsername = admininfo.Username;
        }
    }
}