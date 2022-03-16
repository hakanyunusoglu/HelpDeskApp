using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace SUPPORTMVC.ENTITIES.DBT
{
    [Table("Users")]
    public class Users
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [StringLength(20)]
        public string Username { get; set; }
        [StringLength(25)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Ad alanı boş geçilemez!"), StringLength(30)]
        public string UName { get; set; }
        [Required(ErrorMessage = "Soyad alanı boş geçilemez!"), StringLength(50)]
        public string USurname { get; set; }
        [Required(ErrorMessage = "Email alanı boş geçilemez!"), StringLength(200)]
        public string Email { get; set; }
        [StringLength(100)]
        public string ProfileImage { get; set; }
        public int Authorized { get; set; }
        public int LoginAuthorized { get; set; }
        public int RoleID { get; set; }
        public int CompanyID { get; set; }
        public bool IsActive { get; set; }
        public int CreatorUserID { get; set; }
        public int ModifiedUserID { get; set; }
        [StringLength(20)]
        [DataType(DataType.Date)]
        public string CreationDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserGuidID { get; set; }

        public virtual Companies Company { get; set; }
        public virtual List<Requests> RequestList { get; set; }
        public virtual List<RequestComments> CommentList { get; set; }
        public virtual List<RequestDocuments> userDoc { get; set; }
    }
}