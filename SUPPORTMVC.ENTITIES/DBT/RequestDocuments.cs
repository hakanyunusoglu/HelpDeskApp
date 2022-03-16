using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    public class RequestDocuments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentID { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentTitleChanged { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}"), StringLength(15)]
        [DataType(DataType.Date)]
        public string AddedDate { get; set; }
        [DataType(DataType.Time)]
        public string AddedHour { get; set; }
        public double FileSize { get; set; }

        public virtual Requests docReq { get; set; }
        public virtual Users docUser { get; set; }
    }
}