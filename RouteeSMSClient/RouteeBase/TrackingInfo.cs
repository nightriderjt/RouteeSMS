using System;
using System.Collections.Generic;

namespace RouteeSMSClient.RouteeBase
{
    /// <summary>
    /// 
    /// </summary>
    public class TrackingInfo
    {
        public string smsId { get; set; }
        public string messageId { get; set; }
        public string to { get; set; }
        public List<string> groups { get; set; }
        public string country { get; set; }
        public string @operator { get; set; }
        public Status status { get; set; }
        public string body { get; set; }
        public string applicationName { get; set; }
        public string originatingService { get; set; }
        public int latency { get; set; }
        public int parts { get; set; }
        public int part { get; set; }
        public double price { get; set; }
        public string from { get; set; }
        public string direction { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingInfo"/> class.
        /// </summary>
        public TrackingInfo()
        {
        }

        public class Reason
        {
            public string detailedStatus { get; set; }
            public string description { get; set; }
        }

        public class Status
        {
            public string status { get; set; }
            public Reason reason { get; set; }
            public DateTime date { get; set; }
        }
    }
}