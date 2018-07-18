

using System;
using SMSInterfaces.Enums;

namespace SMSInterfaces.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthorizationResult
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        AuthorizationStatus Status { get; set; }
    }
}
