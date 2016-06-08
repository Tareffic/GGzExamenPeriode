﻿using GGzApplicatie.Helpers;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GGzApplicatie.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResultPage : Page
    {
        public ResultPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            LoadAllLabelsAndTextboxes();
        }
        public void LoadAllLabelsAndTextboxes()
        {
            if(UserHelper.HasFirstScore == true && UserHelper.HasSecondScore == false)
            {
                lbl_FirstScoreText.Text = "De onderstaande score is de uitslag behaald op " + ScoreHelper.tmpDateOfScore.ToString("dd/MMMM/yyyy") + ".";
                txtb_FirstCount.Text = ScoreHelper.tmpTotalScore.ToString();
            }
            else if (UserHelper.HasFirstScore == true && UserHelper.HasSecondScore == true)
            {
                lbl_FirstScoreText.Text = "De onderstaande score is de uitslag behaald op " + ScoreHelper.tmpDateOfScore.ToString("dd/MMMM/yyyy") + ".";
                lbl_SecondScoreText.Text = "De onderstaande score is de uitslag behaald op " + ScoreHelperPrevious.tmpDateOfScore.ToString("dd/MMMM/yyyy") + ".";
                txtb_FirstCount.Text = ScoreHelper.tmpTotalScore.ToString();
                txtb_SecondCount.Text = ScoreHelperPrevious.tmpTotalScore.ToString();
            }
            else if (UserHelper.HasFirstScore == false)
            {
                lbl_FirstScoreText.Text = "Voltooi een vragenlijst om de scores in te kunnen zien.";
            }

        }

        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btn_Results_Click(object sender, RoutedEventArgs e)
        {

        }
        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
    }
}
