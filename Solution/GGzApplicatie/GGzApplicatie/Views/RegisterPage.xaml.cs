using GGzApplicatie.Common;
using SQLite;
using System;
using System.Linq;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GGzApplicatie
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        Model.Admin adminInfo;
        Model.User userinfo;

        public RegisterPage()
        {
            this.InitializeComponent();
            adminInfo = new Model.Admin();
            userinfo = new Model.User();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            LoadComboBox();
        }

        public void LoadComboBox()
        {
            using (var difference = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                adminInfo = difference.Query<Model.Admin>
                            ("select Username from tbl_Admin").FirstOrDefault();
                cmb_AdminUsernames.Items.Add(adminInfo.Username);
            }
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void btn_RegisterAccount_Click(object sender, RoutedEventArgs e)
        {

            using (var difference = new SQLite.SQLiteConnection("GGzDB.db"))
            {
                var userlist = difference.Query<Model.User>
                                     ("select Username from tbl_User").ToList();
                if (txtb_Username.Text != string.Empty)
                {
                    var user = userlist.Where(x => x.Username == txtb_Username.Text).FirstOrDefault();

                    if (user != null)
                    {

                        if (txtb_Username.Text == user.Username)
                        {

                            MessageDialog msgbox = new MessageDialog("De opgegeven gebruikersnaam is al in gebruik.");
                            await msgbox.ShowAsync();
                        }
                    }
                    else if (txtb_Name.Text != string.Empty && txtb_Surname.Text != string.Empty && txtb_Username.Text != string.Empty)
                    {
                        insertData(txtb_Name.Text, txtb_Surname.Text, txtb_Username.Text, dtp_Birthday.Date.DateTime, cmb_AdminUsernames.SelectedItem.ToString());
                    }
                }
            }
            Frame.GoBack();
        }

        public async static void insertData(string Name, string Surname, string Username, DateTime DateOfBirth, string Admin)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection("GGzDB.db"))
                {
                    var statement = connection.CreateCommand(@"INSERT INTO tbl_User (Name, Surname, Username, DateOfBirth, Admin) VALUES(?, ? , ? , ? , ?);", Name, Surname, Username, DateOfBirth, Admin);
                    statement.ExecuteNonQuery();
                }
                MessageDialog msgbox = new MessageDialog("Meld aan met uw gebruikersnaam en laat het wachtwoord invullen door de behandelaar.");
                await msgbox.ShowAsync();    
            }
            catch (Exception)
            {
            }
        }
        public async void DatabaseInsertFailed()
        {
            MessageDialog msgbox = new MessageDialog("Er is een fout opgetreden bij het registeren. Neem contact op met de systeembeheerder.");
            await msgbox.ShowAsync();
        }
    }
}
