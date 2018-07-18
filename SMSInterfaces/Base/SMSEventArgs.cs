using System;
using System.Collections.Generic;
using System.Text;

namespace SMSInterfaces.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.EventArgs" />
  public  class SMSEventArgs :EventArgs 
    {
        public SMSEventArgs()
        {
        }

        public SMSEventArgs(object data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public object Data { get; set; }
    }
}
