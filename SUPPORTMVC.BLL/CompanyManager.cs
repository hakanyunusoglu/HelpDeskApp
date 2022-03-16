using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SUPPORTMVC.BLL.Abstract;
using SUPPORTMVC.BLL.Results;
using SUPPORTMVC.COMMON;
using SUPPORTMVC.DLL.EntityFramework;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.ENTITIES.DBTO;

namespace SUPPORTMVC.BLL
{
    public class CompanyManager : ManagerBase<Companies>
    {
        private Repository<Users> repo_users = new Repository<Users>();

        ErrorsResults<Companies> er = new ErrorsResults<Companies>();

        public List<Companies> GetCompanyList()
        {
            return List();
        }

        public List<Users> GetUserList()
        {
            return repo_users.List();
        }
        public ErrorsResults<Companies> RegisterCompany(CompanyAddModel data)
        {
            Companies chckcompany = Find(x => x.CompanyCode == data.CompanyCode);

            if (chckcompany != null)
            {
                er.Errors.Add("Firma zaten kayıtlı!");
            }
            else
            {
                int dbResult = Insert(new Companies()
                {
                   CompanyCode = data.CompanyCode,
                   CompanyName =  data.CompanyName,
                   CompanyOfficePhone =  data.CompanyOfficePhone,
                   CompanyMobilePhone =  data.CompanyMobilePhone,
                   CompanyAuthName =  data.CompanyAuthName,
                   CompanyAuthSurname = data.CompanyAuthSurname,
                   CompanyEmail = data.CompanyEmail,
                   CompanyDomain = data.CompanyDomain,
                   ModifiedUserID = App.Common.GetUserID().Value
                });
                if (dbResult > 0)
                {
                    er.Result = Find(x => x.CompanyCode == data.CompanyCode);

                }
            }

            return er;
        }

        public ErrorsResults<Companies> EditCompany(CompanyAddModel data)
        {
            int loggeduser = App.Common.GetUserID().Value;
            int roleid = 0;
            foreach (Users usr in GetUserList())
            {
                if (loggeduser == usr.UserID)
                {
                    roleid = usr.RoleID;
                }
            }

            Companies chckcompany = Find(x => x.CompanyID != data.CompanyID && (x.CompanyEmail == data.CompanyEmail || x.CompanyDomain == data.CompanyDomain));

            if (chckcompany != null && chckcompany.CompanyID != data.CompanyID)
            {
                if (chckcompany.CompanyEmail == data.CompanyEmail && chckcompany.CompanyID != data.CompanyID)
                {
                    er.Errors.Add("E-mail adresi kullanılmaktadır!");
                }

                if (chckcompany.CompanyDomain == data.CompanyDomain && chckcompany.CompanyID != data.CompanyID)
                {
                    er.Errors.Add("Domain adresi kullanılmaktadır!");
                }
                if (chckcompany.CompanyCode == data.CompanyCode && chckcompany.CompanyID != data.CompanyID)
                {
                    er.Errors.Add("Firma kodu kullanılmaktadır!");
                }
                return er;
            }

            er.Result = Find(x => x.CompanyID == data.CompanyID);
                if (!String.IsNullOrEmpty(data.CompanyEmail) && !String.IsNullOrEmpty(data.CompanyDomain))
                {
                    er.Result.CompanyCode = data.CompanyCode;
                    er.Result.CompanyName = data.CompanyName;
                    er.Result.CompanyAuthName = data.CompanyAuthName;
                    er.Result.CompanyAuthSurname = data.CompanyAuthSurname;
                    er.Result.CompanyMobilePhone = data.CompanyMobilePhone;
                    er.Result.CompanyOfficePhone = data.CompanyOfficePhone;
                    er.Result.CompanyEmail = data.CompanyEmail;
                    er.Result.CompanyDomain = data.CompanyDomain;
                    er.Result.ModifiedUserID = loggeduser;
                }

            if (Update(er.Result) == 0)
            {
                //LayerResult.Errors.Add("Başarısız! Bilgiler güncellenirken bir hata oluştu!");
            }

            return er;
        }
    }
}