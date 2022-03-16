using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    [Table("RequestType")]
    public class RequestType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestTypeID { get; set; }
        [Required(ErrorMessage = "Tür başlığı boş geçilemez!"), StringLength(300)]
        public string RequestTypeTitle { get; set; }


        public virtual List<Requests> TypeList { get; set; }

    }
}