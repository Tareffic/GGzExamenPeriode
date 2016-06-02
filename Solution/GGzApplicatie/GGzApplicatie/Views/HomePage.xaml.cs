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
using Windows.UI.Popups;
using Windows.Phone.UI.Input;  


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GGzApplicatie.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        Constants constants;
        Model.User userinfo;
        DatabaseHelperClass dbHelper;
        public HomePage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            dbHelper = new DatabaseHelperClass();
            userinfo = new Model.User();
            constants = new Constants();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            UserLogin(txtb_Password.Password, txtb_Username.Text);
        }

        private async void UserLogin(string admin, string username)
        {

            using (var difference = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                userinfo = difference.Query<Model.User>
                            ("select Admin, Username from tbl_User").FirstOrDefault();
            }
            if(admin == userinfo.Admin && username == userinfo.Username)
            {
                MessageDialog msgbox = new MessageDialog("Inloggen gelukt"); //testdialogue
                await msgbox.ShowAsync();
                constants.isLogged = true;
                OpenTestPage(); //placeholder replace with Menu its added
            }
            else
            {
                MessageDialog msgbox = new MessageDialog("Inloggen failed"); //testdialogue
                await msgbox.ShowAsync();
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
        public void OpenRegisterPage()
        {
            if (constants.isLogged == false)
            {
                Frame.Navigate(typeof(Register));
            }
        }
        public void OpenTestPage()
        {
            if (constants.isLogged == false)
            {
                Frame.Navigate(typeof(Test));
            }
        }
        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            OpenRegisterPage();
        }
    }
}
