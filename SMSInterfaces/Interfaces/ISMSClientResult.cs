

using System.Collections.Generic;
using SMSInterfaces.Enums;

namespace SMSInterfaces.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISmsClientResult
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        SMSStatus SMSStatus { get; set; }

        /// <summary>
        /// Gets or sets the provider response.
        /// </summary>
        /// <value>
        /// The provider response.
        /// </value>
        Dictionary<string,string> ProviderResponse { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        string MessageId { get; set; }
    }
}
