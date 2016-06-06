using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGzApplicatie.Model
{
    public class Score
    {
        public byte Scores { get; set; }
        public byte TotalScore { get; set; }
        public DateTime DateOfScore { get; set; }
        public byte AGGRScore { get; set; }
        public byte AGORScore { get; set; }
        public byte ANXIScore { get; set; }
        public byte COGNScore { get; set; }
        public byte MOODScore { get; set; }
        public byte SOMAScore { get; set; }
        public byte SOPHScore { get; set; }
        public byte VITAScore { get; set; }
        public byte WORKScore { get; set; }
        public Score()
        {
            //Constructor
        }
    }
}
