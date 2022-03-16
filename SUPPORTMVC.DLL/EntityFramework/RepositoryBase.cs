using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SUPPORTMVC.DLL.EntityFramework;

namespace SUPPORTMVC.DLL.EntityFramework
{
    public class RepositoryBase
    {
        protected static SupportContext db;
        private static object _locksync = new object();

        protected RepositoryBase()
        {
            CreateContext();
        }

        private static void CreateContext()
        {
            if (db == null)
            {
                lock (_locksync)
                { 
                    if(db == null)
                    { 
                      db = new SupportContext();
                    }
                }
            }
        }

    }
}