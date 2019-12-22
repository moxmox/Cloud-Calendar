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
        TableLayoutPanel TableLayout = new DoubleBufferLayoutPanel();
        Label MonthLabel = new Label();
        Button LeftButton = new Button();
        Button RightButton = new Button();

        public Form1()
        {
            InitializeComponent();

            this.Text = "Cloud Calendar";
            this.MinimumSize = new Size(700, 700);

            String DateNTime = "It is Currently " + DateTime.Now.ToString("MMMM dd");

            MonthLabel.Location = new Point((this.Width / 2) - (MonthLabel.Width / 2), 580);
            MonthLabel.Size = new Size(150, 100);
            MonthLabel.Anchor = AnchorStyles.Bottom;
            MonthLabel.Text = DateNTime;

            TableLayout.Padding = new Padding(40, 30, 40, 40);
            TableLayout.ColumnCount = 7;
            TableLayout.RowCount = 5;
            TableLayout.Location = new Point(0, 10);
            TableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            TableLayout.Size = new Size((this.Width), (this.Height-100));
            TableLayout.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;

            RightButton.Text = "Right";
            RightButton.Name = "RightButton";
            RightButton.Size = new Size(80, 20);
            RightButton.Location = new Point(this.Width-110, 600);
            RightButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            RightButton.Click += new EventHandler(LeftRightButton_Click);
            LeftButton.Text = "Left";
            LeftButton.Name = "LeftButton";
            LeftButton.Size = new Size(80, 20);
            LeftButton.Location = new Point(10, 600);
            LeftButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LeftButton.Click += new EventHandler(LeftRightButton_Click);

            this.Controls.Add(MonthLabel);
            this.Controls.Add(TableLayout);
            this.Controls.Add(RightButton);
            this.Controls.Add(LeftButton);
        }

        private void LeftRightButton_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Name.Equals(RightButton.Name))
            {
                MessageBox.Show("Right");
            }
            else if (((Button)sender).Name.Equals(LeftButton.Name))
            {
                MessageBox.Show("Left");
            }
        }

        private void CellLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A Cell has Been Clicked");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TableLayout.SuspendLayout();
            for (int col = 0; col <= TableLayout.ColumnCount-1; col++)
            {
                TableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                for (int row = 0; row <= TableLayout.RowCount-1; row++)
                {
                    TableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    Label CellLabel = new Label();
                    CellLabel.Text = "label:" + row.ToString();
                    CellLabel.TextAlign = ContentAlignment.MiddleCenter;
                    CellLabel.Dock = DockStyle.Fill;
                    CellLabel.Click += new EventHandler(CellLabel_Click);
                    TableLayout.Controls.Add(CellLabel, col, row);
                }
            }
            TableLayout.ResumeLayout();
        }
    }
}
