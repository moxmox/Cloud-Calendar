using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloud_Calendar
{
    class DateController
    {
        private static DateController Instance;
        public DateTime Now { get; set; }
        public DateTime Focused { get; set; }
        public int Month { get; set; }

        private DateController()
        {
            Now = DateTime.Now;
            Focused = new DateTime(Now.Year, Now.Month, Now.Day);
        }

        public static DateController GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DateController();
            }
            return Instance;
        }

        public int GetDaysInCurrentMonth()
        {
            return DateTime.DaysInMonth(Focused.Year, Focused.Month);
        }

        public String GetStringMonth()
        {
            switch (Focused.Month)
            {
                case 1:
                    return "January";
                case 2:
                    return "Febuary";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    throw new InvalidTimeZoneException();
            }
        }

        public void AddMonth()
        {
            Focused = Focused.AddMonths(1);
        }

        public void SubtractMonth()
        {
            if (Focused.Month - 1 < 1)
            {
                Focused = new DateTime(Focused.Year - 1, 12, Focused.Day);
            }
            else
            {
                Focused = Focused.AddMonths(-1);
            }
        }
    }
}
