using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBTO
{
    public class RequestAddModel
    {
        public int RequestID { get; set; }
        [Required(ErrorMessage = "Tarih alanı boş geçilemez!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public string RequestDate { get; set; }
        public string UserDatetime { get; set; }
        public int reqPriority { get; set; }
        [Required(ErrorMessage = "Destek türü boş geçilemez!")]
        public int RequestType { get; set; }
        [Required(ErrorMessage = "Açıklama alanı boş geçilemez!")]
        public string RequestDescription { get; set; }
        public int ReqUser { get; set; }
        public int ReqCompany { get; set; }
        public int ReqStatus { get; set; }
        public int CommentTotalTime { get; set; }
        public string CommentText { get; set; }
        public string RequestNumber { get; set; }
    }
}