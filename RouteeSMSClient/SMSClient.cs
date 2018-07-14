using System;

using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RouteeSMSClient.Interfaces;
using RouteeSMSClient.RouteeBase;

namespace RouteeSMSClient
{
    public class SmsClient:ISmsClient<SmSResult,Credentials>
    {

        private string AuthorizeUri { get;  } = "https://auth.routee.net/oauth/token";
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
        public async Task<IAuthorizationResult> AuthorizeAsync(Credentials credentials)
        {
           
         var keyToEncode = credentials.ApplicationId  + ":" + credentials.ApplicationSecret ;
          var encodedValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(keyToEncode));

            var client = new RestClient(AuthorizeUri);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", "Basic " + encodedValue);
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials", ParameterType.RequestBody);
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Token   = Newtonsoft.Json.JsonConvert.DeserializeObject<RouteeBase.AuthorizationToken>(response.Content);
                return  Token ;
            }
            else
            {
                var error = new 
                {
                    ErrorCode = "0",
                    Errormessage = response.Content
                };
                OnAuthorizationFailed(new RouteeEventArgs() { Data =error });
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
