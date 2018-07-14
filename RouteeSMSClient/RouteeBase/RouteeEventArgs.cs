using System;


namespace RouteeSMSClient.RouteeBase
{
  public  class RouteeEventArgs:EventArgs 
    {
        public RouteeEventArgs()
        {
        }

        public object Data { get; set; }


    }
}
