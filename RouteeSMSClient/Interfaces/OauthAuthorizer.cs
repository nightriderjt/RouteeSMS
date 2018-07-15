using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RouteeSMSClient.Interfaces
{

    /// <summary>
    /// Performs Oauth authorization 
    /// </summary>
    public interface IOauthAuthorizer<T>
    {

        /// <summary>
        /// Authorizes  asynchronous.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        Task<IAuthorizationResult> AuthorizeAsync(IServiceCredentialStoreOauth credentials);
        /// <summary>
        /// Occurs when [authorization failed].
        /// </summary>
        event EventHandler<T> AuthorizationFailed;

    }
}
