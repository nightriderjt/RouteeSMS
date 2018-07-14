using System;


namespace RouteeSMSClient.RouteeBase
{
   
    public class SmSResult
    {
        public string TrackingId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}
