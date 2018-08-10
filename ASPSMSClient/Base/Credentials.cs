using SMSInterfaces.Interfaces;

namespace ASPSMSClient.Base
{
    public class Credentials:IServiceCredentialsStorePlain  
    {
        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}