using GGzApplicatie.Helpers;
using GGzApplicatie.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
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
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            btn_AnswerF.Visibility = Visibility.Collapsed;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
           
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

                        
                        if (QuestionSessionId == 9 || QuestionSessionId == 15 || QuestionSessionId == 20 || QuestionSessionId == 30 || QuestionSessionId == 35)
                        {
                            btn_AnswerF.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            btn_AnswerF.Visibility = Visibility.Collapsed;
                        }
                        QuestionSessionId++;
                        DoneWithQuestions = false;
                    }
                    else
                    {
                        txtb_LoadQuestion.Text = "Alle vragen zijn beantwoord, klik op inleveren om door te gaan.";
                        btn_AnswerA.Visibility = Visibility.Collapsed;
                        btn_AnswerB.Visibility = Visibility.Collapsed;
                        btn_AnswerC.Visibility = Visibility.Collapsed;
                        btn_AnswerD.Visibility = Visibility.Collapsed;
                        btn_AnswerE.Visibility = Visibility.Collapsed;
                        btn_AnswerF.Visibility = Visibility.Collapsed;
                        btn_NextQuestion.Content = "Inleveren";
                        DoneWithQuestions = true;
                    }
                }
                catch
                {

                }
            }

        }


        private async void btn_NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (btn_AnswerA.IsChecked == false && btn_AnswerB.IsChecked == false &&
              btn_AnswerC.IsChecked == false && btn_AnswerD.IsChecked == false && btn_AnswerE.IsChecked == false &&
              btn_AnswerF.IsChecked == false)
            {
                MessageDialog msgbox = new MessageDialog("Kies een optie om door te gaan.");
                await msgbox.ShowAsync();
            }
            else if (DoneWithQuestions == false && btn_AnswerA.IsChecked == true || btn_AnswerB.IsChecked == true ||
                btn_AnswerC.IsChecked == true || btn_AnswerD.IsChecked == true || btn_AnswerE.IsChecked == true ||
                btn_AnswerF.IsChecked == true)
            {
                if(btn_AnswerA.IsChecked == true)
                {
                    btn_Score = 0;
                    CalculateCategoryScore();
                    btn_AnswerA.IsChecked = false;
                }
                if (btn_AnswerB.IsChecked == true)
                {
                    btn_Score = 1;
                    CalculateCategoryScore();
                    btn_AnswerB.IsChecked = false;
                }
                if (btn_AnswerC.IsChecked == true)
                {
                    btn_Score = 2;
                    CalculateCategoryScore();
                    btn_AnswerC.IsChecked = false;
                }
                if (btn_AnswerD.IsChecked == true)
                {
                    btn_Score = 3;
                    CalculateCategoryScore();
                    btn_AnswerD.IsChecked = false;
                }
                if (btn_AnswerE.IsChecked == true)
                {
                    btn_Score = 4;
                    CalculateCategoryScore();
                    btn_AnswerE.IsChecked = false;
                }
                if (btn_AnswerF.IsChecked == true)
                {
                    btn_Score = 0;
                    CalculateCategoryScore();
                    btn_AnswerF.IsChecked = false;
                }
                GetNextQuestion();
            }

            if (DoneWithQuestions == true)
            {
                //save scores to db , dont save WORKscore to total!!
                scores.TotalScore = (scores.AGGRScore + scores.AGORScore + scores.ANXIScore + scores.COGNScore + scores.MOODScore + scores.SOMAScore + scores.SOPHScore + scores.VITAScore);
                 //fix last id +=1
                int newid = NewDateScoreHelper.tmpId += 1;
                InsertScores(newid, UserHelper.tmpUserName, scores.TotalScore, DateTime.Now, scores.AGORScore, scores.AGORScore, scores.ANXIScore, scores.COGNScore, scores.MOODScore, scores.SOMAScore, scores.SOPHScore, scores.VITAScore, scores.WORKScore);
                Frame.GoBack();
            }

        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
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
        }


        public void CalculateCategoryScore()
        {

            switch (currentCategory)
            {
                case "AGGRScore":
                    scores.AGGRScore += btn_Score;
                    break;
                case "AGORScore":
                    scores.AGORScore += btn_Score;
                    break;
                case "ANXIScore":
                    scores.ANXIScore += btn_Score;
                    break;
                case "COGNScore":
                    scores.COGNScore += btn_Score;
                    break;
                case "MOODScore":
                    scores.MOODScore += btn_Score;
                    break;
                case "SOMAScore":
                    scores.SOMAScore += btn_Score;
                    break;
                case "SOPHScore":
                    scores.SOPHScore += btn_Score;
                    break;
                case "VITAScore":
                    scores.VITAScore += btn_Score;
                    break;
                case "WORKScore":
                    scores.WORKScore += btn_Score;
                    break;

            }
        }
    }
}
   

