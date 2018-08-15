using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMSInterfaces.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISMStracker
    {

        /// <summary>
        /// Tracks the SMS asynchronous.
        /// </summary>
        /// <param name="MessageID">The message identifier.</param>
        /// <returns></returns>
        Task<ITrackingInfo> TrackSmsAsync(string MessageID);
    }
}
