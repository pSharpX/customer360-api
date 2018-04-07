using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Transport
{
    public class LogDTO
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string CallSite { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Method { get; set; }
        public string InnerException { get; set; }
        public string AdditionalInfo { get; set; }
        public string LoggedOnDate { get; set; }
        public string BrowSer { get; set; }
        public string User { get; set; }
        public string RequestUrl { get; set; }
        public string IPAddressServer { get; set; }
        public string IPRemoteServer { get; set; }
    }
}
