using GGzApplicatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGzApplicatie.Helpers
{
    class QuestionHelper
    {
        List<Question> allQuestions = new List<Question>();
        public void loadAllQuestions()
        {
            Question question = new Question(1, "soas", "Welke soa heb ik?"); // manier 1
            allQuestions.Add(question);
            //in code ja maar als hij compiled maakt niets uit dan vind ik 1 wel overzichtelijke voor mezelf ;p dan zet ik wel summaray bij question.cs maar waarom haal je ze niet uit de db?
            // waar is je databasehandler? oke dus de vorige had ik zo gedaan
            Question vraag2 = new Question(); // manier 2
            vraag2.QuestionId = 2;
            vraag2.QuestionString = "";
            vraag2.Category = "Rare kleuren poep";
            allQuestions.Add(vraag2);
        }

    }

}
