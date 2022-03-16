using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SUPPORTMVC.ENTITIES.DBTO
{
    public class CompanyAddModel
    {   [Required(ErrorMessage = "Firma kodu boş geçilemez!")]
        public string CompanyCode { get; set; }
        [Required(ErrorMessage = "Firma adı boş geçilemez!")]
        public string CompanyName { get; set; }
        public string CompanyOfficePhone { get; set; }
        public string CompanyMobilePhone { get; set; }
        public string CompanyAuthName { get; set; }
        public string CompanyAuthSurname { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyDomain { get; set; }
        public int CompanyID { get; set; }
    }
}