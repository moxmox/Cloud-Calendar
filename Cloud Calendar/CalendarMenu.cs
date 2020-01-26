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
        const string LOGIN_NAME = "LOGIN_NAME";
        const string LOGOUT_NAME = "LOGOUT_NAME";
        const string EXIT_NAME = "EXIT_NAME";
        MenuItem UserItem;
        MenuItem AboutItem;
        public CalendarMenu()
        {
            UserItem = MenuItems.Add("&User");
            AboutItem = MenuItems.Add("&About");
            MenuItem loginItem = new MenuItem("&Login");
            loginItem.Name = LOGIN_NAME;
            loginItem.Click += new EventHandler(UserItem_SubItemClick);
            loginItem.Shortcut = Shortcut.CtrlL;
            MenuItem logoutItem = new MenuItem("&Logout");
            logoutItem.Name = LOGOUT_NAME;
            logoutItem.Click += new EventHandler(UserItem_SubItemClick);
            logoutItem.Shortcut = Shortcut.CtrlC;
            logoutItem.Enabled = false;
            MenuItem exitItem = new MenuItem("&Exit");
            exitItem.Name = EXIT_NAME;
            exitItem.Click += UserItem_SubItemClick;
            exitItem.Shortcut = Shortcut.CtrlX;
            UserItem.MenuItems.Add(loginItem);
            UserItem.MenuItems.Add(logoutItem);
            UserItem.MenuItems.Add(exitItem);
            AboutItem.Click += new EventHandler(AboutItem_Click);
        }

        private void UserItem_SubItemClick(object sender, EventArgs args)
        {
            string senderName = ((MenuItem)sender).Name;
            switch(senderName)
            {
                case LOGIN_NAME:
                    MessageBox.Show(LOGIN_NAME);
                    break;
                case LOGOUT_NAME:
                    MessageBox.Show(LOGOUT_NAME);
                    break;
                case EXIT_NAME:
                    GetForm().Close();
                    break;
            }
        }

        private void AboutItem_Click(object sender, EventArgs args)
        {
            MessageBox.Show("About button clicked");
        }
    }
}
