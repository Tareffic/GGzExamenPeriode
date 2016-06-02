using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GGzApplicatie
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        Model.Admin adminInfo;
        public Register()
        {
            this.InitializeComponent();
            adminInfo = new Model.Admin();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            LoadComboBox();      
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                //add "Weet u zeker dat u de registratie wilt afbreken?"
                //Ja
                Frame.GoBack();
                //Nee
            }
        }

        private void btn_RegisterAccount_Click(object sender, RoutedEventArgs e)
        {
            //add here from textboxes to database save logic ricky


            //end command
            //Frame.GoBack();
        }
    }
}
