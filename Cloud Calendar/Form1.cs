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
        TableLayoutPanel tableLayout = new DoubleBufferLayoutPanel();
        Label monthLabel = new Label();

        public Form1()
        {
            InitializeComponent();

            this.Text = "Cloud Calendar";
            this.MinimumSize = new Size(700, 700);

            monthLabel.Location = new Point((this.Width / 2) - (monthLabel.Width / 2), 580);
            monthLabel.Size = new Size(60, 100);
            monthLabel.Anchor = AnchorStyles.Bottom;
            monthLabel.Text = "January";

            tableLayout.Padding = new Padding(40, 30, 40, 40);
            tableLayout.ColumnCount = 7;
            tableLayout.RowCount = 5;
            tableLayout.Location = new Point(0, 10);
            tableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayout.Size = new Size((this.Width), (this.Height-100));
            tableLayout.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;

            Button rightButton = new Button();
            rightButton.Text = "Right";
            rightButton.Size = new Size(80, 20);
            rightButton.Location = new Point(this.Width-110, 600);
            rightButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button leftButton = new Button();
            leftButton.Text = "Left";
            leftButton.Size = new Size(80, 20);
            leftButton.Location = new Point(10, 600);
            leftButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            this.Controls.Add(monthLabel);
            this.Controls.Add(tableLayout);
            this.Controls.Add(rightButton);
            this.Controls.Add(leftButton);
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
                    Label temp_lable = new Label();
                    temp_lable.Text = "label:" + row.ToString();
                    temp_lable.TextAlign = ContentAlignment.MiddleCenter;
                    temp_lable.Dock = DockStyle.Fill;
                    tableLayout.Controls.Add(temp_lable, col, row);
                }
            }
            tableLayout.ResumeLayout();
        }
    }
}
