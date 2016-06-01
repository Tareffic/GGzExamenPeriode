using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGzApplicatie.Helpers.Score
{
    class DataHandler
    {
        Model.Score scoreinfo = new Model.Score();
        public void WriteAllScores()
        {

        }
        public void GetPreviousScore(int previousScore)
        {
            using (var difference = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                scoreinfo = difference.Query<Model.Score>
                            ("select TotalScore from tbl_Score").FirstOrDefault();
            }
            previousScore = scoreinfo.TotalScore;
        }
    }
}
