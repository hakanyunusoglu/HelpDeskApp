using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBTO
{
    public class RequestListModel<T>
    {
        public List<T> data { get; set; }
    }
}