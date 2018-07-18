using System;
using System.Collections.Generic;
using System.Text;
using SMSInterfaces.Interfaces;


namespace RouteeSMSClient.RouteeBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IOptions" />
    /// <inheritdoc />
    public class ClientOptions:IOptions 
    {
        public ClientOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientOptions" /> class.
        /// </summary>
        /// <param name="callBackUri">The call back URI.</param>
        /// <param name="originator">The originator.</param>
        /// <inheritdoc />
        public ClientOptions(string callBackUri, string originator)
        {
            CallBackUri = callBackUri;
            Originator = originator;
        }

        /// <summary>
        /// Gets or sets the call back URI.
        /// </summary>
        /// <value>
        /// The call back URI.
        /// </value>
        public string CallBackUri { get; set; }


        /// <summary>
        /// Gets or sets the originator.
        /// </summary>
        /// <value>
        /// The originator.
        /// </value>
        public string Originator { get; set; }


    }
}
