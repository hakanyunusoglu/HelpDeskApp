using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SUPPORTMVC.ENTITIES.DBTO
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez!"), DataType(DataType.Password)]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}