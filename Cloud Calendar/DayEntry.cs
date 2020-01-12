using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Calendar
{
    class DayEntry
    {
        private List<Appointment> Appointments= new List<Appointment>();

        public DateTime DateInfo { get; }

        public DayEntry(DateTime dateTime)
        {
            DateInfo = dateTime;
        }

        public bool HasAppointments()
        {
            if (Appointments.Count > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public bool AddAppointment(Appointment appointment,bool updateDB)
        {
            if(!updateDB)
            {
                Appointments.Add(appointment);
                return true;
            }
            else
            {
                //TODO update database and then add to list
            }
            return false;
        }

        public bool RemoveAppointment()
        {
            //TODO add code to remove appointment from list and also from database
            return false;
        }

        public override string ToString()
        {
            string value = "";
            value += DateInfo.ToString() + "/";
            value += string.Format("Has {0} Appointments.", Appointments.Count);
            return value;
        }
    }
}
