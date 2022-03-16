using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    [Table("RequestComments")]
    public class RequestComments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}"), StringLength(50)]
        [DataType(DataType.DateTime)]
        public string CommentDateTime { get; set; }
        [Required]
        public string CommentText { get; set; }
        public int CommentTotalTime { get; set; }
        public int ParentID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CommentGuidID { get; set; }

        public int ModifiedUser { get; set; }
        

        public virtual Users comUser { get; set; }
        public virtual Requests comReq { get; set; }
        public virtual CommentStatus comStat { get; set; }
    }
}