using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RouteeSMSClient.Interfaces
{
    interface IOauthAuthorizer<in T> where T:class
    {

        Task<IAuthorizationResult> AuthorizeAsync(T credentials);
        /// <summary>
        /// Occurs when [authorization failed].
        /// </summary>
        event EventHandler AuthorizationFailed;

    }
}
