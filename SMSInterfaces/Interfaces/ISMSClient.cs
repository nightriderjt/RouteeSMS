using System;
using System.Threading.Tasks;

namespace SMSInterfaces.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISmsClient<T>
    {
        /// <summary>
        /// Sends the SMS asynchronous.
        /// </summary>
        /// <param name="recipientNumber">The recipient number.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task<ISmsClientResult> SendSmsAsync(string recipientNumber, string message);

        /// <summary>
        /// Occurs when [on sms failed].
        /// </summary>
        event EventHandler<T>  SmSfailed ;

        /// <summary>
        /// Occurs when [on SMS sent].
        /// </summary>
        event EventHandler<T> SmsSent;
    }
}

   

