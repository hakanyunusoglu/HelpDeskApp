using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPPORTMVC.ENTITIES.DBTO
{
    public class NewCommentModel
    {
        public int CommentID { get; set; }
        [AllowHtml]
        public string CommentText { get; set; }
        public int comReq { get; set; }
        public int comUser { get; set; }
        public int comStat { get; set; }
        public int ReqStatus { get; set; }
        public int CommentTotalTime { get; set; }
        public int reqPriority { get; set; }
        public int ModifiedUser { get; set; }
        public int ParentID { get; set; }
        public Guid CommentGuidID { get; set; }
    }
}