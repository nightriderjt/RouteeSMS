using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RouteeSMSClient.Interfaces;
using RouteeSMSClient.RouteeBase;

namespace RouteeSMSClient
{
    /// <summary>
    /// The Routee SMS Client
    /// </summary>
    /// <seealso cref="IServiceCredentialStoreOauth" />
    /// /// <seealso cref="SmSResult" />
    public class SmsClient:ISmsClient<SmSResult>,IOauthAuthorizer<IServiceCredentialStoreOauth >
    {

        private string AuthorizeUri { get;  } = "https://auth.routee.net/oauth/token";
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public AuthorizationToken Token { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsClient"/> class.
        /// </summary>
        public SmsClient()
        {
        }



        /// <summary>
        /// Authorizes the client
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns cref="AuthorizationToken"> an autorization token object or null in case of failure</returns>
        public async Task<IAuthorizationResult> AuthorizeAsync(IServiceCredentialStoreOauth credentials)
        {
           
         var keyToEncode = credentials.ApplicationId  + ":" + credentials.ApplicationSecret ;
          var encodedValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(keyToEncode));

            var client = new RestClient(AuthorizeUri);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", "Basic " + encodedValue);
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials", ParameterType.RequestBody);
            var response =await client.ExecutePostTaskAsync(request);
            if (response.IsSuccessful)
            {
                Token= Newtonsoft.Json.JsonConvert.DeserializeObject<RouteeBase.AuthorizationToken>(response.Content);
                return  Token ;
            }
            else
            {
                OnAuthorizationFailed(new RouteeEventArgs() { Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content) });
                return null;
            }
        }

        public event EventHandler AuthorizationFailed;

        public async Task<ISmsClientResult<SmSResult>> SendSmsAsync(string recipientNumber, string message)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnAuthorizationFailed(RouteeEventArgs e)
        {
            AuthorizationFailed?.Invoke(this, e);
        }

      
    }
}
