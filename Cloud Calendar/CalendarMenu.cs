using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloud_Calendar
{
    class CalendarMenu : MainMenu
    {
        MenuItem UserItem;
        MenuItem AboutItem;
        public CalendarMenu()
        {
            UserItem = MenuItems.Add("&User");
            AboutItem = MenuItems.Add("&About");
            MenuItem loginItem = new MenuItem("&Login");
            loginItem.Click += new EventHandler(LoginItem_Click);
            loginItem.Shortcut = Shortcut.CtrlL;
            MenuItem logoutItem = new MenuItem("&Logout");
            logoutItem.Click += new EventHandler(LogoutItem_Click);
            logoutItem.Shortcut = Shortcut.CtrlC;
            logoutItem.Enabled = false;
            MenuItem registerItem = new MenuItem("&New User");
            registerItem.Click += new EventHandler(RegisterItem_Click);
            registerItem.Shortcut = Shortcut.CtrlR;
            MenuItem exitItem = new MenuItem("&Exit");
            exitItem.Click += new EventHandler(ExitItem_Click);
            exitItem.Shortcut = Shortcut.CtrlX;
            UserItem.MenuItems.Add(loginItem);
            UserItem.MenuItems.Add(logoutItem);
            UserItem.MenuItems.Add(registerItem);
            UserItem.MenuItems.Add(exitItem);
            AboutItem.Click += new EventHandler(AboutItem_Click);
        }

        private void RegisterItem_Click(object sender, EventArgs args)
        {
            //TODO: implement http client to make connection to node API for registration
            RegistrationDialog dialog = new RegistrationDialog(GetForm());
            dialog.Show();
        }

        private void LoginItem_Click(object sender, EventArgs args)
        {
            //TODO: implement http client to make connection to node API for login
            LoginDialog dialog = new LoginDialog(GetForm());
            dialog.Show();
        }

        private void LogoutItem_Click(object sender, EventArgs args)
        {
            //TODO: drop username and password; clear form
        }

        private void ExitItem_Click(object sender, EventArgs args) => GetForm().Close();

        //TODO: create about window to give application information
        private void AboutItem_Click(object sender, EventArgs args) 
        {
            MessageBox.Show("About button clicked");
        }
    }
}
