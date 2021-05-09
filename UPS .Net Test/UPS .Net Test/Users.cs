using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UPS.Net_Test
{
    [DataContract]
    public class Users
    {
        [DataMember]
        public int code { get; set; }
        [DataMember]
        public Meta meta { get; set; }
        [DataMember]
        public List<UserInfo> data { get; set; }
    }

    public class UserInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }

        public override string ToString()
        {
            return "Name: " + name; /*+ " Gender: " + gender + " Status: " + status + "\nEmail: " + email;*/
        }
    }

    public class Data
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

    public class Meta
    {
        public Data data { get; set; }
    }
}
