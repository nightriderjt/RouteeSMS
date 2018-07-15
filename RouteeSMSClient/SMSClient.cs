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
    /// <seealso cref="T:RouteeSMSClient.Interfaces.IServiceCredentialStoreOauth" />
    /// /// <seealso cref="T:RouteeSMSClient.RouteeBase.SmSResult" />
    public class SmsClient:ISmsClient<RouteeEventArgs >,IOauthAuthorizer<RouteeEventArgs>
    {

        private string AuthorizeUri { get;  } = "https://auth.routee.net/oauth/token";
        private string SmsUri { get; } = "https://connect.routee.net/sms";

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>


        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public AuthorizationToken Token { get; set; }
        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public ClientOptions Options { get;  set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsClient"/> class.
        /// </summary>
        public SmsClient()
        {
            Options = new ClientOptions();
        }

        public SmsClient(RouteeBase.ClientOptions   options)
        {
            Options =options;
        }

    

        /// <summary>
        /// Authorizes the client
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns cref="AuthorizationToken"> an authorization token object or null in case of failure</returns>
        public async Task<IAuthorizationResult> AuthorizeAsync(IServiceCredentialStoreOauth   credentials)
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

        /// <inheritdoc />
        public event EventHandler<RouteeEventArgs> AuthorizationFailed;

        /// <summary>
        /// Sends the SMS asynchronous.
        /// </summary>
        /// <param name="recipientNumber">The recipient number.</param>
        /// <param name="message">The message.</param>
        /// <returns cref="SmSResult"> an SMSreuslt or null in case of failure</returns>
        public async Task<ISmsClientResult> SendSmsAsync(string recipientNumber, string message)
        {
            var client = new RestClient(SmsUri );
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + Token.access_token);
            object body;
            if (string.IsNullOrEmpty(Options.CallBackUri   ))
            {
                body = new
                {
                    body = message,
                    to = recipientNumber,
                    from = this.Options.Originator
                };
            }
            else
            {
                body = new
                {
                    body = message,
                    to = recipientNumber,
                    from = this.Options.Originator,
                    callback = new { url =Options.CallBackUri }
                };
            }

            request.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            var response = await client.ExecutePostTaskAsync(request);

            if (response.IsSuccessful )
            {
                RouteeBase.SmSResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<RouteeBase.SmSResult>(response.Content);
                RouteeEventArgs eventargs = new RouteeEventArgs {Data = result};
                OnSmsSent(eventargs);
                return result;
            }
            else
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        OnAuthorizationFailed(new RouteeEventArgs() { Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content) });
                        return null;
                    default:
                        OnSmSfailed(new RouteeEventArgs()
                        {
                            Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(
                                response.Content)
                        });
                        return null;
                }
            }
        }

        /// <inheritdoc />
        public event EventHandler<RouteeEventArgs> SmSfailed;

        /// <summary>
        /// Occurs when [on SMS sent].
        /// </summary>
        public event EventHandler<RouteeEventArgs> SmsSent;


        /// <summary>
        /// Raises the <see cref="E:AuthorizationFailed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="RouteeEventArgs"/> instance containing the event data.</param>
        protected virtual void OnAuthorizationFailed(RouteeEventArgs e)
        {
            AuthorizationFailed?.Invoke(this, e);
        }


        /// <summary>
        /// Raises the <see cref="E:OnSmSfailed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="RouteeEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSmSfailed(RouteeEventArgs e)
        {
            SmSfailed?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:OnSmsSent" /> event.
        /// </summary>
        /// <param name="e">The <see cref="RouteeEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSmsSent(RouteeEventArgs e)
        {
            SmsSent?.Invoke(this, e);
        }
    }
}
