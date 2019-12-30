using System;
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
            string connectionString = @"server=192.168.0.6;userid=calendar_user;password=" + MYSQL_PASSWORD + ";database=cloud_calendar;";
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
    }
}
