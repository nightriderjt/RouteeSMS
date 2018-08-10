using System;
using SMSInterfaces.Interfaces;

namespace ASPSMSClient.Base
{
    public class Options : IOptions
    {
        /// <summary>
        /// URL that will be called when a message is delivered instantly. The submitted TransactionReferenceNumber will added to the URL.
        /// </summary>
        /// <value>
        /// The URL delivery notification.
        /// </value>
        public string UrlDeliveryNotification { get; set; }
        /// <summary>
        /// URL that will be connected when a message is not delivered. The submitted TransactionReferenceNumber will added to the URL.
        /// </summary>
        /// <value>
        /// The URL non delivery notification.
        /// </value>
        public string UrlNonDeliveryNotification { get; set; }
        /// <summary>
        /// If you are a affiliate partner with ASPSMS you can provide your affiliate-id here.
        /// </summary>
        /// <value>
        /// The affiliate identifier.
        /// </value>
        public string AffiliateId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether Force sending GSM7bit characters only to avoid extra cost by acidentally using Unicode Characters [force GSM7 bit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [force GSM7 bit]; otherwise, <c>false</c>.
        /// </value>
        public bool ForceGsm7Bit { get; set; }
        /// <summary>
        /// You can use a phone number or up to 11 Alphabetic characters (e.g. 'MYBUSINESS') as Originator. In order to use a phone number (e.g. '+4176000000') you must unlock it first using the UnlockOriginator method
        /// </summary>
        /// <value>
        /// The originator.
        /// </value>
        public string Originator { get; set; }
        /// <summary>
        /// By setting the deferred delivery time you can schedule jobs to execute at a specific time. The date time format accepted is ISO8601 UTC YYYY-MM-DDThh:mm:ssTZD (e.g.'2018-08-31T14:00:07+02:00')
        /// </summary>
        /// <value>
        /// The deferred delivery time.
        /// </value>
        public DateTime? DeferredDeliveryTime {get;set;}= null;

        /// <summary>
        /// The message appears directly on the screen of the mobile phone. A flashing message isn't saved directly on the mobile phone. The recipient has to choose to save it. Otherwise the message irrevocably disappears from the screen.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [flashing SMS]; otherwise, <c>false</c>.
        /// </value>
        public bool FlashingSms { get; set; }
    }
}