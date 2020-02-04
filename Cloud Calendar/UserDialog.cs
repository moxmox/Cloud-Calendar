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
        protected Label MainLabel;
        protected TextBox UsernameBox;
        protected PasswordBox PasswordBox;
        protected PasswordBox CnfrmPassBox;
        protected Label UsernameLabel;
        protected Label PasswordLabel;
        protected Label CnfrmPasswordLabel;
        protected Button ActionButton;
        protected Button CnclButton;

        private Form ParentWindow;

        private readonly Size GENERAL_CONTROL_SIZE = new Size(150, 20);
        private readonly Size BUTTON_SIZE = new Size(100, 20);
        public UserDialog(Form parentWindow) 
        {
            ParentWindow = parentWindow;
            ParentWindow.Enabled = false;

            Width = 350;
            Height = 250;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            Text = "Change Me";
            MainLabel = new Label();
            MainLabel.Text = "Change Me";
            MainLabel.Font = new Font("Arial", 18, FontStyle.Bold);
            MainLabel.ForeColor = Color.CornflowerBlue;
            MainLabel.Size = new Size(180, 30);
            MainLabel.Location = new Point(Width/3, 20);
            UsernameBox = new TextBox();
            UsernameBox.Size = GENERAL_CONTROL_SIZE;
            UsernameBox.Location = new Point(Width/3, Height/4);
            PasswordBox = new PasswordBox();
            PasswordBox.Size = GENERAL_CONTROL_SIZE;
            PasswordBox.Location = new Point(Width/3, Height/4 + 40);
            CnfrmPassBox = new PasswordBox();
            CnfrmPassBox.Size = GENERAL_CONTROL_SIZE;
            CnfrmPassBox.Location = new Point(Width/3, Height/4 + 80);
            CnfrmPassBox.Visible = false;
            UsernameLabel = new Label();
            UsernameLabel.Text = "Username: ";
            UsernameLabel.Size = GENERAL_CONTROL_SIZE;
            UsernameLabel.Location = new Point(Width/6, Height/4);
            PasswordLabel = new Label();
            PasswordLabel.Text = "Password:";
            PasswordLabel.Size = GENERAL_CONTROL_SIZE;
            PasswordLabel.Location = new Point(Width/6, Height/4 + 40);
            CnfrmPasswordLabel = new Label();
            CnfrmPasswordLabel.Text = "Confirm Pass:";
            CnfrmPasswordLabel.Size = GENERAL_CONTROL_SIZE;
            CnfrmPasswordLabel.Location = new Point(Width/8, Height/4+80);
            CnfrmPasswordLabel.Visible = false;
            CnclButton = new Button();
            CnclButton.Text = "Cancel";
            CnclButton.Size = BUTTON_SIZE;
            CnclButton.Location = new Point(110, Height-70); //Height was 180
            CnclButton.Click += new EventHandler(CnclButton_Click);
            ActionButton = new Button();
            ActionButton.Text = "Action";
            ActionButton.Size = BUTTON_SIZE;
            ActionButton.Location = new Point(GENERAL_CONTROL_SIZE.Width + 70, Height-70);

            Controls.Add(MainLabel);
            Controls.Add(UsernameBox);
            Controls.Add(PasswordBox);
            Controls.Add(CnfrmPassBox);
            Controls.Add(UsernameLabel);
            Controls.Add(PasswordLabel);
            Controls.Add(CnfrmPasswordLabel);
            Controls.Add(CnclButton);
            Controls.Add(ActionButton);

            ShowConfirmPassword(true);
            FormClosed += new FormClosedEventHandler(UserDialog_Close);
        }

        public void ShowConfirmPassword(bool visible)
        {
            CnfrmPassBox.Visible = visible;
            CnfrmPasswordLabel.Visible = visible;
        }

        public void UserDialog_Close(object sender, EventArgs args)
        {
            ParentWindow.Enabled = true;
        }

        public void CnclButton_Click(object sender, EventArgs args)
        {
            Close();
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
