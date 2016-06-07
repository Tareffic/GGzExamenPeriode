using GGzApplicatie.Helpers;
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
    public sealed partial class UserInfoPage : Page
    {
        public UserInfoPage()
        {
            this.InitializeComponent();
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
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
