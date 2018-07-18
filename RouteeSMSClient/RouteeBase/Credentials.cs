


using SMSInterfaces.Interfaces;

namespace RouteeSMSClient.RouteeBase
{
    /// <summary>
    /// The Oauth credentials for Routee SMS application for authentication
    /// </summary>
    public class Credentials :IServiceCredentialStoreOauth
    {
        public Credentials()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="applicationSecret">The application secret.</param>
        public Credentials(string applicationId, string applicationSecret)
        {
            ApplicationId = applicationId;
            ApplicationSecret = applicationSecret;
        }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public string ApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the application secret.
        /// </summary>
        /// <value>
        /// The application secret.
        /// </value>
        public string ApplicationSecret { get; set; }
    }
}
