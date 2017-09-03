using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader r = new StreamReader("win.json"))
            {
                byte[] temp = Encoding.ASCII.GetBytes("realMessage");

                string json = r.ReadToEnd();
                JsonData jsondata = JsonConvert.DeserializeObject<JsonData>(json);
                Console.WriteLine(jsondata.type);
                HttpRequest getnewtoken = new HttpRequest();
                getnewtoken.TakeNewAccessToken(jsondata.mqttGateway, jsondata.clientId, jsondata.clientSecret);




                MqttClient client = new MqttClient("test.mosquitto.org");

                // register to message received
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);

                // subscribe to the topic "/home/temperature" with QoS 2
                client.Subscribe(new string[] { "/home/temperature/jk" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                   
                // publish a message on "/home/temperature" topic with QoS 2
                client.Publish("/home/temperature/jk", temp, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,false);


                Console.ReadLine();



            }
            
        }
        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // handle message received
            Console.WriteLine(Encoding.UTF8.GetString(e.Message));
        }
    }
}
