using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.ENTITIES;

namespace SUPPORTMVC.DLL.EntityFramework
{
    public class SupportContext: DbContext
    {
        public int GetNextSequenceValue()
        {
            var rawQuery = Database.SqlQuery<int>("SELECT NEXT VALUE FOR seqNumber;");
            var task = rawQuery.SingleAsync();
            int nextVal = task.Result;

            return nextVal;
        }

        public DbSet<Users> SupportUsers { get; set; }
        public DbSet<Requests> SupportRequests { get; set; }
        public DbSet<RequestComments> SupportRequestComments { get; set; }
        public DbSet<Companies> SupportCompanies { get; set; }
        public DbSet<LeftMenuItems> SupportLeftMenuItems { get; set; }
        public DbSet<RequestType> SupportRequestTypes { get; set; }
        public DbSet<RequestPriority> SupportRequestPriorities { get; set; }
        public DbSet<RequestStatus> SupportRequestStatus { get; set; }
        public DbSet<AttendantParameter> SupportAttendantParameters { get; set; }
        public DbSet<RequestDocuments> SupportRequestDocuments { get; set; }
        public DbSet<Notifications> SupportNotifications { get; set; }
        public DbSet<AttendantAssignment> SupportAttendantAssignment { get; set; }
    }
}