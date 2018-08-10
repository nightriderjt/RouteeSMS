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
    public class SmsClient:SMSInterfaces .Interfaces .ISmsClient
    {
        private string SmsUri { get; } = "https://json.aspsms.com/SendTextSMS";

        public ASPSMSClient .Base .Options Options { get; set; }

        public SmsClient(IServiceCredentialsStorePlain credentials, Options options)
        {
            Credentials = credentials;
            Options = options;
        }

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

                ASPSMSClient.Base.TrackingInfo responseJson =(ASPSMSClient.Base.TrackingInfo)
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

        public Task<ITrackingInfo> TrackSmsAsync(string messageId)
        {
            throw new NotImplementedException();
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
    }
}