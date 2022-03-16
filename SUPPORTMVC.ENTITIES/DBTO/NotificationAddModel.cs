using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBTO
{
    public class NotificationAddModel
    {
      public int NotifyID { get; set; }
        public string NotifyMessage { get; set; }
        public string NotifySubject { get; set; }
        public string NotifyAddedDate { get; set; }
        public string NotifyAddedHour { get; set; }
    }
}