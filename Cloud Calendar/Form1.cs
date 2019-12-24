using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Cloud_Calendar
{
    public partial class Form1 : Form
    {
        TableLayoutPanel TableLayout = new DoubleBufferLayoutPanel();
        DateController Controller = DateController.GetInstance();
        Label MonthLabel = new Label();
        Button LeftButton = new Button();
        Button RightButton = new Button();
        List<Label> CellLabels = new List<Label>();

        public Form1()
        {
            InitializeComponent();

            this.Text = "Cloud Calendar";
            this.MinimumSize = new Size(700, 700);

            MonthLabel.Location = new Point((this.Width / 2) - (MonthLabel.Width/2), 580);
            MonthLabel.Size = new Size(150, 100);
            MonthLabel.Anchor = AnchorStyles.Bottom;
            MonthLabel.Text = Controller.GetStringMonth();

            TableLayout.Padding = new Padding(40, 30, 40, 40);
            TableLayout.ColumnCount = 7;
            TableLayout.RowCount = 6;
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

        private void LoadMonth ()
        {
            int DaysInMonth = DateTime.DaysInMonth(Controller.Focused.Year, Controller.Focused.Month);
            DateTime FirstOfTheMonth = new DateTime(Controller.Focused.Year, Controller.Focused.Month, 1);
            int InitialOffset;
            if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Sunday) { InitialOffset = 0; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Monday) { InitialOffset = 1; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Tuesday) { InitialOffset = 2; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Wednesday) { InitialOffset = 3; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Thursday) { InitialOffset = 4; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Friday) { InitialOffset = 5; }
            else { InitialOffset = 6; }
            TableLayout.SuspendLayout();
            foreach (Label temp in CellLabels)
            {
                temp.Text = "";
            }
            for (int i = InitialOffset, j = 0; i < DaysInMonth + InitialOffset; i++, j++)
            {
                CellLabels[i].Text = (j + 1).ToString();
            }
            MonthLabel.Text = Controller.GetStringMonth();
            TableLayout.ResumeLayout();
        }

        private void LeftRightButton_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Name.Equals(RightButton.Name))
            {
                Controller.AddMonth();
                LoadMonth();
            }
            else if (((Button)sender).Name.Equals(LeftButton.Name))
            {
                Controller.SubtractMonth();
                LoadMonth();
            }
        }

        private void CellLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A Cell has Been Clicked");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int DaysInMonth = DateTime.DaysInMonth(Controller.Focused.Year, Controller.Focused.Month);
            DateTime FirstOfTheMonth = new DateTime(Controller.Focused.Year, Controller.Focused.Month, 1);
            int InitialOffset;
            if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Sunday) { InitialOffset = 0; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Monday) { InitialOffset = 1; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Tuesday) { InitialOffset = 2; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Wednesday) { InitialOffset = 3; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Thursday) { InitialOffset = 4; }
            else if (FirstOfTheMonth.DayOfWeek == DayOfWeek.Friday) { InitialOffset = 5; }
            else { InitialOffset = 6; }
            TableLayout.SuspendLayout();
            for (int row = 0; row <= TableLayout.RowCount - 1; row++)
            {
                TableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                for (int col = 0; col <= TableLayout.ColumnCount - 1; col++)
                {
                    int CellNumber = col + row;
                    TableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                    Label CellLabel = new Label();
                    CellLabel.TextAlign = ContentAlignment.MiddleCenter;
                    CellLabel.Dock = DockStyle.Fill;
                    CellLabels.Add(CellLabel);
                    CellLabel.Click += new EventHandler(CellLabel_Click);
                    TableLayout.Controls.Add(CellLabel, col, row);
                }
            }
            for (int i = InitialOffset, j = 0; i < DaysInMonth + InitialOffset; i++, j++)
            {
                CellLabels[i].Text = (j + 1).ToString();
            }
            TableLayout.ResumeLayout();
        }


    }
}
