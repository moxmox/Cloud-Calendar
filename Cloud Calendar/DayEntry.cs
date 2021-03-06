﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        /**
         * These functions should not interact directly with database; only calls to db should 
         * be made indirectly through instances of DatabaseConnectionController;
         *    
         */
        public void AddAppointment(Appointment appointment)
        {
            Appointments.Add(appointment);
        }

        public bool AddAppointment(string description)
        {
            DatabaseConnectionController dbController = DatabaseConnectionController.GetInstance();
            Appointment apt = new Appointment(DateInfo, description);
            if (dbController.PushAppointment(apt))
            {
                Appointments.Add(apt);
                return true;
            }
            return false;
        }

        public void RemoveAppointment(Appointment apt)
        {
            var objectForDeletion = from a in Appointments
                                    where (a.DateInfo == apt.DateInfo && a.Description == apt.Description)
                                    select a;
                                    
            Appointments.Remove(objectForDeletion.First());
        }

        public bool RemoveAppointment(string description)
        {
            DatabaseConnectionController dbController = DatabaseConnectionController.GetInstance();
            Appointment apt = new Appointment(DateInfo, description);
            if (dbController.DeleteAppointment(apt))
            {
                Appointments.Remove(apt);
                return true;
            }
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
