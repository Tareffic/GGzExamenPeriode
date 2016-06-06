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
        Model.Admin admininfo;
        DatabaseHelperClass dbHelper;
        public HomePage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            dbHelper = new DatabaseHelperClass();
            userinfo = new Model.User();
            admininfo = new Model.Admin();
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

        private void UserLogin(string adminPassword, string username)
        {
            try
            {
                using (var difference = new SQLite.SQLiteConnection("GGzDB.db"))
                {
                    var userlist = difference.Query<Model.User>
                                     ("select Username, Admin from tbl_User").ToList();       
                    var adminlist = difference.Query<Model.Admin>
                                     ("select Username, Password from tbl_Admin").ToList();

                    var user = userlist.Where(x => x.Username == username).FirstOrDefault().Admin;
                    var admin = adminlist.Where(x => x.Username == user && x.Password == adminPassword).FirstOrDefault();

                    if (admin != null)
                    {
                        constants.isLogged = true;
                        OpenMenuPage();
                    }
                }     
            }
            catch (Exception)
            {
                LoginFailed();
            }
        }
        public async void LoginFailed()
        {
            MessageDialog msgbox = new MessageDialog("Gebruikersnaam of wachtwoord is onjuist.");
            await msgbox.ShowAsync();
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
                Frame.Navigate(typeof(RegisterPage));
            }
        }
        public void OpenMenuPage()
        {
            if (constants.isLogged == true)
            {
                Frame.Navigate(typeof(MenuPage));
            }
        }
        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            OpenRegisterPage();
        }
    }
}
