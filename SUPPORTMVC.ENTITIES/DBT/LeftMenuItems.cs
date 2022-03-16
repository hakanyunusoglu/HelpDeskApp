using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBT
{
    [Table("LeftMenuItems")]
    public class LeftMenuItems
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }
        [Required, StringLength(300)]
        public string MenuName { get; set; }
        public int TopMenu { get; set; }
        public int SubMenu { get; set; }
        public int LinkID { get; set; }
        public int AuthID { get; set; }
    }
}