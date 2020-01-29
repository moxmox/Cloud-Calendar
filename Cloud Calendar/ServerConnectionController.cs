using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Calendar
{
    class ServerConnectionController
    {
        private const string IP = "192.168.0.6"; //TODO: will changed for public access
        private const string PORT = "4444";

        private static ServerConnectionController Instance;

        private HttpClient client;

        private ServerConnectionController()
        {
            client = new HttpClient();
            string textUrl = string.Format("httpClient://{0}:{1}", IP, PORT);
            client.BaseAddress = new Uri(textUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); ;
        }

        public static ServerConnectionController GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ServerConnectionController();
            }
            return Instance;
        }

        public List<DayEntry> LoadEntries()
        {
            //TODO: code to load all Appointments for a year & month saved to date controller focussed object
            throw new NotImplementedException();
        }

        public List<Appointment> LoadForDay()
        {
            //TODO: code to load all Apointments for a single day
            throw new NotImplementedException();
        }

        public bool PushAppointment(Appointment apt)
        {
            //TODO: code to push appointment to server
            throw new NotImplementedException();
        }

        public bool DeleteAppointment(Appointment apt)
        {
            //TODO: code to delete appointment from server
            throw new NotImplementedException();
        }
    }
}
