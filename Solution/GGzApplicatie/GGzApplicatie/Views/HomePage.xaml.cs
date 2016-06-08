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
    /// HomePage represents the start-up page of the application, it is used to navigate to register or to login.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            UserLogin(txtb_Username.Text, txtb_Password.Password);
        }

        private void UserLogin(string username, string adminPassword)
        {
            try
            {
                // using Sql connection
                using (SQLite.SQLiteConnection difference = new SQLite.SQLiteConnection("GGzDB.db"))
                {
                    // Create list from database to Model.User.
                    var userlist = difference.Query<Model.User>
                                     ("select Username, Admin from tbl_User").ToList();    
                    // Create list from database to Model.Admin.
                    var adminlist = difference.Query<Model.Admin>
                                     ("select Username, Password from tbl_Admin").ToList();
                    // Compares input with list, returns found username with Admin username
                    var user = userlist.Where(x => x.Username == username).FirstOrDefault().Admin;
                    // Compares input matches with the Admin password 
                    var admin = adminlist.Where(x => x.Username == user && x.Password == adminPassword).FirstOrDefault();

                    // Check if admin not is null
                    if (admin != null)
                    {
                        LoadUserData();
                        LoadScoreData();
                        OpenMenuPage();
                    }
                    if(adminPassword != admin.Password)
                    {
                        LoginFailed();
                    }
                }     
            }
            catch (Exception)
            {
                LoginFailed();
            }
        }
        public void LoadUserData()
        {
            // Using Sql connection
            using (SQLite.SQLiteConnection difference = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                // Create list from database to model
                List<Model.User> selectuserinfo = difference.Query<Model.User>
                                 ("select Name, Surname, Username, DateOfBirth, Admin  from tbl_User").ToList();

                // Select user from list
                Model.User selectUserInfo = selectuserinfo.Where(x => x.Username == txtb_Username.Text).FirstOrDefault();

                // Fill Userhelper strings
                UserHelper.tmpName = selectUserInfo.Name;
                UserHelper.tmpSurname = selectUserInfo.Surname;
                UserHelper.tmpUserName = selectUserInfo.Username;
                UserHelper.tmpDateOfBirth = selectUserInfo.DateOfBirth;
                UserHelper.tmpAdmin = selectUserInfo.Admin;
            } 
        }
        public void LoadScoreData()
        {
            // Using Sql connection
            using (SQLite.SQLiteConnection difference = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                // Create list from database to model
                    List<Model.Score> selectuserinfoSecond = difference.Query<Model.Score>
                     ("select * from tbl_Score where Username = '" + UserHelper.tmpUserName + "' order by date(DateOfScore) Desc limit 2").ToList();
                    //new fill strings
                    try
                    {
                        ScoreHelperPrevious.tmpTotalScore = selectuserinfoSecond[0].TotalScore;
                        ScoreHelperPrevious.tmpVITAScore = selectuserinfoSecond[0].AGGRScore;
                        ScoreHelperPrevious.tmpAGORScore = selectuserinfoSecond[0].AGORScore;
                        ScoreHelperPrevious.tmpANXIScore = selectuserinfoSecond[0].ANXIScore;
                        ScoreHelperPrevious.tmpCOGNScore = selectuserinfoSecond[0].COGNScore;
                        ScoreHelperPrevious.tmpDateOfScore = selectuserinfoSecond[0].DateOfScore;
                        ScoreHelperPrevious.tmpMoodScore = selectuserinfoSecond[0].MOODScore;
                        ScoreHelperPrevious.tmpSOMAScore = selectuserinfoSecond[0].SOMAScore;
                        ScoreHelperPrevious.tmpSOPHScore = selectuserinfoSecond[0].SOPHScore;
                        ScoreHelperPrevious.tmpWORKScore = selectuserinfoSecond[0].WORKScore;
                        ScoreHelperPrevious.tmpVITAScore = selectuserinfoSecond[0].VITAScore;
                        UserHelper.HasFirstScore = true;
                    }
                    catch
                    {
                        UserHelper.HasFirstScore = false;
                    }
                    try
                    {
                        //old fill strings
                        ScoreHelper.tmpTotalScore = selectuserinfoSecond[1].TotalScore;
                        ScoreHelper.tmpVITAScore = selectuserinfoSecond[1].AGGRScore;
                        ScoreHelper.tmpAGORScore = selectuserinfoSecond[1].AGORScore;
                        ScoreHelper.tmpANXIScore = selectuserinfoSecond[1].ANXIScore;
                        ScoreHelper.tmpCOGNScore = selectuserinfoSecond[1].COGNScore;
                        ScoreHelper.tmpDateOfScore = selectuserinfoSecond[1].DateOfScore;
                        ScoreHelper.tmpMoodScore = selectuserinfoSecond[1].MOODScore;
                        ScoreHelper.tmpSOMAScore = selectuserinfoSecond[1].SOMAScore;
                        ScoreHelper.tmpSOPHScore = selectuserinfoSecond[1].SOPHScore;
                        ScoreHelper.tmpWORKScore = selectuserinfoSecond[1].WORKScore;
                        ScoreHelper.tmpVITAScore = selectuserinfoSecond[1].VITAScore;
                        UserHelper.HasSecondScore = true;
                    }
                    catch
                    {
                        UserHelper.HasSecondScore = false;
                    }
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
                Frame.Navigate(typeof(RegisterPage));
        }
        public void OpenMenuPage()
        {
                Frame.Navigate(typeof(UserMenuPage));
        }
        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            OpenRegisterPage();
        }
    }
}
