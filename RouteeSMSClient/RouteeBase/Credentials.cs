
using RouteeSMSClient.Interfaces;

namespace RouteeSMSClient.RouteeBase
{
    /// <summary>
    /// 
    /// </summary>
    public class Credentials :IServiceCredentialStore
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

        public string ApplicationId { get; set; }
        public string ApplicationSecret { get; set; }


    }
}
