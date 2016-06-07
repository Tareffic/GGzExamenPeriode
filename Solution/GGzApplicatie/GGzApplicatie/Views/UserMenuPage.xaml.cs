using GGzApplicatie.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    /// UserMenuPage represents a page that shows after being logged in.
    /// </summary>
    public sealed partial class UserMenuPage : Page
    {

        public UserMenuPage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            LoadWelcome(UserHelper.tmpName, UserHelper.tmpUserName);       
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        /// <param name="tmpName">tmpName from UserHelper.cs.
        /// This parameter is typically used to display the name of the user to an label</param>
        /// <param name="tmpName">tmpUsername from UserHelper.cs
        /// This parameter is typically used to display the username of the user to an label</param>
        public void LoadWelcome(string tmpName, string tmpUsername)
        {
            lbl_Welcome.Text = "Welkom " + tmpName + ". U bent ingelogd als " + tmpUsername + "."; 
        }
        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_MyInformation_Click(object sender, RoutedEventArgs e)
        {
            OpenAbout();
        }

        private void btn_MyResults_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public void OpenAbout()
        {
            Frame.Navigate(typeof(UserInfoPage));
        }
        private void btn_StartQuestions_Click(object sender, RoutedEventArgs e)
        {

        }
        //When back button handled go back one frame.
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
