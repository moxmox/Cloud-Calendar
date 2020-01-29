using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Cloud_Calendar
{
    class UserDialog : Form //UserDialog to server as base class for LoginDialog and RegistrationDialog
    {
        private WaterMarkTextBox UsernameBox;
        private PasswordBox PasswordBox;
        private PasswordBox CnfrmPassBox;
        private Button ActionButton;
        private Button CancelButton;

        private Size TextBoxSize = new Size(150, 20);
        public UserDialog() 
        {
            Width = 400;
            Height = 250;
            UsernameBox = new WaterMarkTextBox("Enter Username");
            UsernameBox.Size = TextBoxSize;
            UsernameBox.Location = new Point(Width/2, Height/4);
            PasswordBox = new PasswordBox();
            PasswordBox.Size = TextBoxSize;
            PasswordBox.Location = new Point(Width/2, Height/4 + 40);
            CnfrmPassBox = new PasswordBox();
            CnfrmPassBox.Size = TextBoxSize;
            CnfrmPassBox.Location = new Point(Width/2, Height/4 + 80);
            CnfrmPassBox.Visible = false;

            Controls.Add(UsernameBox);
            Controls.Add(PasswordBox);
            Controls.Add(CnfrmPassBox);
        }

        public void ShowConfirmPassword(bool visible)
        {
            CnfrmPassBox.Visible = visible;
        }
    }

    class PasswordBox : TextBox //seeing if this works with inheriting from WatermarkTextbox
    {
        public PasswordBox()
        {
            PasswordChar = '*';
            MaxLength = 14;
        }
    }
}
