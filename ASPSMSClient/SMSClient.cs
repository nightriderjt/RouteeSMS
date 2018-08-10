using System;
using System.Threading.Tasks;
using SMSInterfaces.Base;
using SMSInterfaces.Interfaces;

namespace ASPSMSClient
{
    public class SMSClient:SMSInterfaces .Interfaces .ISmsClient
    {
        private IServiceCredentialStoreOauth Credentials { get; set; }

        public Task<ISmsClientResult> SendSmsAsync(string recipientNumber, string message)
        {
            throw new NotImplementedException();
        }

        public Task<ITrackingInfo> TrackSmsAsync(string messageId)
        {
            throw new NotImplementedException();
        }

      
        public event EventHandler<SMSEventArgs> SmSfailed;
        public event EventHandler<SMSEventArgs> SmsSent;
        public event EventHandler<SMSEventArgs> AuthorizationFailed;
    }
}