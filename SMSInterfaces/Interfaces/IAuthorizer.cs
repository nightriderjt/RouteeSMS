using System;
using System.Threading.Tasks;

namespace SMSInterfaces.Interfaces
{

    /// <summary>
    /// Performs Oauth authorization 
    /// </summary>
    public interface IAuthorizer
    {

        /// <summary>
        /// Authorizes  asynchronous.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        Task<IAuthorizationResult> AuthorizeAsync(ICredentialStore  credentials);
        /// <summary>
        /// Occurs when [authorization failed].
        /// </summary>
        event EventHandler<Base.SMSEventArgs> AuthorizationFailed;

    }
}
