using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    public class RequestPriority
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriorityID { get; set; }
        [Required(ErrorMessage = "Öncelik başlığı boş geçilemez!"), StringLength(300)]
        public string PriorityTitle { get; set; }

        public virtual List<Requests> PrioList { get; set; }
    }
}