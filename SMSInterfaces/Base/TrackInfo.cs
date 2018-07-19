using System;
using System.Collections.Generic;
using System.Text;
using SMSInterfaces.Enums;
using SMSInterfaces.Interfaces;

namespace SMSInterfaces.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SMSInterfaces.Interfaces.ITrackingInfo" />
    /// <inheritdoc />
  public  class TrackInfo :ITrackingInfo 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackInfo"/> class.
        /// </summary>
        public TrackInfo()
        {
        }

        public string Messageid { get; set; }
        public SMSStatus Status { get; set; }
        public object Providerresponse { get; set; }
    }
}
