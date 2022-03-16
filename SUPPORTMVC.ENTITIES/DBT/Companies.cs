using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    [Table("Companies")]
    public class Companies
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }
        [Required, StringLength(50)]
        public string CompanyCode { get; set; }
        [Required, StringLength(300)]
        public string CompanyName { get; set; }
        [StringLength(50)]
        public string CompanyOfficePhone { get; set; }
        [StringLength(50)]
        public string CompanyMobilePhone { get; set; }
        [StringLength(30)]
        public string CompanyAuthName { get; set; }
        [StringLength(40)]
        public string CompanyAuthSurname { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyDomain { get; set; }
        public int ModifiedUserID { get; set; }

        public virtual List<Users> userList { get; set; }
        public virtual List<Requests> reqList { get; set; }
    }
}