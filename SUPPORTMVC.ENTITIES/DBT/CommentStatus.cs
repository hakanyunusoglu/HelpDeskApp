using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    public class CommentStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentStatusID { get; set; }
        public string CommentStatusTitle { get; set; }

        public virtual List<RequestComments> reqComStat { get; set; }
    }
}