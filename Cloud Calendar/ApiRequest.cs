using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cloud_Calendar
{
    class ApiRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("action")]
        public int Action { get; set; }
        
        //TODO: more to be added later as need for specific data becomes obvious
    }

    class ActionCode 
    {
        public static int CHECK_CREDENTIALS = 1964;
        public static int GET_ALL = 7456;
        public static int GET_FOR_DAY = 8934;
        public static int ADD_EVENT = 1329;

    }

    class ResponseCode
    {
        public static int CREDENTIALS_VALID = 4498;
    }
}
