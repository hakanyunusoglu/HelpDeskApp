using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    public class Notifications
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotifyID { get; set; }
        public string NotifyMessage { get; set; }
        public string NotifySubject { get; set; }
        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}"), StringLength(20)]
        [DataType(DataType.Date)]
        public string NotifyAddedDate { get; set; }
        [DataType(DataType.Time), StringLength(10)]
        public string NotifyAddedHour { get; set; }
    }
}