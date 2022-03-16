using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    [Table("AssignedRequests")]
    public class AttendantAssignment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignedReqID { get; set; }
        [Required]
        public int ModifiedUser { get; set; }
        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}"), StringLength(35)]
        [DataType(DataType.Date)]
        public string AssignmentDate { get; set; }
        public int UserID { get; set; }
        public int RequestID { get; set; }
        public int AssignmentStatus { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AssignmentGuidID { get; set; }

    }
}