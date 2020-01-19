using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Cloud_Calendar
{
    class CellDialog : Form
    {
        private readonly Size CELLDIALOG_SIZE = new Size(560, 220);
        private readonly Size GENERAL_CONTROL_SIZE = new Size(200, 30);
        private readonly Size BUTTON_SIZE = new Size(100, 20);

        private DateTimePicker AddTimePicker;
        private WaterMarkTextBox AddDescriptionBox;
        private Button AddButton, RemoveButton, CnclButton;
        private ListView AptListView;
        private MainWindow ParentWindow;
        private DateController Controller;
        private DateTime CellDate;
        private DatabaseConnectionController DbController;
        private List<Appointment> Appointments;

        public delegate void CellActionCompleteHandler(object sender, CellDialogActionEventArgs args);
        public event CellActionCompleteHandler ActionCompleted;
        public CellDialog(MainWindow parent)
        {
            DbController = DatabaseConnectionController.GetInstance();
            Controller = DateController.GetInstance();
            CellDate = new DateTime(Controller.Focused.Year, Controller.Focused.Month, Controller.SelectedDay);
            string title = string.Format("Details for: {0} {1}, {2}",
                Controller.GetStringMonth(),
                Controller.Focused.Day,
                Controller.Focused.Year);
            Text = title;

            ParentWindow = parent;
            ParentWindow.Enabled = false;
            Size = CELLDIALOG_SIZE;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            AddTimePicker = new DateTimePicker();
            AddTimePicker.Location = new Point(10, 10);
            AddTimePicker.Size = GENERAL_CONTROL_SIZE;
            AddTimePicker.ShowUpDown = true;
            AddTimePicker.Format = DateTimePickerFormat.Custom;
            AddTimePicker.CustomFormat = "hh:mm tt";
            AddDescriptionBox = new WaterMarkTextBox("Enter event description");
            AddDescriptionBox.TextChanged += new EventHandler(AddDescriptionBox_TextChanged);
            AddDescriptionBox.Location = new Point(220, 10);
            AddDescriptionBox.Size = GENERAL_CONTROL_SIZE;
            AddButton = new Button();
            AddButton.Text = "Add";
            AddButton.Location = new Point(430, 10);
            AddButton.Size = BUTTON_SIZE;
            AddButton.AutoSize = false;
            AddButton.Enabled = false;
            AddButton.Click += new EventHandler(AddButton_Click);
            AptListView = new ListView();
            AptListView.Select();
            int lvWidth = Width - 40;
            AptListView.Size = new Size(lvWidth, 100);
            AptListView.Location = new Point(10, 40);
            AptListView.View = View.Details;
            AptListView.Columns.Add("Date", lvWidth / 4, HorizontalAlignment.Left);
            AptListView.Columns.Add("Description", (lvWidth / 4) * 3, HorizontalAlignment.Left);
            AptListView.FullRowSelect = true;
            AptListView.SelectedIndexChanged += new EventHandler(AptListView_SelectedIndexChange);
            RemoveButton = new Button();
            RemoveButton.Text = "Remove";
            RemoveButton.Size = BUTTON_SIZE;
            RemoveButton.Location = new Point(430, 150);
            RemoveButton.Enabled = false;
            RemoveButton.Click += new EventHandler(RemoveButton_Click);
            CnclButton = new Button();
            CnclButton.Text = "Cancel";
            CnclButton.Size = BUTTON_SIZE;
            int x = 430 - (BUTTON_SIZE.Width + 10);
            CnclButton.Location = new Point(x, 150);
            CnclButton.Click += new EventHandler(CnclButton_Click);

            Controls.Add(AddTimePicker);
            Controls.Add(AddDescriptionBox);
            Controls.Add(AddButton);
            Controls.Add(AptListView);
            Controls.Add(RemoveButton);
            Controls.Add(CnclButton);

            PopulateAppointmentListView();

            FormClosed += new FormClosedEventHandler(CellDialog_Closed);
        }

        private void PopulateAppointmentListView()
        {
            Appointments = DbController.LoadForDay();

            ListViewItem temp;
            foreach (Appointment apt in Appointments)
            {
                temp = new ListViewItem(apt.DateInfo.ToString());
                temp.SubItems.Add(apt.Description);
                AptListView.Items.Add(temp);
            }
        }

        private void AddButton_Click(object sender, EventArgs args)
        {
            DateTime selected_time = AddTimePicker.Value;

            DateTime date = new DateTime(CellDate.Year,
                                            CellDate.Month,
                                            CellDate.Day,
                                            selected_time.Hour,
                                            selected_time.Minute,
                                            selected_time.Second);

             var duplicate_items =  from item in AptListView.Items.Cast<ListViewItem>()
                                    where  item.SubItems[0].Text == date.ToString()
                                    select item;

            if (duplicate_items.Count() > 0)
            {
                string msg = string.Format("There is another event already scheduled at {0}", date);
                MessageBox.Show(msg);
                return;
            }

            DayEntry dayEntry = new DayEntry(date);
            string description = AddDescriptionBox.Text;
            if (dayEntry.AddAppointment(description))
            {
                AddDescriptionBox.Text = "";
                AptListView.Items.Clear();
                PopulateAppointmentListView();
                Appointment apt = new Appointment(date, description);
                ActionCompleted(this, new CellDialogActionEventArgs(CellDate.Day - 1, apt));
            }
        }

        private void CnclButton_Click(object sender, EventArgs args)
        {
            Close();
        }

        private void RemoveButton_Click(object sender, EventArgs args)
        {
            string dateinfo = AptListView.SelectedItems[0].Text;
            string description = AptListView.SelectedItems[0].SubItems[1].Text;
            string msg = string.Format("Remove \"{0}\" at {1} from calendar?", description, dateinfo);
            DialogResult result = MessageBox.Show(msg, "Confirm", MessageBoxButtons.OKCancel);
            if(result.Equals(DialogResult.OK))
            {
                DateTime dt = Convert.ToDateTime(dateinfo);
                DayEntry dayEntry = new DayEntry(dt);
                if(dayEntry.RemoveAppointment(description))
                {
                    AptListView.Items.Clear();
                    PopulateAppointmentListView();
                    Appointment apt = new Appointment(dt, description);
                    ActionCompleted(this, new CellDialogActionEventArgs(CellDate.Day - 1, apt, delete: true));
                }
            }
        }

        private void AptListView_SelectedIndexChange(object sender, EventArgs args)
        {
            if (AptListView.SelectedIndices != null)
            {
                RemoveButton.Enabled = true;
            }
        }

        private void AddDescriptionBox_TextChanged(object sender, EventArgs args)
        {
            if (AddDescriptionBox.Text == "" || AddDescriptionBox.Text == "Enter event description")
            {
                AddButton.Enabled = false;
            }
            else
            {
                AddButton.Enabled = true;
            }
        }

        private void CellDialog_Closed(object sender, EventArgs args)
        {
            ParentWindow.Enabled = true;
        }

        public class CellDialogActionEventArgs
        {
            public int Day;
            public Appointment Appointment;
            public bool Delete;
            public CellDialogActionEventArgs(int day, Appointment appointment, bool delete = false)
            {
                Day = day;
                Appointment = appointment;
                Delete = delete;
            }
        }

    }
}
