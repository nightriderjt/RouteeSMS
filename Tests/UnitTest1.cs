using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RouteeSMSClient.RouteeBase;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private string _RouteeApplicationID { get; set; } = "5b4b0523e4b0d44c78c64ee5";
        private string _RouteeApplicationSecret { get; set; } = "Nki0HA3WwY";



        [TestMethod]
        public async System.Threading.Tasks.Task AuthorizeRouteeSMSAsync()
        {

            RouteeSMSClient.SmsClient Client = new RouteeSMSClient.SmsClient();

            Client.AuthorizationFailed += Authenticationfailed;
            var aa = await Client.AuthorizeAsync(new RouteeSMSClient .RouteeBase .Credentials()
                { ApplicationId  = _RouteeApplicationID, ApplicationSecret  = _RouteeApplicationSecret }
            );
            Assert.AreNotEqual(null, aa);
        }

    

        private void Authenticationfailed(object sender, EventArgs  e)
        {
            Console.WriteLine(((RouteeEventArgs)e).Data);
        }
    }
}
