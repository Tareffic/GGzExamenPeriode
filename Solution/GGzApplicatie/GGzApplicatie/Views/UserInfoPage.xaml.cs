using GGzApplicatie.Helpers;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace GGzApplicatie.Views
{
    /// <summary>
    /// This page represents the userinfo where all the information from tbl_User is displayed.
    /// </summary>
    public sealed partial class UserInfoPage : Page
    {
        public UserInfoPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            LoadUserInfoToLabels();
        }
        /// <summary>
        /// Loads all temporary UserHelper data to defined labels.
        /// </summary>
        public void LoadUserInfoToLabels()
        {
            lbl_Information.Text = "Hieronder bevinden zich de account gegevens van " + UserHelper.tmpName + ".";
            lbl_UsernameLoad.Text = UserHelper.tmpUserName;
            lbl_NameLoad.Text = UserHelper.tmpName;
            lbl_SurnameLoad.Text = UserHelper.tmpSurname;
            lbl_BirthdayLoad.Text = UserHelper.tmpDateOfBirth.ToString("dd/MMMM/yyyy");
            lbl_AdminLoad.Text = UserHelper.tmpAdmin;
        }

        private void btn_BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
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
