using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Calendar
{
    class Appointment
    {
        public DateTime DateInfo { get; set; }
        public string Description { get; set; }

        public Appointment(DateTime datetime, string description)
        {
            this.DateInfo = datetime;
            this.Description = description;
        }
    }
}
