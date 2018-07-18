using System;
using System.Collections.Generic;
using SMSInterfaces.Enums;
using SMSInterfaces.Interfaces;


namespace RouteeSMSClient.RouteeBase
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SMSInterfaces.Interfaces.ISmsClientResult" />
    public class SmSResult:ISmsClientResult
    {
        /// <summary>
        /// Gets or sets the tracking identifier.
        /// </summary>
        /// <value>
        /// The tracking identifier.
        /// </value>
        public string TrackingId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        /// <summary>
        /// Gets or sets the body the sms message.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public SMSStatus SMSStatus { get; set; }
        /// <summary>
        /// Gets or sets the provider response.
        /// </summary>
        /// <value>
        /// The provider response.
        /// </value>
        public Dictionary<string, string> ProviderResponse { get; set; }
        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public string MessageId { get; set; }
    }
}
