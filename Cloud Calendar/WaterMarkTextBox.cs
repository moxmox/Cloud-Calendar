using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Cloud_Calendar
{
    class WaterMarkTextBox : TextBox
    {
        private string Hint;
        public WaterMarkTextBox(string hint)
        {
            Hint = hint;
            Text = Hint;
            ForeColor = Color.Gray;
            GotFocus += WaterMarkTextBox_GotFocus;
            LostFocus += WaterMarkTextBox_LostFocus;
        }

        private void WaterMarkTextBox_GotFocus(object sender, EventArgs e)
        {
            if(Text == Hint)
            {
                Text = "";
                ForeColor = Color.Black;
            }
        }

        private void WaterMarkTextBox_LostFocus(object sender, EventArgs e)
        {
            if(Text == "")
            {
                Text = Hint;
                ForeColor = Color.Gray;
            }
        }
    }
}
