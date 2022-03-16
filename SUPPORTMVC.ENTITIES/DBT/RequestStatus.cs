using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    public class RequestStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusID { get; set; }
        [Required(ErrorMessage = "Durum başlığı boş geçilemez!"), StringLength(300)]
        public string StatusTitle { get; set; }

        public virtual List<Requests> StatList { get; set; }
    }
}