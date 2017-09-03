using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class HttpRequest
    {
        public void TakeNewAccessToken(String MQTT_IP, String CLIENT_ID, String CLIENT_SECRET)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            string url = "https://" + "192.168.8.101" + ":8243/token";
            WebRequest myReq = WebRequest.Create(url);
            myReq.Method = "POST";
            string credentials = CLIENT_ID + ":" + CLIENT_SECRET;
            CredentialCache mycache = new CredentialCache();

            string postData = "grant_type=password&username=admin&password=admin&scope=perm:admin:device-type perm:device-types:events perm:device-types:events:view perm:device-types:types perm:devices:operations";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(postData);

            // Set the content type of the data being posted.
            myReq.ContentType = "application/x-www-form-urlencoded";
            myReq.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));

            // Set the content length of the string being posted.
            myReq.ContentLength = byte1.Length;
            Stream newStream = myReq.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            WebResponse wr = myReq.GetResponse();
            Stream receiveStream = wr.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
            string content = reader.ReadToEnd();
            Console.WriteLine(content);
            var json = "[" + content + "]"; // change this to array
            var objects = JArray.Parse(json); // parse as array  
            foreach (JObject o in objects.Children<JObject>())
            {
                foreach (JProperty p in o.Properties())
                {
                    string name = p.Name;
                    string value = p.Value.ToString();
                    Console.Write(name + ": " + value);
                }
            }
            Console.ReadLine();
        }
    }
}
