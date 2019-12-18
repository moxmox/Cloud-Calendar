using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloud_Calendar
{
    public partial class Form1 : Form
    {
        TableLayoutPanel tableLayout = new TableLayoutPanel();
        Label userLoginLabel = new Label();

        public Form1()
        {
            InitializeComponent();

            userLoginLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            userLoginLabel.Text = "Click to Login";
            userLoginLabel.Size = new Size(80, 20);
            userLoginLabel.Location = new Point(this.Width-100, 10);

            tableLayout.ColumnCount = 7;
            tableLayout.RowCount = 5; //this may need to be set individually for each month
            tableLayout.Location = new Point(10,60);
            tableLayout.Size = new Size((this.Width - 10), (this.Height - 40));
            tableLayout.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;

            this.Controls.Add(userLoginLabel);
            this.Controls.Add(tableLayout);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tableLayout.SuspendLayout();
            for (int col = 0; col <= tableLayout.ColumnCount-1; col++)
            {
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                for (int row = 0; row <= tableLayout.RowCount-1; row++)
                {
                    tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    Label l = new Label();
                    l.Text = "label:" + row.ToString();
                    tableLayout.Controls.Add(l, col, row);
                }
            }
            tableLayout.ResumeLayout();
        }
    }
}
