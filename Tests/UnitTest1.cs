using System;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RouteeSMSClient.Interfaces;
using RouteeSMSClient.RouteeBase;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private string _RouteeApplicationID { get; set; } = "5b4b0523e4b0d44c78c64ee6";
        private string _RouteeApplicationSecret { get; set; } = "Nki0HA3WwY";



        [TestMethod]
        public async System.Threading.Tasks.Task AuthorizeRouteeSMSAsync()
        {

           RouteeSMSClient.SmsClient   Client = new RouteeSMSClient.SmsClient();
            Client.AuthorizationFailed += Authenticationfailed;
            IAuthorizationResult  aa = await Client.AuthorizeAsync(new RouteeSMSClient .RouteeBase .Credentials()
                { ApplicationId  = _RouteeApplicationID, ApplicationSecret  = _RouteeApplicationSecret }
            );
            Assert.AreNotEqual(null, aa);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task SendRouteeSMSAsync()
        {

            RouteeSMSClient.SmsClient Client = new RouteeSMSClient.SmsClient
            {
                Options = new ClientOptions() {CallBackUri = "", Originator = "nightrider"}
            };


            Client.AuthorizationFailed += Authenticationfailed;
            IAuthorizationResult aa = await Client.AuthorizeAsync(new RouteeSMSClient.RouteeBase.Credentials()
                { ApplicationId = _RouteeApplicationID, ApplicationSecret = _RouteeApplicationSecret }
            );

            Client.SmsSent += SMSSent;


     var result=   await    Client.SendSmsAsync("+306983446730", "hello there");

            Assert.AreNotEqual(null, result);
        }

        private void SMSSent(object sender, RouteeEventArgs e)
        {
            Console.WriteLine(((RouteeEventArgs)e).Data);
        }

        private void SMSFailed(object sender, RouteeEventArgs e)
        {
            Console.WriteLine(((RouteeEventArgs)e).Data);
        }


        private void Authenticationfailed(object sender, EventArgs  e)
        {
            Console.WriteLine(((RouteeEventArgs)e).Data);
        }
    }
}
