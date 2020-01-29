using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Cloud_Calendar
{
    class DatabaseConnectionController
    {
        private static DatabaseConnectionController Instance;
        private const string MYSQL_PASSWORD = @"X78v9o!3";

        public MySqlConnection Connection { get; set; }

        private DatabaseConnectionController()
        {
            string connectionString = @"server=68.5.17.113;userid=calendar_user;password=" + MYSQL_PASSWORD + ";database=cloud_calendar;";
            try
            {
                Connection = new MySqlConnection(connectionString);
                Connection.Open();
            }catch(Exception e)
            {
                string msg = string.Format("Problem connecting to database: {0}", e);
                MessageBox.Show(msg);
            }
        }

        public static DatabaseConnectionController GetInstance()
        {
            if(Instance == null)
            {
                Instance = new DatabaseConnectionController();
            }
            return Instance;
        }
                       
        public List<DayEntry> LoadEntries()
        {
            DateController controller = DateController.GetInstance();
            DateTime focused = controller.Focused;
            List<DayEntry> entries = new List<DayEntry>();
            for(int i = 0; i < controller.GetDaysInCurrentMonth(); i++)
            {
                entries.Add(new DayEntry(new DateTime(focused.Year, focused.Month, i + 1)));
            }
            string sql = string.Format(@"SELECT * FROM event WHERE MONTH(datetime) = {0} AND YEAR(datetime) = {1}", focused.Month, focused.Year);
            var command = new MySqlCommand(sql, Connection);
            MySqlDataReader reader = command.ExecuteReader();
            Appointment temp;
            while(reader.Read())
            {
                DateTime eventDate = reader.GetDateTime("datetime");
                string description = reader.GetString("description");
                temp = new Appointment(eventDate, description);
                foreach(DayEntry entry in entries)
                {
                    if(temp.DateInfo.Day == entry.DateInfo.Day)
                    {
                        entry.AddAppointment(appointment: temp);
                    }
                }
            }
            reader.Close();
            return entries;
        }

        public List<Appointment> LoadForDay()
        {
            DateController controller = DateController.GetInstance();
            DateTime focused = controller.Focused;

            string sql = string.Format(@"SELECT * FROM event WHERE MONTH(datetime) = {0} AND YEAR(datetime) = {1} AND DAY(datetime) = {2}",
                            focused.Month, focused.Year, controller.SelectedDay
                         );
            var command = new MySqlCommand(sql, Connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Appointment> entries = new List<Appointment>();
            Appointment temp;
            while(reader.Read())
            {
                temp = new Appointment(reader.GetDateTime("datetime"), reader.GetString("description"));
                entries.Add(temp);
            }
            reader.Close();
            return entries;
        }

        // TODO: consider finding way to merge part of PushAppointment() & DeleteAppointment() 
        // in order to reduce repetitive code; Using an UpdateAppointments() method with
        // a MySqlCommand object passed to it could achieve same result
        // example: UpdateAppointments(Appointment apt, MySqlCommand command);
        public bool PushAppointment(Appointment apt)
        {
            bool success = false;
            string sql = @"INSERT INTO event (datetime, description) VALUES (@datetime, @description)";
            var command = new MySqlCommand(sql, Connection);
            command.Parameters.AddWithValue("@datetime", apt.DateInfo);
            command.Parameters.AddWithValue("@description", apt.Description);
            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    success = true;
                }
            }
            catch (MySqlException mysqlx)
            {
                string msg = string.Format("An error occured while updating the database: {0}", mysqlx);
                MessageBox.Show(msg);
            }
            return success;
        }

        public bool DeleteAppointment(Appointment apt)
        {
            bool success = false;
            string sql = @"DELETE FROM event WHERE datetime = @datetime AND description = @description";
            MySqlCommand command = new MySqlCommand(sql, Connection);
            command.Parameters.AddWithValue("@datetime", apt.DateInfo);
            command.Parameters.AddWithValue("@description", apt.Description);
            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    success = true;
                }
            }catch(MySqlException mysqlx)
            {
                string msg = string.Format("An error occured while updating the database: {0}", mysqlx);
                MessageBox.Show(msg);
            }
            return success;
        }
    }
}
