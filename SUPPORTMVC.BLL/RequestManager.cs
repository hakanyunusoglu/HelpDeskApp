using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using SUPPORTMVC.BLL.Abstract;
using SUPPORTMVC.BLL.Results;
using SUPPORTMVC.COMMON;
using SUPPORTMVC.COMMON.Helpers;
using SUPPORTMVC.DLL.EntityFramework;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.ENTITIES.DBTO;

namespace SUPPORTMVC.BLL
{
    public class RequestManager : ManagerBase<Requests>
    {
        SupportContext sc = new SupportContext();

        private Repository<Users> repo_users = new Repository<Users>();
        private Repository<Requests> repo_requests = new Repository<Requests>();
        private Repository<RequestComments> repo_comments = new Repository<RequestComments>();
        private Repository<Companies> repo_company = new Repository<Companies>();
        private Repository<RequestType> repo_type = new Repository<RequestType>();
        private Repository<RequestPriority> repo_priority = new Repository<RequestPriority>();
        private Repository<RequestStatus> repo_status = new Repository<RequestStatus>();
        private Repository<AttendantParameter> repo_attendantParameter = new Repository<AttendantParameter>();
        private Repository<CommentStatus> repo_commentstatus = new Repository<CommentStatus>();
        private Repository<RequestDocuments> repo_documents = new Repository<RequestDocuments>();
        private Repository<AttendantAssignment> repo_assignmentattendant = new Repository<AttendantAssignment>();

        public List<Users> GetReqUser()
        {
          return repo_users.List();
        }
        public List<Requests> GetRequestList()
        {
            return repo_requests.List();
        } 
        public List<RequestComments> GetRequestComments()
        {
            return repo_comments.List();
        }
        public List<Companies> GetReqComp()
        {
           return repo_company.List();
        }
        public List<RequestType> GetReqTypeList()
        {
            return repo_type.List();
        }
        public List<RequestPriority> GetReqPrioList()
        {
           return repo_priority.List();
        }
        public List<RequestStatus> GetReqStatus()
        {
            return repo_status.List();
        }
        public List<AttendantParameter> GetReqAttendantParameter()
        {
          return  repo_attendantParameter.List();
        }
        public List<CommentStatus> GetCommentStatus()
        {
            return repo_commentstatus.List();
        }
        public List<RequestDocuments> GetCommentDocuments()
        {
            return repo_documents.List();
        }
        public List<AttendantAssignment> GetAssignedAttendants()
        {
            return repo_assignmentattendant.List();
        }

        public ErrorsResults<RequestStatus> AddStatus(RequestStatus model)
        {
            ErrorsResults<RequestStatus> LayerResult = new ErrorsResults<RequestStatus>();
            int dbResult = repo_status.Insert(new RequestStatus()
            {
                StatusTitle = model.StatusTitle
            });
            if (dbResult > 0)
            {
                LayerResult.Result = repo_status.Find(x=> x.StatusID == model.StatusID);
            }

            return LayerResult;

        }
        public ErrorsResults<RequestType> AddType(RequestType model)
        {
            ErrorsResults<RequestType> LayerResult = new ErrorsResults<RequestType>();

            int dbResult = repo_type.Insert(new RequestType()
            {
                RequestTypeTitle = model.RequestTypeTitle
            });
            if (dbResult > 0)
            {
                LayerResult.Result = repo_type.Find(x => x.RequestTypeID == model.RequestTypeID);
            }

            return LayerResult;

        }
        public ErrorsResults<RequestPriority> AddPriority(RequestPriority model)
        {
            ErrorsResults<RequestPriority> LayerResult = new ErrorsResults<RequestPriority>();



            int dbResult = repo_priority.Insert(new RequestPriority()
            {
                PriorityTitle = model.PriorityTitle
            });
            if (dbResult > 0)
            {
                LayerResult.Result = repo_priority.Find(x => x.PriorityID == model.PriorityID);
            }

            return LayerResult;

        }

  
        public ErrorsResults<Requests> AddRequest(RequestAddModel data)
        {
            ErrorsResults<Requests> LayerResult = new ErrorsResults<Requests>();

            int loggeduser = App.Common.GetUserID().Value;
            string requestDate = "";
            int requestStatus = 0;
            int userole = 0;
            int requestCommentStatus = 0;
            int requestAttendantCheck = 0;
            string commenttext = "";
            int lastrequestsID = 0;
            foreach (Users usr in GetReqUser())
            {
                if (usr.UserID == loggeduser)
                {
                    userole = usr.RoleID;
                }
            }
            if (((!String.IsNullOrEmpty(data.RequestDate) && data.RequestDate != "undefined") || ((String.IsNullOrEmpty(data.RequestDate) || data.RequestDate == "undefined") && userole < 3)) &&
                (!String.IsNullOrEmpty(data.ReqCompany.ToString()) && data.ReqCompany != 0) &&
                (!String.IsNullOrEmpty(data.ReqUser.ToString()) && data.ReqUser != 0) &&
                (!String.IsNullOrEmpty(data.reqPriority.ToString()) && data.reqPriority != 0) &&
                !String.IsNullOrEmpty(data.RequestDescription) &&
                (!String.IsNullOrEmpty(data.RequestType.ToString()) && data.RequestType != 0))
            {

                if ((String.IsNullOrEmpty(data.RequestDate) || data.RequestDate == "undefined") && userole < 3)
                {
                    requestDate = data.UserDatetime;
                }
                else
                {
                    requestDate = data.RequestDate;
                }

                if (data.ReqStatus != 0 || !String.IsNullOrEmpty(data.CommentText) || data.CommentTotalTime != 0)
                {
                    if ((String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0) &&
                        (String.IsNullOrEmpty(data.CommentTotalTime.ToString()) || data.CommentTotalTime == 0) &&
                        !String.IsNullOrEmpty(data.CommentText))
                    {
                        LayerResult.Errors.Add("Lütfen tüm alanları doldurduğunuzdan emin olunuz!");
                    }

                    if ((String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0) &&
                        (String.IsNullOrEmpty(data.CommentText)) &&
                        (!String.IsNullOrEmpty(data.CommentTotalTime.ToString()) || data.CommentTotalTime != 0))
                    {
                        LayerResult.Errors.Add("Lütfen tüm alanları doldurduğunuzdan emin olunuz!");
                    }

                    if ((!String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus != 0) &&
                        (!String.IsNullOrEmpty(data.CommentText)) &&
                        (String.IsNullOrEmpty(data.CommentTotalTime.ToString()) || data.CommentTotalTime == 0))
                    {
                        LayerResult.Errors.Add("Lütfen tüm alanları doldurduğunuzdan emin olunuz!");
                    }

                    if ((String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0) &&
                        (!String.IsNullOrEmpty(data.CommentText)) &&
                        (!String.IsNullOrEmpty(data.CommentTotalTime.ToString()) || data.CommentTotalTime != 0))
                    {
                        LayerResult.Errors.Add("Lütfen tüm alanları doldurduğunuzdan emin olunuz!");
                    }

                    if ((!String.IsNullOrEmpty(data.ReqStatus.ToString()) && data.ReqStatus != 0) &&
                        (String.IsNullOrEmpty(data.CommentText)) &&
                        (!String.IsNullOrEmpty(data.CommentTotalTime.ToString()) && data.CommentTotalTime != 0))
                    {
                        requestStatus = data.ReqStatus;

                        if (data.ReqStatus == 6)
                        {
                            requestCommentStatus = 5;
                        }

                        if (data.ReqStatus == 7)
                        {
                            requestCommentStatus = 4;
                        }

                        requestAttendantCheck = loggeduser;

                        if (requestCommentStatus == 5)
                        {
                            commenttext = "Talep durumu İPTAL EDİLDİ olarak değiştirilmiştir!";
                        }

                        if (requestCommentStatus == 4)
                        {
                            commenttext = "Talep durumu ONAYLANDI olarak değiştirilmiştir!";
                        }

                    }
                    else if ((!String.IsNullOrEmpty(data.ReqStatus.ToString()) && data.ReqStatus != 0) &&
                             (!String.IsNullOrEmpty(data.CommentText)) &&
                             (!String.IsNullOrEmpty(data.CommentTotalTime.ToString()) && data.CommentTotalTime != 0))
                    {
                        requestStatus = data.ReqStatus;

                        if (data.ReqStatus == 6)
                        {
                            requestCommentStatus = 5;
                        }

                        if (data.ReqStatus == 7)
                        {
                            requestCommentStatus = 4;
                        }

                        requestAttendantCheck = loggeduser;
                        commenttext = data.CommentText;

                    }
                }

                if (String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0)
                {
                    requestStatus = 1;
                    requestCommentStatus = 1;
                    requestAttendantCheck = 19;
                }
                lastrequestsID = GetRequestList().Max(x => x.RequestID);
                foreach (Requests rq in GetRequestList())
                {
                    if (rq.RequestID == lastrequestsID)
                    {
                        if (rq.RequestDate == data.RequestDate && rq.reqPriority.PriorityID == data.reqPriority &&
                            rq.reqStatus.StatusID == requestStatus && rq.CommentStatusID == requestCommentStatus && rq.reqType.RequestTypeID == data.RequestType &&
                            rq.RequestDescription == data.RequestDescription && rq.reqUser.UserID == data.ReqUser &&
                            rq.reqCompany.CompanyID == data.ReqCompany && rq.CreatorUser == loggeduser &&
                            rq.ModifiedUser == loggeduser && rq.AssignmentStatus == 0)
                        {
                            LayerResult.Errors.Add("Talep zaten açılmış!");
                        }
                        else
                        {
                            int dbResult = repo_requests.Insert(new Requests()
                            {
                                RequestDate = requestDate,
                                reqPriority = GetReqPrioList().Find(x => x.PriorityID == data.reqPriority),
                                reqStatus = GetReqStatus().Find(x => x.StatusID == requestStatus),
                                CommentStatusID = requestCommentStatus,
                                RequestNumber = sc.GetNextSequenceValue().ToString(),
                                reqType = GetReqTypeList().Find(x => x.RequestTypeID == data.RequestType),
                                RequestDescription = data.RequestDescription,
                                RequestAttendant = requestAttendantCheck,
                                reqUser = GetReqUser().Find(x => x.UserID == data.ReqUser),
                                reqCompany = GetReqComp().Find(x => x.CompanyID == data.ReqCompany),
                                CreatorUser = App.Common.GetUserID().Value,
                                ModifiedUser = App.Common.GetUserID().Value,
                                AssignmentStatus = 0
                            });
                            
                            lastrequestsID = GetRequestList().Max(x => x.RequestID);
                            data.RequestID = lastrequestsID;
                        }
                    }
                }
                
                if (!String.IsNullOrEmpty(data.ReqStatus.ToString()) && data.ReqStatus != 0)
                {
                    int dbResult2 = repo_comments.Insert(new RequestComments()
                    {
                        CommentDateTime = DateTime.Now.ToShortDateString(),
                        CommentText = commenttext,
                        CommentTotalTime = data.CommentTotalTime,
                        comUser = GetReqUser().Find(x => x.UserID == data.ReqUser),
                        ModifiedUser = loggeduser,
                        ParentID = 0,
                        comStat = GetCommentStatus().Find(x => x.CommentStatusID == requestCommentStatus),
                        comReq = GetRequestList().Find(x => x.RequestID == data.RequestID)

                    });
                }

                string requestOwnerUser = "";
                string requestCompanyMail = " ";
                string requestPriorityMail = " ";
                string requestStatusMail = "Yeni Talep";
                string requestTypeMail = " ";
                string email = "";

                foreach (Users usrs in GetReqUser())
                {
                    if (usrs.UserID == data.ReqUser)
                    {
                        requestOwnerUser = usrs.UName + " " + usrs.USurname;
                        email = usrs.Email;
                    }
                }

                foreach (Companies cp in GetReqComp())
                {
                    if (cp.CompanyID == data.ReqCompany)
                    {
                        requestCompanyMail = cp.CompanyName;
                    }
                }

                foreach (RequestPriority rp in GetReqPrioList())
                {
                    if (rp.PriorityID == data.reqPriority)
                    {
                        requestPriorityMail = rp.PriorityTitle;
                    }
                }

                if (!String.IsNullOrEmpty(data.ReqStatus.ToString()) && data.ReqStatus != 0)
                {
                    foreach (RequestStatus rs in GetReqStatus())
                    {
                        if (rs.StatusID == data.ReqStatus)
                        {
                            requestStatusMail = rs.StatusTitle;
                        }
                    }
                }

                foreach (RequestType rt in GetReqTypeList())
                {
                    if (rt.RequestTypeID == data.RequestType)
                    {
                        requestTypeMail = rt.RequestTypeTitle;
                    }
                }

                string body =
                    $"<b><span style='font-size:15px'>Talep kaydınız başarıyla oluşturulmuştur!</span></b> <br/> " +
                    $"<p><b>Talep Tarihi:</b> <span style='font-size:15px'> {requestDate}</span><br/>" +
                    $"<b>Talep Sahibi:</b> <span style='font-size:15px'> {requestOwnerUser}</span><br/>" +
                    $"<b>Firma Adı:</b> <span style='font-size:15px'> {requestCompanyMail}</span><br/>" +
                    $"<b>Öncelik:</b> <span style='font-size:15px'> {requestPriorityMail}</span><br/>" +
                    $"<b>Talep Durumu:</b> <span style='font-size:15px'> {requestStatusMail}</span><br/> " +
                    $"<b>Konu:</b> <span style='font-size:15px'> {requestTypeMail}</span> <br/>" +
                    $"<b>Sorun Detayları:</b><br/> <span style='font-size:15px'> {data.RequestDescription}</span></p> <br/><br/><br/><hr/><br/>" +

                    $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/KWf1M5f/hbs-logo-mail.png'/></a> <br/> " +
                    $"&nbsp;&nbsp;<b>Tel:</b> (0212) 551 78 78 <br/> " +
                    $"&nbsp;&nbsp;<b>E-Mail:</b> info@hbsyazilim.com <br/> " +
                    $"&nbsp;&nbsp;<b>Web:</b> www.hbsyazilim.com <br/> " +
                    $"&nbsp;&nbsp;<b>Adres:</b> Üniversite mah. Civan sk. <br/>" +
                    $"&nbsp;&nbsp;N:1 K:8 D:115 Allure Tower <br/>" +
                    $"&nbsp;&nbsp;Avcılar/ISTANBUL ";

                MailHelper.SendMail(body, email,
                    "HBSoft Yazılım&Danışmanlık Talep Kaydı Oluşturuldu!");



                //if (dbResult > 0)
                //{
                //    LayerResult.Result = repo_requests.Find(x => x.RequestID == data.RequestID);
                //}
            }
            else
            {
                LayerResult.Errors.Add("Lütfen tüm alanları doldurduğunuzdan emin olunuz!");
            }

            return LayerResult;
        }

        public ErrorsResults<RequestComments> CloseRequestComment(NewCommentModel data)
        {
            ErrorsResults<RequestComments> LayerResult = new ErrorsResults<RequestComments>();

            Requests reqList = repo_requests.Find(x => x.RequestID == data.comReq);
            int loggeduser = App.Common.GetUserID().Value;
            int currentpriority;
            int currentRequeststatus;
            string nullmessage = "";

            if (String.IsNullOrEmpty(data.reqPriority.ToString()) || data.reqPriority == 0)
            {
                currentpriority = reqList.reqPriority.PriorityID;
            }
            else
            {
                currentpriority = data.reqPriority;
            }
            if (String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0)
            {
                currentRequeststatus = reqList.reqStatus.StatusID;
            }
            else
            {
                currentRequeststatus = data.ReqStatus;
            }

            if (String.IsNullOrEmpty(data.CommentText) &&
                (String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0) &&
                (String.IsNullOrEmpty(data.reqPriority.ToString()) || data.reqPriority == 0))
            {
                LayerResult.Errors.Add("Alanlar boş geçilemez!");
            }
            else
            {
            if (reqList != null)
            {
                if (String.IsNullOrEmpty(data.CommentText))
                {
                    if ((!String.IsNullOrEmpty(data.reqPriority.ToString()) && data.reqPriority != 0) &&
                        (String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0))
                    {
                        foreach (RequestPriority rp in GetReqPrioList())
                        {
                            if (rp.PriorityID == data.reqPriority)
                            {
                                data.CommentText = "Talebinizin önceliği " + rp.PriorityTitle.ToUpper() + " olarak güncellenmiştir!";
                            }
                        }
                    }
                    if ((!String.IsNullOrEmpty(data.ReqStatus.ToString()) && data.ReqStatus != 0) &&
                        (String.IsNullOrEmpty(data.reqPriority.ToString()) || data.reqPriority == 0))
                    {
                        foreach (RequestStatus rs in GetReqStatus())
                        {
                            if (rs.StatusID == data.ReqStatus)
                            {
                                data.CommentText = "Talebinizin durumu " + rs.StatusTitle.ToUpper() + " olarak güncellenmiştir!";
                            }
                        }
                    }

                    if ((!String.IsNullOrEmpty(data.ReqStatus.ToString()) && data.ReqStatus != 0) &&
                        (!String.IsNullOrEmpty(data.reqPriority.ToString()) && data.reqPriority != 0))
                    {
                        foreach (RequestPriority rp in GetReqPrioList())
                        {
                            if (rp.PriorityID == data.reqPriority)
                            {
                                nullmessage = $"Talebinizin önceliği {rp.PriorityTitle.ToUpper()}, ";
                            }
                        }
                        foreach (RequestStatus rs in GetReqStatus())
                        {
                            if (rs.StatusID == data.ReqStatus)
                            {
                                nullmessage += $" talebinizin durumu {rs.StatusTitle.ToUpper()} olarak güncellenmiştir!";
                            }
                        }
                        data.CommentText = nullmessage;
                    }
                    
                }
                repo_comments.Insert(new RequestComments()
                {
                    CommentDateTime = DateTime.Now.ToString(),
                   CommentText = "Re: " + data.CommentText,
                   CommentTotalTime = data.CommentTotalTime,
                    comReq = repo_requests.Find(x => x.RequestID == data.comReq),
                    comStat = repo_commentstatus.Find(x => x.CommentStatusID == reqList.CommentStatusID),
                    comUser = repo_users.Find(x => x.UserID == loggeduser),
                    ModifiedUser = loggeduser,
                    ParentID = 0
            });
                reqList.reqStatus = repo_status.Find(x => x.StatusID == currentRequeststatus);
                reqList.ModifiedUser = App.Common.GetUserID().Value;
                reqList.reqPriority = GetReqPrioList().Find(x => x.PriorityID == currentpriority);
                
                if (data.ReqStatus == 5)
                {
                    reqList.CommentStatusID = 2;
                }

                if (data.ReqStatus == 6)
                {
                    reqList.CommentStatusID = 5;
                }

                Update(reqList);

                string body = " ";
                string email = reqList.reqUser.Email;
                if (!String.IsNullOrEmpty(data.CommentTotalTime.ToString()) && data.CommentTotalTime != 0)
                {
                    body =
                        $"<b><span style='font-size:15px'>Talep durumunuz değişmiştir!</span></b> <br/> " +
                        $"<p><b>Talep Tarihi:</b> <span style='font-size:15px'> {reqList.RequestDate}</span><br/>" +
                        $"<b>Talep Sahibi:</b> <span style='font-size:15px'> {reqList.reqUser.UName} {reqList.reqUser.USurname}</span><br/>" +
                        $"<b>Firma Adı:</b> <span style='font-size:15px'> {reqList.reqCompany.CompanyName}</span><br/>" +
                        $"<b>Öncelik:</b> <span style='font-size:15px'> {reqList.reqPriority.PriorityTitle}</span><br/>" +
                        $"<b>Talep Durumu:</b> <span style='font-size:15px'> {reqList.reqStatus.StatusTitle}</span><br/> " +
                        $"<b>Konu:</b> <span style='font-size:15px'> {reqList.reqType.RequestTypeTitle}</span> <br/>" +
                        $"<b>Sorun Detayları:</b><br/> <span style='font-size:15px'> {reqList.RequestDescription}</span><br/><hr/><br/>" +
                        $"<b>İşlem Süresi:</b><span style='font-size:15px'> {data.CommentTotalTime} Dakika</span><br/>"+
                        $"<b>Yetkili Açıklaması:</b><br/> <span style='font-size:15px'> {data.CommentText}</span></p><br/><hr/><br/>" +

                        $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/KWf1M5f/hbs-logo-mail.png'/></a> <br/> " +
                        $"&nbsp;&nbsp;<b>Tel:</b> (0212) 551 78 78 <br/> " +
                        $"&nbsp;&nbsp;<b>E-Mail:</b> info@hbsyazilim.com <br/> " +
                        $"&nbsp;&nbsp;<b>Web:</b> www.hbsyazilim.com <br/> " +
                        $"&nbsp;&nbsp;<b>Adres:</b> Üniversite mah. Civan sk. <br/>" +
                        $"&nbsp;&nbsp;N:1 K:8 D:115 Allure Tower <br/>" +
                        $"&nbsp;&nbsp;Avcılar/ISTANBUL ";
                }
                else
                {
                    body =
                        $"<b><span style='font-size:15px'>Talep durumunuz değişmiştir!</span></b> <br/> " +
                        $"<p><b>Talep Tarihi:</b> <span style='font-size:15px'> {reqList.RequestDate}</span><br/>" +
                        $"<b>Talep Sahibi:</b> <span style='font-size:15px'> {reqList.reqUser.UName} {reqList.reqUser.USurname}</span><br/>" +
                        $"<b>Firma Adı:</b> <span style='font-size:15px'> {reqList.reqCompany.CompanyName}</span><br/>" +
                        $"<b>Öncelik:</b> <span style='font-size:15px'> {reqList.reqPriority.PriorityTitle}</span><br/>" +
                        $"<b>Talep Durumu:</b> <span style='font-size:15px'> {reqList.reqStatus.StatusTitle}</span><br/> " +
                        $"<b>Konu:</b> <span style='font-size:15px'> {reqList.reqType.RequestTypeTitle}</span> <br/>" +
                        $"<b>Sorun Detayları:</b><br/> <span style='font-size:15px'> {reqList.RequestDescription}</span><br/><hr/><br/>" +
                        $"<b>Yetkili Açıklaması:</b> <span style='font-size:15px'> {data.CommentText}</span></p><br/><br/><hr/><br/>" +

                        $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/KWf1M5f/hbs-logo-mail.png'/></a> <br/> " +
                        $"&nbsp;&nbsp;<b>Tel:</b> (0212) 551 78 78 <br/> " +
                        $"&nbsp;&nbsp;<b>E-Mail:</b> info@hbsyazilim.com <br/> " +
                        $"&nbsp;&nbsp;<b>Web:</b> www.hbsyazilim.com <br/> " +
                        $"&nbsp;&nbsp;<b>Adres:</b> Üniversite mah. Civan sk. <br/>" +
                        $"&nbsp;&nbsp;N:1 K:8 D:115 Allure Tower <br/>" +
                        $"&nbsp;&nbsp;Avcılar/ISTANBUL ";
                }

                MailHelper.SendMail(body, email,
                    "HBSoft Yazılım&Danışmanlık Talep Durumu Bilgilendirme!");
            }
            }
            return LayerResult;
        }
        public ErrorsResults<RequestComments> NewCommentAdd(NewCommentModel data)
        {
            int loggeduser = App.Common.GetUserID().Value;
            int commentUser;
            ErrorsResults<RequestComments> LayerResult = new ErrorsResults<RequestComments>();
            
            Requests reqList = repo_requests.Find(x => x.RequestID == data.comReq);

            if (reqList != null)
            {
                if (!String.IsNullOrEmpty(data.ReqStatus.ToString()) && data.ReqStatus != 0)
                {
                    if (data.ReqStatus == 5)
                    {
                        reqList.reqStatus = repo_status.Find(x => x.StatusID == 5);
                        reqList.CommentStatusID = 2;
                        data.comStat = 2;
                    }
                    else
                    {
                        reqList.reqStatus = repo_status.Find(x => x.StatusID == data.ReqStatus);
                        data.comStat = reqList.CommentStatusID;
                    }
                    Update(reqList);
                }
                if ((String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0) && (String.IsNullOrEmpty(data.comStat.ToString()) || data.comStat == 0))
                {
                    data.comStat = reqList.CommentStatusID;
                }
                if (!String.IsNullOrEmpty(data.comStat.ToString()) && data.comStat != 0)
                {
                    if (data.comStat == 4)
                    {
                        reqList.reqStatus = repo_status.Find(x => x.StatusID == 7);
                        reqList.CommentStatusID = 4;
                    }

                    if (data.comStat == 5)
                    {
                        reqList.reqStatus = repo_status.Find(x => x.StatusID == 6);
                        reqList.CommentStatusID = 5;
                    }

                    if (data.comStat == 3)
                    {
                        reqList.reqStatus = repo_status.Find(x => x.StatusID == 2);
                        reqList.CommentStatusID = 3;
                    }

                    if (data.comStat == 2)
                    {
                        reqList.CommentStatusID = 2;
                    }
                }
                if (String.IsNullOrEmpty(data.comUser.ToString()) || data.comUser == 0)
                {
                    commentUser = loggeduser;
                }
                else
                {
                    commentUser = data.comUser;
                }

                int dbResult = repo_comments.Insert(new RequestComments()
                {
                    CommentDateTime = DateTime.Now.ToString(),
                    CommentText = "Re: " + data.CommentText,
                    comReq = repo_requests.Find(x => x.RequestID == data.comReq),
                    comUser = repo_users.Find(x => x.UserID == commentUser),
                    comStat = repo_commentstatus.Find(x=>x.CommentStatusID == data.comStat),
                    CommentTotalTime = data.CommentTotalTime,
                    ModifiedUser = data.ModifiedUser,
                    ParentID = 0
                    
                });
                Update(reqList);
                if (dbResult > 0)
                {
                    LayerResult.Result = repo_comments.Find(x => x.CommentID == data.CommentID);
                    string email = "";
                    string body = " ";
                    string comStatTitle = "";
                    foreach (CommentStatus cs in GetCommentStatus())
                    {
                        if (cs.CommentStatusID == data.comStat)
                        {
                            comStatTitle = cs.CommentStatusTitle;
                        }
                    }
                    foreach (Users usr in GetReqUser())
                    {
                        if (usr.UserID == commentUser)
                        {
                            if (usr.RoleID <= 2)
                            {
                                body =
                                        $"<b><span style='font-size:15px'>Talep durumunuz değişmiştir!</span></b> <br/> " +
                                        $"<p><b>Talep Tarihi:</b> <span style='font-size:15px'> {reqList.RequestDate}</span><br/>" +
                                        $"<b>Talep Sahibi:</b> <span style='font-size:15px'> {reqList.reqUser.UName} {reqList.reqUser.USurname}</span><br/>" +
                                        $"<b>Firma Adı:</b> <span style='font-size:15px'> {reqList.reqCompany.CompanyName}</span><br/>" +
                                        $"<b>Öncelik:</b> <span style='font-size:15px'> {reqList.reqPriority.PriorityTitle}</span><br/>" +
                                        $"<b>Talep Durumu:</b> <span style='font-size:15px'> {reqList.reqStatus.StatusTitle}</span><br/> " +
                                        $"<b>Konu:</b> <span style='font-size:15px'> {reqList.reqType.RequestTypeTitle}</span> <br/>" +
                                        $"<b>Sorun Detayları:</b><br/> <span style='font-size:15px'> {reqList.RequestDescription}</span><br/><hr/><br/>" +
                                        $"<b>Kullanıcı Talep Durumu:</b><span style='font-size:15px'> {comStatTitle} Dakika</span><br/>" +
                                        $"<b>Yorum Detayları:</b><br/> <span style='font-size:15px'> {data.CommentText}</span></p><br/><hr/><br/>" +

                                        $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/KWf1M5f/hbs-logo-mail.png'/></a> <br/> " +
                                        $"&nbsp;&nbsp;<b>Tel:</b> (0212) 551 78 78 <br/> " +
                                        $"&nbsp;&nbsp;<b>E-Mail:</b> info@hbsyazilim.com <br/> " +
                                        $"&nbsp;&nbsp;<b>Web:</b> www.hbsyazilim.com <br/> " +
                                        $"&nbsp;&nbsp;<b>Adres:</b> Üniversite mah. Civan sk. <br/>" +
                                        $"&nbsp;&nbsp;N:1 K:8 D:115 Allure Tower <br/>" +
                                        $"&nbsp;&nbsp;Avcılar/ISTANBUL ";

                                foreach (Users usrattendant in GetReqUser())
                                {
                                    if (usrattendant.UserID == reqList.RequestAttendant)
                                    {
                                        email = usrattendant.Email;
                                    }
                                }
                                MailHelper.SendMail(body, email,
                                    "HBSoft Yazılım&Danışmanlık Talep Durumu Bilgilendirmesi!");
                            }
                            else
                            {
                                body =
                                   $"<b><span style='font-size:15px'>Talep durumunuz değişmiştir!</span></b> <br/> " +
                                        $"<p><b>Talep Tarihi:</b> <span style='font-size:15px'> {reqList.RequestDate}</span><br/>" +
                                        $"<b>Talep Sahibi:</b> <span style='font-size:15px'> {reqList.reqUser.UName} {reqList.reqUser.USurname}</span><br/>" +
                                        $"<b>Firma Adı:</b> <span style='font-size:15px'> {reqList.reqCompany.CompanyName}</span><br/>" +
                                        $"<b>Öncelik:</b> <span style='font-size:15px'> {reqList.reqPriority.PriorityTitle}</span><br/>" +
                                        $"<b>Talep Durumu:</b> <span style='font-size:15px'> {reqList.reqStatus.StatusTitle}</span><br/> " +
                                        $"<b>Konu:</b> <span style='font-size:15px'> {reqList.reqType.RequestTypeTitle}</span> <br/>" +
                                        $"<b>Sorun Detayları:</b><br/> <span style='font-size:15px'> {reqList.RequestDescription}</span><br/><hr/><br/>" +
                                        $"<b>Kullanıcı Talep Durumu:</b><span style='font-size:15px'> {comStatTitle} Dakika</span><br/>" +
                                        $"<b>Yorum Detayları:</b><br/> <span style='font-size:15px'> {data.CommentText}</span></p><br/><hr/><br/>" +

                                        $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/KWf1M5f/hbs-logo-mail.png'/></a> <br/> " +
                                        $"&nbsp;&nbsp;<b>Tel:</b> (0212) 551 78 78 <br/> " +
                                        $"&nbsp;&nbsp;<b>E-Mail:</b> info@hbsyazilim.com <br/> " +
                                        $"&nbsp;&nbsp;<b>Web:</b> www.hbsyazilim.com <br/> " +
                                        $"&nbsp;&nbsp;<b>Adres:</b> Üniversite mah. Civan sk. <br/>" +
                                        $"&nbsp;&nbsp;N:1 K:8 D:115 Allure Tower <br/>" +
                                        $"&nbsp;&nbsp;Avcılar/ISTANBUL ";

                                MailHelper.SendMail(body, reqList.reqUser.Email,
                                    "HBSoft Yazılım&Danışmanlık Talep Durumu Bilgilendirmesi!");
                            }
                        }
                    }
                }

                return LayerResult;
            }

            return LayerResult;
        }

        public ErrorsResults<RequestComments> CommentAnswerNew(NewCommentModel data)
        {
            ErrorsResults<RequestComments> LayerResult = new ErrorsResults<RequestComments>();
            RequestComments reqCompList = GetRequestComments().Find(x => x.CommentID == data.ParentID);
            Requests reqList = GetRequestList().Find(x => x.RequestID == data.comReq);

            int dbResult = repo_comments.Insert(new RequestComments()
            {
                CommentDateTime = DateTime.Now.ToString(),
                CommentText = "Re: " + data.CommentText,
                comReq = repo_requests.Find(x => x.RequestID == data.comReq),
                comUser = repo_users.Find(x => x.UserID == data.comUser),
                comStat = repo_commentstatus.Find(x => x.CommentStatusID == data.comStat),
                ModifiedUser = data.ModifiedUser,
                ParentID = data.ParentID
            });
            string commentdate = DateTime.Now.ToString();
            if (dbResult > 0)
            {
                LayerResult.Result = repo_comments.Find(x => x.CommentID == data.CommentID);
            }


            string body =
                $"<b><span style='font-size:15px'>Talep durumunuz değişmiştir!</span></b> <br/> " +
                $"<p><b>Talep Tarihi:</b> <span style='font-size:15px'> {reqList.RequestDate}</span><br/>" +
                $"<b>Talep Sahibi:</b> <span style='font-size:15px'> {reqList.reqUser.UName} {reqList.reqUser.USurname}</span><br/>" +
                $"<b>Firma Adı:</b> <span style='font-size:15px'> {reqList.reqCompany.CompanyName}</span><br/>" +
                $"<b>Konu:</b> <span style='font-size:15px'> {reqList.reqType.RequestTypeTitle}</span> <br/>" +
                $"<b>Sorun Detayları:</b><br/> <span style='font-size:15px'> {reqList.RequestDescription}</span><br/><br/>" +
                $"<b>Yanıtlanan Yorum Tarihi:</b> <span style='font-size:15px'> {reqCompList.CommentDateTime}</span><br/>" +
                $"<b>Yanıtlanan Yorum Detayı:</b><br/> <span style='font-size:15px'> {reqCompList.CommentText}</span><br/><hr/><br/>" +
                $"<b>Yorum Cevap Tarihi:</b><br/> <span style='font-size:15px'> {commentdate}</span><br/>"+
                $"<b>Yorum Cevap Detayı:</b><br/> <span style='font-size:15px'> {data.CommentText}</span></p><br/><hr/><br/>" +

                $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/KWf1M5f/hbs-logo-mail.png'/></a> <br/> " +
                $"&nbsp;&nbsp;<b>Tel:</b> (0212) 551 78 78 <br/> " +
                $"&nbsp;&nbsp;<b>E-Mail:</b> info@hbsyazilim.com <br/> " +
                $"&nbsp;&nbsp;<b>Web:</b> www.hbsyazilim.com <br/> " +
                $"&nbsp;&nbsp;<b>Adres:</b> Üniversite mah. Civan sk. <br/>" +
                $"&nbsp;&nbsp;N:1 K:8 D:115 Allure Tower <br/>" +
                $"&nbsp;&nbsp;Avcılar/ISTANBUL ";

            MailHelper.SendMail(body, reqCompList.comUser.Email,
                "HBSoft Yazılım&Danışmanlık Talep Durumu Bilgilendirmesi!");


            return LayerResult;
        }

        public ErrorsResults<RequestDocuments> NewRequestDocument(RequestDocumentModel rdm)
        {
            ErrorsResults<RequestDocuments> LayerResult = new ErrorsResults<RequestDocuments>();
            //double kb = rdm.FileSize / 1024;
            //double mb = kb / 1024;
            if (rdm.FileSize <= 5242880)
            {
                int dbResultDoc = repo_documents.Insert(new RequestDocuments()
                {
                    docReq = GetRequestList().Find(x => x.RequestID == rdm.docReq),
                    DocumentTitle = rdm.DocumentTitle,
                    DocumentTitleChanged = rdm.DocumentTitleChanged,
                    AddedDate = DateTime.Now.ToShortDateString(),
                    AddedHour = DateTime.Now.ToShortTimeString(),
                    docUser = GetReqUser().Find(x => x.UserID == rdm.docUser),
                    FileSize = rdm.FileSize
                });
            }
            return LayerResult;
        }
        public ErrorsResults<Requests> NewRequestAttendantUser(TakeRequestSelectedModel data)
        {
            ErrorsResults<Requests> LayerResult = new ErrorsResults<Requests>();
            Requests reqList = repo_requests.Find(x => x.RequestID == data.ReqsID);
            AttendantAssignment atsList = repo_assignmentattendant.Find(x => x.RequestID == data.ReqsID);
            int loggeduser = App.Common.GetUserID().Value;

            
            if (reqList != null)
            {
                if (atsList != null)
                {
                    if (data.TakeRequestSelectedValue == loggeduser)
                    {
                        atsList.AssignmentStatus = 1;
                        reqList.AssignmentStatus = 2;
                        atsList.UserID = data.TakeRequestSelectedValue;
                        atsList.ModifiedUser = loggeduser;
                        atsList.AssignmentDate = DateTime.Now.ToString();
                        reqList.RequestAttendant = loggeduser;
                    }
                    else { 
                    atsList.AssignmentStatus = 0;
                    atsList.UserID = data.TakeRequestSelectedValue;
                    atsList.ModifiedUser = loggeduser;
                    atsList.AssignmentDate = DateTime.Now.ToString();
                    reqList.AssignmentStatus = 1;
                    }
                }
                else
                { 
                    if(data.TakeRequestSelectedValue == loggeduser)
                    {
                        int dbResult = repo_assignmentattendant.Insert(new AttendantAssignment()
                        {
                            ModifiedUser = loggeduser,
                            AssignmentDate = DateTime.Now.ToString(),
                            UserID = data.TakeRequestSelectedValue,
                            RequestID = data.ReqsID,
                            AssignmentStatus = 1
                        });
                        reqList.AssignmentStatus = 2;
                        reqList.RequestAttendant = loggeduser;
                    }
                    else
                    {
                        int dbResult = repo_assignmentattendant.Insert(new AttendantAssignment()
                        {
                            ModifiedUser = loggeduser,
                            AssignmentDate = DateTime.Now.ToString(),
                            UserID = data.TakeRequestSelectedValue,
                            RequestID = data.ReqsID,
                            AssignmentStatus = 0
                        });
                        reqList.AssignmentStatus = 1;
                    }
                }
            }
            Update(reqList);

            return LayerResult;
        }

        public ErrorsResults<AttendantAssignment> NewAssignmentAttendant(int? id)
        {
            int loggeduser = App.Common.GetUserID().Value;

            ErrorsResults<AttendantAssignment> LayerResult = new ErrorsResults<AttendantAssignment>();

            Requests reqList = repo_requests.Find(x => x.RequestID == id);
            AttendantAssignment atsList = repo_assignmentattendant.Find(x => x.RequestID == id);

            if (reqList != null)
            {
                reqList.AssignmentStatus = 2;
                reqList.RequestAttendant = loggeduser;
                atsList.AssignmentStatus = 1;
            }
            Update(reqList);
            return LayerResult;
        }

        public ErrorsResults<AttendantAssignment> CancelAssignmentAttendant(int? id)
        {
            int loggeduser = App.Common.GetUserID().Value;

            ErrorsResults<AttendantAssignment> LayerResult = new ErrorsResults<AttendantAssignment>();

            Requests reqList = repo_requests.Find(x => x.RequestID == id);
            AttendantAssignment atsList = repo_assignmentattendant.Find(x => x.RequestID == id);

            if (reqList != null)
            {
                reqList.AssignmentStatus = 0;
                atsList.AssignmentStatus = 2;
            }
            Update(reqList);
            return LayerResult;
        }
    }
}