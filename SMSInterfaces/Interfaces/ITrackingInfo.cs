namespace SMSInterfaces.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITrackingInfo
    {
        /// <summary>
        /// Gets or sets the messageid.
        /// </summary>
        /// <value>
        /// The messageid.
        /// </value>
        string Messageid { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        Enums.SMSStatus  Status { get; set; }
        /// <summary>
        /// Gets or sets the providerresponse.
        /// </summary>
        /// <value>
        /// The providerresponse.
        /// </value>
        object Providerresponse { get; set; }
    }
}