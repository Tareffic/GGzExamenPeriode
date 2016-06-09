using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGzApplicatie.Model
{
    class Question
    {
        public int QuestionId { get; set; }
        public string Category { get; set; }
        public string QuestionString { get; set; }

        public Question() 
        {
            //empty
        }

        public Question(int questionId, string category, string questionString)
        {
            this.QuestionId = questionId;
            this.Category = category;
            this.QuestionString = questionString;
        }
    }
}
