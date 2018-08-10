using System.Collections.Generic;
using SMSInterfaces.Enums;
using SMSInterfaces.Interfaces;

namespace ASPSMSClient.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SMSInterfaces.Interfaces.ISmsClientResult" />
    public class SmsResult:ISmsClientResult 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsResult"/> class.
        /// </summary>
        public SmsResult()
        {
        }

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
        /// Gets or sets the tracking identifier.
        /// </summary>
        /// <value>
        /// The tracking identifier.
        /// </value>
        public string TrackingId { get; set; }

        public string From { get; set; }
        public string To { get; set; }
    }
}