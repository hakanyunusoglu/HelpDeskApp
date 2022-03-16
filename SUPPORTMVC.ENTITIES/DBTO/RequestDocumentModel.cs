using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBTO
{
    public class RequestDocumentModel
    {
        public string DocumentTitle { get; set; }
        public string DocumentTitleChanged { get; set; }
        public int docReq { get; set; }
        public int docUser { get; set; }
        public double FileSize { get; set; }
    }
}