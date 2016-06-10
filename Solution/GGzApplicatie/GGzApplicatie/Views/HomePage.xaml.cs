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
using Windows.Graphics.Display;  


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
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
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
        public static void LoadScoreData()
        {
            // Using Sql connection
            using (SQLite.SQLiteConnection difference = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                // Create list from database to model
                    List<Model.Score> selectuserinfoSecond = difference.Query<Model.Score>
                     ("select * from tbl_Score where Username = '" + UserHelper.tmpUserName + "' order by date(DateOfScore) Desc limit 2").ToList();
                    try
                    {
                        List<Model.Score> selectId = difference.Query<Model.Score>
                         ("select * from tbl_Score order by Id Desc limit 1").ToList();
                    

                        // Fill new sorted on date strings
                        NewDateScoreHelper.tmpId = selectId[0].Id;
                        NewDateScoreHelper.tmpAGGRScore = selectuserinfoSecond[0].AGGRScore;
                        NewDateScoreHelper.tmpTotalScore = selectuserinfoSecond[0].TotalScore;
                        NewDateScoreHelper.tmpVITAScore = selectuserinfoSecond[0].VITAScore;
                        NewDateScoreHelper.tmpAGORScore = selectuserinfoSecond[0].AGORScore;
                        NewDateScoreHelper.tmpANXIScore = selectuserinfoSecond[0].ANXIScore;
                        NewDateScoreHelper.tmpCOGNScore = selectuserinfoSecond[0].COGNScore;
                        NewDateScoreHelper.tmpDateOfScore = selectuserinfoSecond[0].DateOfScore;
                        NewDateScoreHelper.tmpMoodScore = selectuserinfoSecond[0].MOODScore;
                        NewDateScoreHelper.tmpSOMAScore = selectuserinfoSecond[0].SOMAScore;
                        NewDateScoreHelper.tmpSOPHScore = selectuserinfoSecond[0].SOPHScore;
                        NewDateScoreHelper.tmpWORKScore = selectuserinfoSecond[0].WORKScore;
                        UserHelper.HasFirstScore = true;
                    }
                    catch
                    {
                        // Boolean to help ResultPage
                        UserHelper.HasFirstScore = false;
                    }
                    try
                    {
                        // fill Older sorted on date strings
                        OldDateScoreHelper.tmpTotalScore = selectuserinfoSecond[1].TotalScore;
                        OldDateScoreHelper.tmpAGGRScore = selectuserinfoSecond[1].AGGRScore;
                        OldDateScoreHelper.tmpAGORScore = selectuserinfoSecond[1].AGORScore;
                        OldDateScoreHelper.tmpANXIScore = selectuserinfoSecond[1].ANXIScore;
                        OldDateScoreHelper.tmpCOGNScore = selectuserinfoSecond[1].COGNScore;
                        OldDateScoreHelper.tmpDateOfScore = selectuserinfoSecond[1].DateOfScore;
                        OldDateScoreHelper.tmpMoodScore = selectuserinfoSecond[1].MOODScore;
                        OldDateScoreHelper.tmpSOMAScore = selectuserinfoSecond[1].SOMAScore;
                        OldDateScoreHelper.tmpSOPHScore = selectuserinfoSecond[1].SOPHScore;
                        OldDateScoreHelper.tmpWORKScore = selectuserinfoSecond[1].WORKScore;
                        OldDateScoreHelper.tmpVITAScore = selectuserinfoSecond[1].VITAScore;
                        UserHelper.HasSecondScore = true;
                    }
                    catch
                    {
                        // Boolean to help ResultPage
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
