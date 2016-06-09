using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGzApplicatie.Model
{
    public class Score
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Scores { get; set; }
        public int TotalScore { get; set; }
        public DateTime DateOfScore { get; set; }
        public int AGGRScore { get; set; }
        public int AGORScore { get; set; }
        public int ANXIScore { get; set; }
        public int COGNScore { get; set; }
        public int MOODScore { get; set; }
        public int SOMAScore { get; set; }
        public int SOPHScore { get; set; }
        public int VITAScore { get; set; }
        public int WORKScore { get; set; }
        public Score()
        {
            //Constructor
        }

    }
}
