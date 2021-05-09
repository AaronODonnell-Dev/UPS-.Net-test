using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace UPS.Net_Test
{
    class RESTClient
    {
        public List<UserInfo> GetInfo()
        {
            List<UserInfo> users = new List<UserInfo>();

            var Url = "https://gorest.co.in/public-api/users";

            var syncClient = new WebClient();
            var content = syncClient.DownloadString(Url);

            DataContractJsonSerializer serial = new DataContractJsonSerializer(typeof(Users));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                var userData = (Users)serial.ReadObject(ms);
                users = userData.data.ToList();
            }

            return users;
        }

        public void PostInfo(List<UserInfo> postUsers)
        {
            var Url = "https://gorest.co.in/public-api/users";

            string json = JsonConvert.SerializeObject(postUsers);

            HttpWebRequest request = WebRequest.Create(Url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                Debug.Write(json);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (request.HaveResponse && response != null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                        }
                    }

                }
            }

        }

    }
}
