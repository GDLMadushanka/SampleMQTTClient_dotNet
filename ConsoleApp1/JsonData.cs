using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class JsonData
    {
        public String type { get; set; }
        public String deviceId { get; set; }
        public String clientId { get; set; }
        public String clientSecret { get; set; }
        public String accessToken { get; set; }
        public String refreshToken { get; set; }
        public String mqttGateway { get; set; }
        public String httpsGateway { get; set; }
        public String httpGateway { get; set; }
    }
}