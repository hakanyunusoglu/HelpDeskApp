using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.ENTITIES.DBTO
{
    public class UserAddModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Ad alanı boş geçilemez!")]
        public string UName { get; set; }
        [Required(ErrorMessage = "Soyad alanı boş geçilemez!")]
        public string USurname { get; set; }
        [EmailAddress(ErrorMessage = "Geçersiz e-mail adresi!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Firma alanı boş geçilemez!")]
        public int CompanyID { get; set; }
        public string ProfileImage { get; set; }
        public bool Authorized { get; set; }
        public bool LoginAuthorized { get; set; }
        [Required(ErrorMessage = "Kullanıcı tipi boş geçilemez!")]
        public int RoleID { get; set; }
        public bool IsActive { get; set; }

        
    }

    
}