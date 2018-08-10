using RestSharp;
using RouteeSMSClient.RouteeBase;
using SMSInterfaces.Base;
using SMSInterfaces.Enums;
using SMSInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Extensions;

namespace RouteeSMSClient
{



    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SMSInterfaces.Interfaces.ISmsClient" />
    /// <seealso cref="SMSInterfaces.Interfaces.IAuthorizer" />
    public class SmsClient:ISmsClient,IAuthorizer
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsClient"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SmsClient(ClientOptions options)
        {
            Options =options;
        }



    
        /// <summary>
        /// Authorizes the client
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns cref="IAuthorizationResult"> an authorization token object or null in case of failure</returns>
        /// /// <returns cref="AuthorizationToken "> an authorization token object or null in case of failure</returns>
        public async Task<IAuthorizationResult> AuthorizeAsync(ICredentialStore    credentials)
        {
            var credentialStore =(IServiceCredentialStoreOauth ) credentials;
         var keyToEncode = credentialStore.ApplicationId  + ":" + credentialStore.ApplicationSecret ;
          var encodedValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(keyToEncode));

            var client = new RestClient(AuthorizeUri);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", "Basic " + encodedValue);
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials", ParameterType.RequestBody);
            var response =await client.ExecutePostTaskAsync(request);
            if (response.IsSuccessful)
            {
                Token= Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorizationToken>(response.Content);
                Token.Status = AuthorizationStatus.Authorized;
                return  Token ;
            }
            else
            {
                OnAuthorizationFailed(new SMSEventArgs() { Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content) });
                Token.Status = AuthorizationStatus.Unauthorized ;
                return null;
            }
        }

        /// <summary>
        /// Occurs when [authorization failed].
        /// </summary>
        /// <inheritdoc />
        public event EventHandler<SMSEventArgs> AuthorizationFailed;


        /// <summary>
        /// Sends the SMS asynchronous.
        /// </summary>
        /// <param name="recipientNumber">The recipient number.</param>
        /// <param name="message">The message.</param>
        /// <returns cref="SmSResult"></returns>
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
                    from = Options.Originator
                };
            }
            else
            {
                body = new
                {
                    body = message,
                    to = recipientNumber,
                    from = Options.Originator,
                    callback = new { url =Options.CallBackUri }
                };
            }

            request.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            var response = await client.ExecutePostTaskAsync(request);

            if (response.IsSuccessful )
            {
                var result = new SmSResult
                {
                    SMSStatus = SMSStatus.Sent,
                    ProviderResponse = (Dictionary<string, string>)Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content, typeof(Dictionary<string, string>))
                };
                result.TrackingId  = result.ProviderResponse["trackingId"];
                result.Body = result.ProviderResponse["body"];
                result.CreatedAt =DateTime.Parse(  result.ProviderResponse["createdAt"]);
                result.From  = result.ProviderResponse["from"];
                result.To  = result.ProviderResponse["to"];
                result.TrackingId = result.ProviderResponse["trackingId"];
                var eventargs = new SMSEventArgs { Data = result};

                OnSmsSent(eventargs);
                return result;
            }
            else
            {
                SmSResult result = new SmSResult();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        result.SMSStatus = SMSStatus.BadAuthorization;
                        result.ProviderResponse =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                     
                        OnAuthorizationFailed(new SMSEventArgs() { Data = result.ProviderResponse  });
                        break;
                    default:
                        result.SMSStatus = SMSStatus.FailedToSend ;
                        result.ProviderResponse =Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                        OnSmSfailed(new SMSEventArgs()
                        {
                             Data =result.ProviderResponse 
                        });
                        break;
                }
                return result;
            }
        }



        /// <summary>
        /// Tracks the SMS asynchronous.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <returns cref="TrackInfo">TrackInfo</returns>
        public async Task<ITrackingInfo> TrackSmsAsync(string messageId)
        {
            var client = new RestClient("https://connect.routee.net/sms/tracking/single/" + messageId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + Token.access_token);
            var response = await client.ExecuteTaskAsync(request);
            var result = new SMSInterfaces.Base.TrackInfo();

            if (response.IsSuccessful)
            {
                var jsonresult =
                    (List<TrackingInfo>) Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content,
                        typeof(List<TrackingInfo>));
               
                result.Providerresponse = jsonresult[0];
                result.Messageid = messageId;

                switch (jsonresult[0].status.status)
                {
                    case "Delivered":
                        result.Status = SMSStatus.Delivered;
                        break;
                    case "Undelivered":
                        result.Status = SMSStatus.Undelivered;
                        break;
                    case "Queued":
                        result.Status = SMSStatus.Queued ;
                        break;
                    case "Sent":
                        result.Status = SMSStatus.Sent ;
                        break;
                    case "Failed":
                        result.Status = SMSStatus.FailedToSend ;
                        break;
                    case "Unsent":
                        result.Status = SMSStatus.FailedToSend;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                       result.Status  = SMSStatus.BadAuthorization;
                        result.Providerresponse  =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);

                        OnAuthorizationFailed(new SMSEventArgs() {Data = result.Providerresponse });
                        break;
                    default:
                        result.Status = SMSStatus.FailedToSend;
                        result.Providerresponse =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                        OnSmSfailed(new SMSEventArgs()
                        {
                            Data = result.Providerresponse
                        });
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Occurs when [on sms failed].
        /// </summary>
        /// <inheritdoc />
        public event EventHandler<SMSEventArgs> SmSfailed;

        /// <summary>
        /// Occurs when [on SMS sent].
        /// </summary>
        public event EventHandler<SMSEventArgs> SmsSent;



        /// <summary>
        /// Raises the <see cref="E:AuthorizationFailed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SMSEventArgs"/> instance containing the event data.</param>
        protected virtual void OnAuthorizationFailed(SMSEventArgs e)
        {
            AuthorizationFailed?.Invoke(this, e);
        }


        /// <summary>
        /// Raises the <see cref="E:OnSmSfailed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SMSEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSmSfailed(SMSEventArgs e)
        {
            SmSfailed?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:OnSmsSent" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SMSEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSmsSent(SMSEventArgs e)
        {
            SmsSent?.Invoke(this, e);
        }
    }
}
