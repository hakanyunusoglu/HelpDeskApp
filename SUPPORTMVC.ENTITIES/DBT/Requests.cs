using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    [Table("RequestList")]
    public class Requests
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }
        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}"), StringLength(20)]
        [DataType(DataType.Date)]
        public string RequestDate { get; set; }
        public string RequestNumber { get; set; }
        [Required]
        public string RequestDescription { get; set; }
        public int RequestAttendant { get; set; }
        public int CreatorUser { get; set; }
        public int ModifiedUser { get; set; }
        public int CommentStatusID { get; set; }
        public int AssignmentStatus { get; set; }

        public virtual RequestPriority reqPriority { get; set; }
        public virtual RequestStatus reqStatus { get; set; }
        public virtual RequestType reqType { get; set; }
        public virtual Users reqUser { get; set; }
        public virtual Companies reqCompany { get; set; }
        public virtual List<RequestComments> reqComments { get; set; }
        public virtual List<RequestDocuments> reqDoc { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RequestGuidID { get; set; }
    }
}