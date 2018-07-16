
using System.Collections.Generic;
using SMSInterfaces.Interfaces;


namespace RouteeSMSClient.RouteeBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IAuthorizationResult" />
    public class AuthorizationToken:IAuthorizationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationToken"/> class.
        /// </summary>
        public AuthorizationToken()
        {
        }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string access_token { get; set; }
        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public string token_type { get; set; }
        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>
        /// The expires in.
        /// </value>
        public int expires_in { get; set; }
        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        public string scope { get; set; }
        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public List<string> permissions { get; set; }
    }
}
