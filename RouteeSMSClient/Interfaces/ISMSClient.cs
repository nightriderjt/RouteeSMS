using System;
using System.Threading.Tasks;


namespace RouteeSMSClient.Interfaces
{
  public  interface ISmsClient<T1>
    {
        /// <summary>
        /// Sends the SMS asynchronous.
        /// </summary>
        /// <param name="recipientNumber">The recipient number.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task<ISmsClientResult<T1>> SendSmsAsync(string recipientNumber, string message);
       
    }

   
}
