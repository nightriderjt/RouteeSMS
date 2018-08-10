

namespace SMSInterfaces.Interfaces
{
    /// <summary>
    ///  Holds the application ID and Secret for Oauth Token authentication
    /// </summary>
    public interface IServiceCredentialStoreOauth:ICredentialStore 
    {
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        string ApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the application secret.
        /// </summary>
        /// <value>
        /// The application secret.
        /// </value>
        string ApplicationSecret { get; set; }
    }
}
