using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace Cloud_Calendar
{
    class DoubleBufferLayoutPanel : TableLayoutPanel
    {
        public DoubleBufferLayoutPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer|
                ControlStyles.UserPaint, true);
        }

        public DoubleBufferLayoutPanel(IContainer container)
        {
            container.Add(this);
            SetStyle(ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.UserPaint, true);
        }
    }
}
