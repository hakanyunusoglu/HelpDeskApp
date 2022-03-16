using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SUPPORTMVC.BLL.Abstract;
using SUPPORTMVC.DLL.EntityFramework;
using SUPPORTMVC.ENTITIES.DBT;

namespace SUPPORTMVC.BLL
{
    public class LeftMenuManager : ManagerBase<LeftMenuItems>
    {
        public List<LeftMenuItems> GetLeftMenuItems()
        {
            return List();
        }

        public LeftMenuItems GetLeftMenuItemID(int id)
        {
            return Find(x => x.LinkID == id);
        }

    }
}