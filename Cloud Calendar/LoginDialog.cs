using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloud_Calendar
{
    class LoginDialog : UserDialog
    {
        public LoginDialog(Form parentWindow) : base(parentWindow)
        {
            Text = "Login";
            MainLabel.Text = "Login";

            CnclButton.Click += new EventHandler(CnclButton_Click);
            ActionButton.Text = "Login";
            ActionButton.Click += new EventHandler(ActionButton_Click);

            ShowConfirmPassword(false);
        }

        public async void ActionButton_Click(object sender, EventArgs args)
        {
            ServerConnectionController controller = ServerConnectionController.GetInstance();
            try
            {
                await controller.Login(UsernameBox.Text, PasswordBox.Text);
            }
            catch (TaskCanceledException tex)
            {
                MessageBox.Show(tex.ToString());
            }
        }
    }
}
