using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Cloud_Calendar
{
    public partial class MainWindow : Form
    {
        TableLayoutPanel TableLayout = new DoubleBufferLayoutPanel();
        DateController Controller = DateController.GetInstance();
        Label MonthLabel = new Label();
        Button LeftButton = new Button();
        Button RightButton = new Button();
        List<Label> CellLabels = new List<Label>();
        List<DayEntry> Days = new List<DayEntry>();
        MySqlConnection connection;

        public MainWindow()
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
            TableLayout.Size = new Size((Width), (Height-100));
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

            Controls.Add(MonthLabel);
            Controls.Add(TableLayout);
            Controls.Add(RightButton);
            Controls.Add(LeftButton);

            DatabaseConnectionController dbController = DatabaseConnectionController.GetInstance();
            connection = dbController.Connection;
            Days = dbController.LoadEntries();
        }

        private void LoadMonth ()
        {
            Controller.SelectedDay = DateController.NONE_SELECTED;
            ResetColors();
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
            DatabaseConnectionController dbController = DatabaseConnectionController.GetInstance();
            ResetColors();
            if (((Button)sender).Name.Equals(RightButton.Name))
            {
                Controller.AddMonth();
                Days = dbController.LoadEntries();
                LoadMonth();
                ColorAppointmentCells();
            }
            else if (((Button)sender).Name.Equals(LeftButton.Name))
            {
                Controller.SubtractMonth();
                Days = dbController.LoadEntries();
                LoadMonth();
                ColorAppointmentCells();
            }
        }

        private void CellLabel_Click(object sender, EventArgs e)
        {
            int selectedDay;
            if (int.TryParse((sender as Label).Text, out selectedDay))
            {
                Controller.SelectedDay = selectedDay;
                var focused = Controller.Focused;
                Controller.Focused = new DateTime(focused.Year, focused.Month, selectedDay);
                CellDialog dialog = new CellDialog(this);
                dialog.Show();
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
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
            ColorAppointmentCells();
        }

        private void ColorAppointmentCells()
        {
            foreach (DayEntry day in Days)
            {
                if (day.HasAppointments())
                {
                    foreach (Label label in CellLabels)
                    {
                        int text_day;
                        bool has_text = int.TryParse(label.Text, out text_day);
                        if (has_text)
                        {
                            int apt_day = day.DateInfo.Day;
                            if (text_day == apt_day)
                            {
                                label.BackColor = Color.LightCoral;
                            }
                        }
                    }
                }
            }
        }

        private void ResetColors()
        {
            foreach (Label label in CellLabels)
            {
                label.BackColor = SystemColors.Control;
            }
        }
    }
}
