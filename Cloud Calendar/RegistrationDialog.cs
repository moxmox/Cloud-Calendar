using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloud_Calendar
{
    class RegistrationDialog : UserDialog
    {
        public RegistrationDialog(Form parentWindow) : base(parentWindow)
        {
            Text = "Register";
            MainLabel.Text = "New User";
            CnclButton.Click += new EventHandler(CnclButton_Click);
            ActionButton.Text = "Add User";
            ActionButton.Click += new EventHandler(ActionButton_Click);
        }

        public void ActionButton_Click(object sender, EventArgs args)
        {

        }
    }
}