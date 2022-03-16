using System;
using System.Collections.Generic;
using SUPPORTMVC.BLL.Abstract;
using SUPPORTMVC.BLL.Results;
using SUPPORTMVC.COMMON;
using SUPPORTMVC.COMMON.Helpers;
using SUPPORTMVC.DLL.EntityFramework;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.ENTITIES.DBTO;

namespace SUPPORTMVC.BLL
{
    public class UserManager : ManagerBase<Users>
    {
        private Repository<Companies> repo_company = new Repository<Companies>();
        public List<Companies> GetReqComp()
        {
            return repo_company.List();
        }
        RequestManager rm = new RequestManager();

        private ErrorsResults<Users> er = new ErrorsResults<Users>();

        public List<Users> GetUsers()
        {
            return List();
        }

        public ErrorsResults<Users> RegisterUser(UserAddModel data)
        {
            Users chckuser = Find(x => x.Username == data.Username || x.Email == data.Email);


            if (chckuser != null)
            {
                if (chckuser.Username == data.Username)
                {
                    er.Errors.Add("Kayıtlı kullanıcı adı!");
                }

                if (chckuser.Email == data.Email)
                {
                    er.Errors.Add("Kayıtlı e-mail adresi!");
                }
            }
            else
            {


                int dta;
                int dtla;
                if (data.Authorized == true)
                {
                    dta = 1;
                }
                else
                {
                    dta = 0;
                }

                if (data.LoginAuthorized == true)
                {
                    dtla = 1;
                }
                else
                {
                    dtla = 0;
                }

                int dbResult = Insert(new Users()
                {
                    Username = data.Username,
                    Password = data.Password,
                    UName = data.UName,
                    USurname = data.USurname,
                    Email = data.Email,
                    CompanyID = data.CompanyID,
                    Authorized = dta,
                    LoginAuthorized = dtla,
                    ProfileImage = "user_default.png",
                    IsActive = true,
                    RoleID = data.RoleID,
                    CreatorUserID = App.Common.GetUserID().Value,
                    CreationDate = DateTime.Now.ToString()


                });

                if (dbResult > 0)
                {
                    er.Result = Find(x => x.Username == data.Username && x.Email == data.Email);
                    string body =
                        $"<b>Tebrikler <span style='font-size:15px'> {data.UName + " " + data.USurname}!</span> <br/> " +
                        $"Kullanıcı kaydınız başarı ile tamamlanmıştır! <br/> " +
                        $"Sisteme giriş yapabilmeniz için gereken kullanıcı bilgileriniz aşağıdadır.</b> <br/><br/> " +
                        $"<p><b>Kullanıcı Adınız:</b> <span style='font-size:15px'> {er.Result.Username}</span> <br/> " +
                        $"<b>Şifreniz:</b> <span style='font-size:15px'>{er.Result.Password} </span> <br/>" +
                        $"<b>Firmanız:</b> <span style='font-size:15px'> {er.Result.Company.CompanyName} </span> <br/><br/><hr><br/>" +
                        $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/KWf1M5f/hbs-logo-mail.png'/></a> <br/> " +
                        $"&nbsp;&nbsp;<b>Tel:</b> (0212) 551 78 78 <br/> " +
                        $"&nbsp;&nbsp;<b>E-Mail:</b> info@hbsyazilim.com <br/> " +
                        $"&nbsp;&nbsp;<b>Web:</b> www.hbsyazilim.com <br/> " +
                        $"&nbsp;&nbsp;<b>Adres:</b> Üniversite mah. Civan sk. <br/>" +
                        $"&nbsp;&nbsp;N:1 K:8 D:115 Allure Tower <br/>" +
                        $"&nbsp;&nbsp;Avcılar/ISTANBUL ";

                    MailHelper.SendMail(body, data.Email, "HBSoft Yazılım&Danışmanlık Kullanıcı Bilgilendirmesi!");

                }
            }

            return er;
        }

        public ErrorsResults<Users> LoginUser(LoginViewModel data)
        {
            er.Result = Find(x => x.Username == data.Username && x.Password == data.Password);

            if (er.Result != null)
            {
                if (!er.Result.IsActive)
                {
                    er.Errors.Add("Giriş yetkisi bulunmamaktadır!");
                }
            }
            else
            {
                er.Errors.Add("Kullanıcı adı veya şifre hatalı!");
            }

            return er;
        }

        public ErrorsResults<Users> UpdateProfile(Users data)
        {
            Users chckuser =
                Find(x => x.UserID != data.UserID && (x.Username == data.Username || x.Email == data.Email));

            if (chckuser != null && chckuser.UserID != data.UserID)
            {
                if (chckuser.Email == data.Email && chckuser.UserID != data.UserID)
                {
                    er.Errors.Add("E-mail adresi kullanılmaktadır!");
                }

                if (chckuser.Username == data.Username && chckuser.UserID != data.UserID)
                {
                    er.Errors.Add("Kullanıcı adı kullanılmaktadır!");
                }

                return er;
            }

            er.Result = Find(x => x.UserID == data.UserID);
            if (er.Result.RoleID >= 3)
            {
                er.Result.Username = data.Username;
                er.Result.Password = data.Password;
                er.Result.UName = data.UName;
                er.Result.USurname = data.USurname;
                er.Result.Email = data.Email;
            }
            else if (er.Result.RoleID < 3)
            {
                er.Result.Password = data.Password;
                er.Result.Email = data.Email;
            }

            if (string.IsNullOrEmpty(data.ProfileImage) == false)
            {
                er.Result.ProfileImage = data.ProfileImage;
            }

            if (Update(er.Result) == 0)
            {
                //LayerResult.Errors.Add("Başarısız! Bilgiler güncellenirken bir hata oluştu!");
            }

            return er;

        }

        public ErrorsResults<Users> GetUserById(int id)
        {
            er.Result = Find(x => x.UserID == id);
            if (er.Result == null)
            {
                er.Errors.Add("Kullanıcı Bulunamadı!");

            }

            return er;
        }

        public ErrorsResults<Users> ForgotPassword(LoginViewModel data)
        {
            ErrorsResults<Users> LayerResult = new ErrorsResults<Users>();
            List<Users> usrlist = GetUsers();

            Users userFound = rm.GetReqUser().Find(x => x.Email == data.Email);

            if (userFound == null)
            {
                LayerResult.Errors.Add("Lütfen geçerli bir e-mail adresi giriniz!");
            }
            else
            {
                foreach (Users usr in usrlist)
                {
                    if (usr.Email == data.Email)
                    {
                        string body =
                            $"Sisteme giriş yapabilmeniz için gereken kullanıcı bilgileriniz aşağıdadır!</b> <br/> " +
                            $"<p><b>Kullanıcı Adınız:</b> <span style='font-size:15px'> {usr.Username}</span> <br/> " +
                            $"<b>Şifreniz:</b> <span style='font-size:15px'>{usr.Password} </span> <br/>" +
                            $"<b>Firmanız:</b> <span style='font-size:15px'> {usr.Company.CompanyName} </span> <br/><br/><hr><br/>" +
                            $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/KWf1M5f/hbs-logo-mail.png'/></a> <br/> " +
                            $"&nbsp;&nbsp;<b>Tel:</b> (0212) 551 78 78 <br/> " +
                            $"&nbsp;&nbsp;<b>E-Mail:</b> info@hbsyazilim.com <br/> " +
                            $"&nbsp;&nbsp;<b>Web:</b> www.hbsyazilim.com <br/> " +
                            $"&nbsp;&nbsp;<b>Adres:</b> Üniversite mah. Civan sk. <br/>" +
                            $"&nbsp;&nbsp;N:1 K:8 D:115 Allure Tower <br/>" +
                            $"&nbsp;&nbsp;Avcılar/ISTANBUL ";

                        MailHelper.SendMail(body, data.Email,
                            "HBSoft Yazılım&Danışmanlık Şifremi Unuttum Bilgilendirmesi!");
                    }

                    return LayerResult;
                }
            }

            return LayerResult;
        }

        public ErrorsResults<Users> EditUser(UserAddModel data)
        {
            int loggeduser = App.Common.GetUserID().Value;
            int roleid = 0;
            foreach (Users usr in GetUsers())
            {
                if (loggeduser == usr.UserID)
                {
                    roleid = usr.RoleID;
                }
            }

            Users chckuser = Find(x => x.UserID != data.UserID && (x.Username == data.Username || x.Email == data.Email));

            if (chckuser != null && chckuser.UserID != data.UserID)
            {
                if (chckuser.Email == data.Email && chckuser.UserID != data.UserID)
                {
                    er.Errors.Add("E-mail adresi kullanılmaktadır!");
                }

                if (chckuser.Username == data.Username && chckuser.UserID != data.UserID)
                {
                    er.Errors.Add("Kullanıcı adı kullanılmaktadır!");
                }

                return er;
            }

            er.Result = Find(x => x.UserID == data.UserID);
            if (roleid < 4)
            {
                if (data.RoleID == 1 || data.RoleID == 2 || data.RoleID == 3)
                {
                    er.Result.Username = data.Username;
                    er.Result.Password = data.Password;
                    er.Result.UName = data.UName;
                    er.Result.USurname = data.USurname;
                    er.Result.Email = data.Email;
                    er.Result.RoleID = data.RoleID;
                    er.Result.IsActive = data.IsActive;
                    er.Result.ModifiedUserID = loggeduser;
                }

                if (data.RoleID > 3 || data.RoleID == 0 || data.RoleID < 0)
                {
                    er.Errors.Add("Tanımlı rol bulunamadı!");
                }
            }

            if (roleid == 4)
            {
                if (data.RoleID == 1 || data.RoleID == 2 || data.RoleID == 3 || data.RoleID == 4)
                {
                    er.Result.Username = data.Username;
                    er.Result.Password = data.Password;
                    er.Result.UName = data.UName;
                    er.Result.USurname = data.USurname;
                    er.Result.Email = data.Email;
                    er.Result.RoleID = data.RoleID;
                    er.Result.IsActive = data.IsActive;
                    er.Result.ModifiedUserID = loggeduser;
                }

                if (data.RoleID == 0 || data.RoleID < 0 || data.RoleID > 4)
                {
                    er.Errors.Add("Tanımlı rol bulunamadı!");
                }
            }

            if (Update(er.Result) == 0)
            {
                //LayerResult.Errors.Add("Başarısız! Bilgiler güncellenirken bir hata oluştu!");
            }

            return er;
        }

        public ErrorsResults<Users> QuickNewUserAdd(UserAddModel data)
        {
            Users chckuser = Find(x => x.Username == data.Username || x.Email == data.Email);


            if (chckuser != null)
            {
                if (chckuser.Username == data.Username)
                {
                    er.Errors.Add("Kayıtlı kullanıcı adı!");
                }

                if (chckuser.Email == data.Email)
                {
                    er.Errors.Add("Kayıtlı e-mail adresi!");
                }
            }
            else
            {
                int dbResult = Insert(new Users()
                {
                    Username = data.Username,
                    Password = data.Password,
                    UName = data.UName,
                    USurname = data.USurname,
                    Email = data.Email,
                    CompanyID = GetReqComp().Find(x=>x.CompanyID == data.CompanyID).CompanyID,
                    Authorized = 1,
                    LoginAuthorized = 1,
                    ProfileImage = "user_default.png",
                    IsActive = data.IsActive,
                    RoleID = data.RoleID,
                    CreatorUserID = App.Common.GetUserID().Value,
                    CreationDate = DateTime.Now.ToString(),
                    ModifiedUserID = App.Common.GetUserID().Value
                    
                });

                if (dbResult > 0)
                {
                    er.Result = Find(x => x.Username == data.Username && x.Email == data.Email);
                    string body =
                        $"<b>Tebrikler <span style='font-size:15px'> {data.UName + " " + data.USurname}!</span> <br/> " +
                        $"Kullanıcı kaydınız başarı ile tamamlanmıştır! <br/> " +
                        $"Sisteme giriş yapabilmeniz için gereken kullanıcı bilgileriniz aşağıdadır.</b> <br/><br/> " +
                        $"<p><b>Kullanıcı Adınız:</b> <span style='font-size:15px'> {er.Result.Username}</span> <br/> " +
                        $"<b>Şifreniz:</b> <span style='font-size:15px'>{er.Result.Password} </span> <br/>" +
                        $"<b>Firmanız:</b> <span style='font-size:15px'> {er.Result.Company.CompanyName} </span> <br/><br/><hr><br/>" +
                        $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/KWf1M5f/hbs-logo-mail.png'/></a> <br/> " +
                        $"&nbsp;&nbsp;<b>Tel:</b> (0212) 551 78 78 <br/> " +
                        $"&nbsp;&nbsp;<b>E-Mail:</b> info@hbsyazilim.com <br/> " +
                        $"&nbsp;&nbsp;<b>Web:</b> www.hbsyazilim.com <br/> " +
                        $"&nbsp;&nbsp;<b>Adres:</b> Üniversite mah. Civan sk. <br/>" +
                        $"&nbsp;&nbsp;N:1 K:8 D:115 Allure Tower <br/>" +
                        $"&nbsp;&nbsp;Avcılar/ISTANBUL ";

                    MailHelper.SendMail(body, data.Email, "HBSoft Yazılım&Danışmanlık Kullanıcı Bilgilendirmesi!");

                }
            }

            return er;
        }
    }
} 