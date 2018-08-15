using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ASPSMSClient.Base;
using RestSharp;
using SMSInterfaces.Base;
using SMSInterfaces.Enums;
using SMSInterfaces.Interfaces;

namespace ASPSMSClient
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SMSInterfaces.Interfaces.ISmsClient" />
    public class SmsClient:SMSInterfaces .Interfaces .ISmsClient
    {
        private string SmsUri { get; } = "https://json.aspsms.com/SendTextSMS";

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public ASPSMSClient .Base .Options Options { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsClient"/> class.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="options">The options.</param>
        public SmsClient(SMSInterfaces .Interfaces .ICredentialStore  credentials, Options options)
        {
            Credentials =(IServiceCredentialsStorePlain) credentials;
            Options = options;
        }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        private IServiceCredentialsStorePlain  Credentials { get; set; }

        public async Task<ISmsClientResult> SendSmsAsync(string recipientNumber, string message)
        {
            var client = new RestClient(SmsUri);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");

            var body = new Dictionary<string, object>
            {
                { "Username", this.Credentials.Username },
                { "Password", this.Credentials.Password },
                { "Originator", this.Options.Originator },
                { "Recipients",new List<string> {recipientNumber}},
                { "MessageText", message },
                { "DeferredDeliveryTime", this.Options.DeferredDeliveryTime },
                { "FlashingSMS", this.Options.FlashingSms },
                { "URLDeliveryNotification", this.Options.UrlDeliveryNotification },
                { "URLNonDeliveryNotification", this.Options.UrlNonDeliveryNotification },
                { "AffiliateID", this.Options.AffiliateId },
                { "ForceGSM7bit", this.Options.ForceGsm7Bit }
            };


            request.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            var response = await client.ExecutePostTaskAsync(request);

            if (response.IsSuccessful)
            {

                var responseJson =(ASPSMSClient.Base.TrackingInfo)
                    Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content,typeof(ASPSMSClient.Base.TrackingInfo));
                if (responseJson.StatusCode != "1")
                {
                    var result = new ASPSMSClient.Base.SmsResult
                    {
                        SMSStatus = SMSStatus.FailedToSend,
                        ProviderResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content)
                    };
                    OnSmSfailed(new SMSEventArgs()
                    {
                        Data = result.ProviderResponse
                    });

                    return result;
                }
                else
                {
                    var result = new ASPSMSClient.Base.SmsResult
                    {
                        SMSStatus = SMSStatus.Sent,
                        ProviderResponse = (Dictionary<string, string>)Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content, typeof(Dictionary<string, string>)),
                        TrackingId = "",

                        From = this.Options.Originator,
                        To = recipientNumber
                    };
                    var eventargs = new SMSEventArgs { Data = result };

                    OnSmsSent(eventargs);
                    return result;
                }
            }
            else
            {
                var result = new ASPSMSClient.Base.SmsResult
                {
                    SMSStatus = SMSStatus.FailedToSend,
                    ProviderResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content)
                };
                OnSmSfailed(new SMSEventArgs()
                        {
                            Data = result.ProviderResponse
                        });
              
                return result;
            }
        }

     

      
        public event EventHandler<SMSEventArgs> SmSfailed;
        public event EventHandler<SMSEventArgs> SmsSent;

        protected virtual void OnSmSfailed(SMSEventArgs e)
        {
            SmSfailed?.Invoke(this, e);
        }

        protected virtual void OnSmsSent(SMSEventArgs e)
        {
            SmsSent?.Invoke(this, e);
        }

        public async Task<List<ISmsClientResult>> SendSmsAsync(List<string> recipientNumbers, string message)
        {
            var results = new List<ISmsClientResult>();
            foreach (var recipient in recipientNumbers)
            {
                results.Add(await this.SendSmsAsync(recipient, message));
            }
            return results;
        }
    }
}