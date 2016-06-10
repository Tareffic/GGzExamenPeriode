using GGzApplicatie.Helpers;
using System.Collections.Generic;
using Windows.Graphics.Display;
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
            HomePage.LoadScoreData();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            LoadAllLabelsAndTextboxes();
        }
        public void LoadAllLabelsAndTextboxes()
        {
            if(UserHelper.HasFirstScore == true && UserHelper.HasSecondScore == false)
            {
                lbl_FirstScoreText.Text = "De onderstaande score is de uitslag behaald op " + NewDateScoreHelper.tmpDateOfScore.ToString("dd/MMMM/yyyy") + ".";
                txtb_FirstCount.Text = NewDateScoreHelper.tmpTotalScore.ToString();
            }
            else if (UserHelper.HasFirstScore == true && UserHelper.HasSecondScore == true)
            {
                lbl_FirstScoreText.Text = "De onderstaande score is de uitslag behaald op " + OldDateScoreHelper.tmpDateOfScore.ToString("dd/MMMM/yyyy") + ".";
                lbl_SecondScoreText.Text = "De onderstaande score is de uitslag behaald op " + NewDateScoreHelper.tmpDateOfScore.ToString("dd/MMMM/yyyy") + ".";
                txtb_FirstCount.Text = OldDateScoreHelper.tmpTotalScore.ToString();
                txtb_SecondCount.Text = NewDateScoreHelper.tmpTotalScore.ToString();
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
