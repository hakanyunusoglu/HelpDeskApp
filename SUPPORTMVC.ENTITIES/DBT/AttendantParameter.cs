using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    [Table("HbsRoleParameter")]
    public class AttendantParameter
    {
        [Key]
        public int ParameterID { get; set; }
        public byte HbsRoleAuth { get; set; }
        public byte AdminAttendantEnable { get; set; }
        public byte AttendantDifferentAuth { get; set; }
        
    }
}