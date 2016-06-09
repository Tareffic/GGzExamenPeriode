using GGzApplicatie.Helpers;
using GGzApplicatie.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GGzApplicatie.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuestionPage : Page
    {
        Score scores = new Score();
        private int QuestionSessionId = 1;
        private int Max_Questions = 49;
        private int btn_Score;
        private string currentCategory;
        private string MaxQuestions = "48";
        private bool DoneWithQuestions = false;

        public QuestionPage()
        {
            this.InitializeComponent();
            btn_AnswerF.Visibility = Visibility.Collapsed;
            GetNextQuestion();

        }

        public void GetNextQuestion()
        {
            using (SQLite.SQLiteConnection difference = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                try
                {
                    if (QuestionSessionId != Max_Questions)
                    {
                        // Create list from database to model
                        List<Model.Question> selectuserinfo = difference.Query<Model.Question>
                                         ("select * from tbl_Question where Id = '" + QuestionSessionId + "' ").ToList();
                        // loads the question with same QuestionId from QuestionSessionId.
                        //  Model.Question s = selectuserinfo.Select(x => x.QuestionId == QuestionSessionId).FirstOrDefault();

                        txtb_LoadQuestion.Text = selectuserinfo[0].QuestionString;
                        currentCategory = selectuserinfo[0].Category;
                        txtb_QuestionProgress.Text = "Vraag " + QuestionSessionId.ToString() + "/" + MaxQuestions + ".";

                        DoneWithQuestions = false;
                        if (QuestionSessionId == 9 || QuestionSessionId == 15 || QuestionSessionId == 20 || QuestionSessionId == 30 || QuestionSessionId == 35)
                        {
                            btn_AnswerF.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            btn_AnswerF.Visibility = Visibility.Collapsed;
                        }
                        QuestionSessionId++;
                    }
                    else
                    {
                        DoneWithQuestions = true;
                        txtb_LoadQuestion.Text = "Alle vragen zijn beantwoord, klik op inleveren om door te gaan.";
                        btn_AnswerA.Visibility = Visibility.Collapsed;
                        btn_AnswerB.Visibility = Visibility.Collapsed;
                        btn_AnswerC.Visibility = Visibility.Collapsed;
                        btn_AnswerD.Visibility = Visibility.Collapsed;
                        btn_AnswerE.Visibility = Visibility.Collapsed;
                        btn_AnswerF.Visibility = Visibility.Collapsed;
                        btn_NextQuestion.Content = "Inleveren";
                    }
                }
                catch
                {

                }
            }

        }

        private void textBlock2_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btn_NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (DoneWithQuestions == false)
            {
                GetNextQuestion();
            }
            else if (DoneWithQuestions == true)
            {
                //save scores to db , dont save WORKscore to total!!
                scores.TotalScore = scores.AGGRScore + scores.AGORScore + scores.ANXIScore + scores.COGNScore + scores.MOODScore + scores.SOMAScore + scores.SOPHScore + scores.VITAScore;

                 //fix last id +=1
                //int newid = NewDateScoreHelper.tmpId += 1;
                //InsertScores(newid, UserHelper.tmpUserName, scores.TotalScore, DateTime.Now, scores.AGORScore, scores.AGORScore, scores.ANXIScore, scores.COGNScore, scores.MOODScore, scores.SOMAScore, scores.SOPHScore, scores.VITAScore, scores.WORKScore);
            }

        }
        public static void InsertScores(int Id, string Username, int TotalScore, DateTime DateOfScore, int AGGRScore, int AGORScore, int ANXIScore, int COGNScore, int MOODscore, int SOMAScore, int SOPHScore, int VITAScore, int WORKScore)
        {

            using (var connection = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                var statement = connection.CreateCommand(@"INSERT INTO tbl_Score(
                                                        Id, Username, TotalScore, 
                                                        DateOfScore, AGGRScore, AGORScore, 
                                                        ANXIScore, COGNScore, MOODScore, 
                                                        SOMAScore, SOPHScore, VITAScore, WORKScore) 
                                                        VALUES(? , ? , ? , ? , ?, ?, ?, ?, ?, ?, ?, ?, ?);",
                                                        Id, Username, TotalScore, 
                                                        DateOfScore, AGGRScore, AGORScore, 
                                                        ANXIScore, COGNScore, MOODscore, 
                                                        SOMAScore, SOPHScore, VITAScore, WORKScore);
                statement.ExecuteNonQuery();
            }
        }
        
        private void Answer_Checked(object sender, RoutedEventArgs e)
        {

            RadioButton radioBtn = (RadioButton)sender;
            if (radioBtn.IsChecked == true)
            {
                switch (radioBtn.Name)
                {
                    case "btn_AnswerA":
                        btn_Score = 0;


                        //do something
                        break;

                    case "btn_AnswerB":
                        btn_Score = 1;
                        //do something
                        break;

                    case "btn_AnswerC":
                        btn_Score = 2;
                        //do something
                        break;

                    case "btn_AnswerD":
                        btn_Score = 3;
                        //do something
                        break;

                    case "btn_AnswerE":
                        btn_Score = 4;
                        //do something
                        break;

                    case "btn_AnswerF":
                        btn_Score = 0;
                        //do something
                        break;
                }
            }
        }
    }
}

