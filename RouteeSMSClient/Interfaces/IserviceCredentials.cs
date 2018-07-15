

namespace RouteeSMSClient.Interfaces
{
    /// <summary>
    ///  Holds the application ID and Secret for Oauth Token authentication
    /// </summary>
    public interface IServiceCredentialStoreOauth
    {
        string ApplicationId { get; set; }
        string ApplicationSecret { get; set; }
    }
}
